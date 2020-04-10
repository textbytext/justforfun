using AutoMapper;
using Books.Common.Events;
using Books.Core;
using Books.Core.Books;
using Books.Domain;
using Books.Infrastructure.Events;
using Books.Infrastructure.SQLite;
using Books.WebApi.Middlewares;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

namespace Books.WebApi
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		private readonly bool _isDevelopment;
		const string ProjectName = "Books Web API";

		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Console.WriteLine($"EnvironmentName: {env.EnvironmentName}");

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
			services.AddSingleton<IEventBus, EventBus>();
			EventsRegistrator.ConfigureServices(services, typeof(GetBookQuery).Assembly);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			//EventsRegistrator.Configure(app.ApplicationServices, typeof(GetBookQuery).Assembly);

			if (env.IsDevelopment())
			{
				//app.UseDeveloperExceptionPage();
			}

			app.UseExceptionHandler(errorApp => errorApp.Run(ExceptionHandler.Handle));

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
