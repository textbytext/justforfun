using AutoMapper;
using Books.Common.Models;
using Books.Core;
using Books.Core.Books.GetBooks;
using Books.Domain;
using Books.Infrastructure.SQLite;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Books.WebApi
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		private readonly bool _isDevelopment;
		const string ProjectName = "Books Web API";

		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			_isDevelopment = env.IsDevelopment();

			Configuration = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
				.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAutoMapper(typeof(GetBookQuery));
			services.AddMediatR(typeof(GetBookQuery).GetTypeInfo().Assembly);
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

			services.AddControllers()
				.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetBookQuery>());

			var connectionString = Configuration.GetConnectionString("SQLiteBooks");
			services
					.AddEntityFrameworkSqlite()
					.AddDbContext<IBooksDbContext, SQLiteBooksDbContext>((sp, options) =>
					{
						options.UseSqlite(connectionString);
						options.UseInternalServiceProvider(sp);
					});
			services.AddScoped<IBooksRepository, BooksRepositorySQLite>();

			services.AddSwaggerGen(s =>
			{
				s.SwaggerDoc("v1", new OpenApiInfo { Title = ProjectName, Version = "v1" });
			});


			services.AddScoped<Core.IMediator, Core.Mediator>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				//app.UseDeveloperExceptionPage();
			}

			app.UseExceptionHandler(errorApp =>
			{
				errorApp.Run(async context =>
				{
					context.Response.StatusCode = context.Response.StatusCode;
					context.Response.ContentType = "application/json";

					var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
					var ex = exceptionHandlerPathFeature?.Error;
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
				});
			});

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ProjectName} v1");
			});

			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
