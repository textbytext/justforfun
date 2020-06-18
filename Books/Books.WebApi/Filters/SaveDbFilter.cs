using Books.Domain;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.WebApi.Filters
{
	public class SaveDbFilter : IAsyncActionFilter
    {
        ILogger<SaveDbFilter> _logger;
        public SaveDbFilter(ILogger<SaveDbFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogDebug("BEGIN");
            // код метода
            await next();

            //throw new Exception();
            //var dbContext = context.HttpContext.RequestServices.GetRequiredService<IBooksDbContext>();
            //dbContext.Dispose();

            _logger.LogDebug("END. " + context.HttpContext.Response.StatusCode);
        }
    }
}
