using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.Web.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
	{
		private readonly ILogger<LoginController> _logger;

		public LoginController(ILogger<LoginController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult IndexPost()
		{
			var user = this.User;
			var user_json = JsonSerializer.Serialize(user);

			HttpContext.Request.Form.TryGetValue("access_token", out StringValues accessToken);
			HttpContext.Request.Form.TryGetValue("id_token", out StringValues idToken);

			var data = new
			{
				accessToken = accessToken.First(),
				idToken = idToken.First(),
				user_json = user_json
			};

			_logger.LogDebug(JsonSerializer.Serialize(data));

			return RedirectToAction("Index", data);
		}

		[HttpPost]
		public async Task Logout()
		{
			await HttpContext.SignOutAsync();
		}

		public async Task<IActionResult> Identity()
		{
			return View();
		}

		public IActionResult Callback()
		{
			_logger.LogDebug("Callback");

			return new EmptyResult();
		}
	}
}