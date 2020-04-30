using Books.Core;
using Books.Core.Models;
using Books.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.WebApi.Middlewares
{
	public class DataBaseSeeder
	{
        private readonly RequestDelegate _next;        

        public DataBaseSeeder(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
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
        }
    }
}
