using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlurFace.Controllers
{
	[Route("/api/proxy")]
	[ApiController]
	[AllowAnonymous]
	public class ProxyApiController : ControllerBase
	{
		private readonly ILogger<ProxyApiController> logger;
		private readonly IBlurFaceService blurFaceService;
		private static string[] validImageExtensions = new[] { ".JPG" };

		public ProxyApiController(
			ILogger<ProxyApiController> logger,
			IBlurFaceService blurFaceService)
		{
			this.logger = logger;
			this.blurFaceService = blurFaceService;
		}

		/*[HttpGet]
		public async Task<IActionResult> Get([FromQuery] string url)
		{
			var Referer = this.HttpContext.Request.Headers["Referer"];
			logger.LogDebug($"Referer: {Referer}, url: {url}");
			await Task.Delay(1);
			return Content(null);
			*//*WebRequest webRequest = HttpWebRequest.Create(url);
			using var response = webRequest.GetResponse();
			var contentType = response.ContentType;
			logger.LogDebug($"{contentType}, {url}");
			using var responseStream = response.GetResponseStream();
			var data = new byte[response.ContentLength];
			var memory = new Memory<byte>(data);
			await responseStream.ReadAsync(memory);
			return File(data, contentType);*//*
		}*/

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] string url)
		{
			logger.LogDebug($"GET: {url}");

			var contentType = "application/x-binary";
			try
			{
				WebRequest webRequest = HttpWebRequest.Create(url);
				using var response = webRequest.GetResponse();
				contentType = response.ContentType;
			}
			catch(Exception error)
			{
				logger.LogError(error.Message);			}			
			

			using var wc = new WebClient();
			var data = await wc.DownloadDataTaskAsync(url);

			if (contentType.IndexOf("jpeg") >= 0)
			{
				data = await blurFaceService.Blur(data);
			}

			return File(data, contentType);
		}
	}
}
