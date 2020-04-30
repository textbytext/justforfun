using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Elasticsearch
{
	public interface IElasticsearchBookConfiguration
	{
		string URL { get; }
		string IndexName { get; }
	}

	public class ElasticsearchBookConfiguration : IElasticsearchBookConfiguration
	{
		public string URL { get; set; }

		public string IndexName { get; set; }
	}
}
