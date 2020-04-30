using Books.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Elasticsearch
{
	public static class ElasticsearchStartupConfigurator
	{
		public static IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
		{
			var ecfg = configuration.GetSection("ElasticsearchBookConfiguration");			
			services.AddSingleton<IElasticsearchBookConfiguration>(ecfg.Get<ElasticsearchBookConfiguration>());

			services.AddTransient<IBooksSearchService, BooksElasticsearchService>();
			services.AddHttpClient<IElasticSearchClient, ElasticSearchClient>();
			return services;
		}
	}
}
