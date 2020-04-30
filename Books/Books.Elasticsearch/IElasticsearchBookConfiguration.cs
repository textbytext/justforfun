using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Elasticsearch
{
	public interface IElasticsearchBookConfiguration
	{
		string URL { get; }
		string IndexNamne { get; }
	}

	public class ElasticsearchBookConfiguration : IElasticsearchBookConfiguration
	{
		public string URL { get; set; }

		public string IndexNamne { get; set; }
	}
}
