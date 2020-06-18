using Books.Core;
using Books.Core.Models;
using Books.Domain;
using Books.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.WebApi.Middlewares
{
	public class DataBaseSeeder
	{
        private readonly RequestDelegate _next;
        private readonly ILogger<DataBaseSeeder> _logger;

        public DataBaseSeeder(RequestDelegate next, ILogger<DataBaseSeeder> logger)
        {
            this._next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogDebug("BEGIN");
            var repo = context.RequestServices.GetRequiredService<IBooksRepository>();            

            if (null == repo)
            {
                await _next.Invoke(context);
                return;
            }

            var count = await repo.GetBooksCount();
            if (count == 0)
            {
                await repo.AddBooks(new[] { 
                    new BookDto
                    {
                        Title = "Title 1"
                    },
                    new BookDto
                    {
                        Title = "Title 2"
                    }
                });
            }
            await _next.Invoke(context);

            _logger.LogDebug("END. " + context.Response.StatusCode);
        }
    }
}
