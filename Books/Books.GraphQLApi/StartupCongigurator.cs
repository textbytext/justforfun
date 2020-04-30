using Books.GraphQLApi.Models;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Transports.AspNetCore.Common;
using GraphQL.SystemTextJson;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Books.GraphQLApi
{
	public class GraphQLUserContext : Dictionary<string, object>
	{
		public ClaimsPrincipal User { get; set; }
	}

	public static class StartupCongigurator
	{
		public static IServiceCollection ConfigureServices(IServiceCollection services, IWebHostEnvironment env)
		{
			services
				/*.AddSingleton<IDocumentExecuter, DocumentExecuter>()
				.AddSingleton<IDocumentWriter, DocumentWriter>()
				
				.AddTransient<BookType>()
				.AddTransient<AuthorType>()
				.AddTransient<AddBookType>()

				.AddScoped<AddBookRequest>()
				.AddScoped<BookQuery>()
				.AddScoped<BookMutation>()*/
				.AddScoped<BookSchema>()			
				.AddGraphQL(options =>
				{
					options.EnableMetrics = env.IsDevelopment();
					options.ExposeExceptions = env.IsDevelopment();
					options.UnhandledExceptionDelegate = ctx =>
					{
						Console.WriteLine(ctx.OriginalException);
					};
				})
				.AddUserContextBuilder(context =>
				{
					return new GraphQLUserContext { User = context.User };
				})
				.AddGraphTypes(ServiceLifetime.Scoped)
				.AddSystemTextJson()
				.AddGraphTypes(typeof(BookSchema))
				;

			return services;
		}

		public static void Configure(IApplicationBuilder app)
		{
			app.UseGraphQL<BookSchema>("/api/graphql"); // if not commented then GraphQlController not using

			//app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()); //to explorer API navigate https://*DOMAIN*/ui/playground           //... UseMVC ....
		}
	}
}
