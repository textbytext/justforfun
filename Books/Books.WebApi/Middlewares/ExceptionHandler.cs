using Books.Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.WebApi.Middlewares
{
	internal class ExceptionHandler
	{
		internal static async Task Handle(HttpContext context)
		{
			var logger = context.RequestServices.GetRequiredService<ILogger<ExceptionHandler>>();			

			context.Response.StatusCode = context.Response.StatusCode;
			context.Response.ContentType = "application/json";

			var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
			var ex = exceptionHandlerPathFeature?.Error;

			logger.LogDebug("Handle. " + ex?.Message);

			var result = new ErrorResult();
			if (null != ex)
			{
				if (ex is AggregateException)
				{
					var exceptions = ex as AggregateException;
					exceptions
						.InnerExceptions
						.ToList()
						.ForEach(e =>
						{
							result.SetError("error", e.Message);
						});
				}
				else if (ex is FluentValidation.ValidationException)
				{
					var exceptions = ex as FluentValidation.ValidationException;
					exceptions
						.Errors
						.ToList()
						.ForEach(e =>
						{
							result.SetError(e.PropertyName, e.ErrorMessage);
						});
				}
			}
			var json = JsonSerializer.Serialize(result);
			await context.Response.WriteAsync(json);
			//await context.Response.WriteAsync(new string(' ', 512)); // IE padding
		}
	}
}
