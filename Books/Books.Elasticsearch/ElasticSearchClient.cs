using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.Elasticsearch
{
	public interface IElasticSearchClient
	{
		Task Add<T>(string typeName, T data);
		Task Update<T>(string typeName, string id, T data);
		Task Delete<T>(string typeName, string id);
	}

	public class ElasticSearchBookClient : IElasticSearchClient
	{
		private readonly string _indexUrl;
		private readonly IElasticsearchBookConfiguration _configuration;
		private readonly HttpClient _httpClient;
		private readonly ILogger<ElasticSearchBookClient> _logger;

		public ElasticSearchBookClient(HttpClient httpClient, IElasticsearchBookConfiguration configuration, ILogger<ElasticSearchBookClient> logger)
		{
			_configuration = configuration;
			_indexUrl = $"{_configuration.URL}/{_configuration.IndexName}";
			_httpClient = httpClient;
			_logger = logger;
			_logger.LogDebug($"elastic address: {_indexUrl}");
		}

		private string _serialize<T>(T obj)
		{
			return JsonSerializer.Serialize(obj);
		}

		private async Task _processResponse(HttpResponseMessage response)
		{
			var resp = await response.Content.ReadAsStringAsync();
			_logger.LogDebug($"Response: {resp}");
		}

		public async Task Add<T>(string typeName, T data)
		{
			var url = $"{_indexUrl}/{typeName}";
			var json = _serialize(data);
			_logger.LogDebug($"Add: {json}");

			var resp = await _httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

			await _processResponse(resp);
			if (resp.IsSuccessStatusCode)
			{
				// ...
			}
		}

		public async Task Delete<T>(string typeName, string id)
		{
			var url = $"{_indexUrl}/{typeName}";
			var resp = await _httpClient.DeleteAsync(url);
			await _processResponse(resp);
			if (resp.IsSuccessStatusCode)
			{
				// ...
			}
		}

		public async Task Update<T>(string typeName, string id, T data)
		{
			var url = $"{_indexUrl}/{typeName}/{id}";
			var json = _serialize(data);
			var resp = await _httpClient.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
			await _processResponse(resp);
			if (resp.IsSuccessStatusCode)
			{
				// ...
			}
		}

		/*
		 get http://localhost:9200/bookindex/books/_search?q=Id:66
		 */
	}
}
