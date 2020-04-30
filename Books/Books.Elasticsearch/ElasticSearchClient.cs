using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.Elasticsearch
{
	public interface IElasticSearchClient
	{ 
		Task Add<T>(T data);
		Task Update<T>(string id, T data);
		Task Delete<T>(string id);
	}

	public class ElasticSearchClient : IElasticSearchClient
	{
		private readonly string _indexUrl;
		private readonly IElasticsearchBookConfiguration _configuration;
		private readonly HttpClient _httpClient;
		private readonly ILogger<ElasticSearchClient> _logger;

		public ElasticSearchClient(HttpClient httpClient, IElasticsearchBookConfiguration configuration, ILogger<ElasticSearchClient> logger)
		{
			_configuration = configuration;
			_indexUrl = $"{_configuration.URL}/{_configuration.IndexName}";
			_httpClient = httpClient;
			_logger = logger;
			_logger.LogDebug($"elastic address: {_indexUrl}");

			/*Task.Run(async () =>
			{
				await CreateIndex();
			});*/
		}

		private async Task CreateIndex()
		{
			var indexSettings = new
			{
				settings = new
				{
					index = new
					{
						number_of_shards = 1,
						number_of_replicas = 0
					}					
				},
				mappings = new
				{
					properties = new
					{
						Title = new
						{
							type = "text"
						},
						Id = new
						{
							type = "long"
						},
						DatePublish = new
						{
							type = "date"
						}
					}
				}
			};

			var json = JsonSerializer.Serialize(indexSettings);
			var resp = await _httpClient.PutAsync(_indexUrl, new StringContent(json, Encoding.UTF8, "application/json"));
			await _processResponse(resp);
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

		public async Task Add<T>(T data)
		{
			var url = _indexUrl;
			var json = _serialize(data);
			_logger.LogDebug($"Add: {json}");

			var resp = await _httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

			await _processResponse(resp);
			if (resp.IsSuccessStatusCode)
			{ 
				// ...
			}
		}

		public async Task Delete<T>(string id)
		{
			var url = _indexUrl;
			var resp = await _httpClient.DeleteAsync(url);
			if (resp.IsSuccessStatusCode)
			{
				// ...
			}
		}

		public async Task Update<T>(string id, T data)
		{
			var url = _indexUrl;
			var json = _serialize(data);
			var resp = await _httpClient.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
			if (resp.IsSuccessStatusCode)
			{
				// ...
			}
		}
	}
}
