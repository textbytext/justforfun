using Microsoft.AspNetCore.Http;
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
		public ElasticSearchClient(HttpClient httpClient, IElasticsearchBookConfiguration configuration)
		{
			_configuration = configuration;
			_indexUrl = $"{_configuration.URL}/{_configuration.IndexNamne}";
			_httpClient = httpClient;
		}

		private string _serialize<T>(T obj)
		{
			return JsonSerializer.Serialize(obj);
		}

		public async Task Add<T>(T data)
		{
			var url = _indexUrl;
			var json = _serialize(data);
			var resp = await _httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
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
