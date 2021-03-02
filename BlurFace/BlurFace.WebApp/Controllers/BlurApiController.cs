/*using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlurFace.Controllers
{
	[Route("/api/blur")]
	[ApiController]
	[AllowAnonymous]
	public class BlurApiController : ControllerBase
	{
		private readonly ILogger<BlurApiController> logger;
		private readonly IBlurFaceService blurFaceService;
		private static string[] validImageExtensions = new[] { ".JPG" };

		public BlurApiController(
			ILogger<BlurApiController> logger,
			IBlurFaceService blurFaceService)
		{
			this.logger = logger;
			this.blurFaceService = blurFaceService;
		}

		[HttpGet("face")]
		public async Task<IActionResult> Face([FromQuery] string url)
		{
			var dotPos = url.LastIndexOf('.');
			if (dotPos >= 0)
			{
				var ext = url.Substring(dotPos)?.ToUpper().Trim();
				logger.LogDebug($"ext: {ext}");
				if (null != ext && !validImageExtensions.Contains(ext))
				{
					return new RedirectResult(url, false);
				}
			}
			else
			{
				return new RedirectResult(url, false);
			}

			logger.LogDebug($"Face. url: {url}");

			var detected = await blurFaceService.Blur(new Uri(url));

			return File(detected, "image/jpeg");
		}

		[HttpGet("ping")]
		public IActionResult Ping()
		{
			logger.LogDebug("Ping");

			return Content("Pong");
		}
	}
}
*/