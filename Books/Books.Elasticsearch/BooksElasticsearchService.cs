using Books.Core;
using Books.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Books.Elasticsearch
{
	public class BooksElasticsearchService : IBooksSearchService
	{
		private readonly IElasticSearchClient _elasticSearchClient;

		public BooksElasticsearchService(IElasticSearchClient elasticSearchClient)
		{
			_elasticSearchClient = elasticSearchClient;
		}

		public async Task AddBook(BookDto book)
		{
			await _elasticSearchClient.Add(book);
		}

		public Task<IEnumerable<long>> FindBookIdsByTitle(string title)
		{
			throw new NotImplementedException();
		}
	}
}
