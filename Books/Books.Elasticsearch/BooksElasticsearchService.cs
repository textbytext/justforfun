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
		private readonly string _bookType;

		public BooksElasticsearchService(IElasticSearchClient elasticSearchClient, IElasticsearchBookConfiguration configuration)
		{
			_elasticSearchClient = elasticSearchClient;
			_bookType = configuration.BookType;
		}

		public async Task AddBook(BookDto book)
		{
			await _elasticSearchClient.Add(_bookType, book);
		}

		public Task<IEnumerable<long>> FindBookIdsByTitle(string title)
		{
			throw new NotImplementedException();
		}
	}
}
