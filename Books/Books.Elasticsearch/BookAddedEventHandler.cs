using Books.Common.Events;
using Books.Core.Events;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.Elasticsearch
{
	public class BookAddedEventHandler : GenericEventSubscriber<BookAddedEvent>
	{
		private readonly ILogger<BookAddedEventHandler> _logger;
		private readonly IElasticSearchClient _elasticSearchClient;

		public BookAddedEventHandler(ILogger<BookAddedEventHandler> logger, IElasticSearchClient elasticSearchClient)
		{
			_logger = logger;
			_elasticSearchClient = elasticSearchClient;
		}

		public override Task Handle(BookAddedEvent evnt)
		{
			_logger.LogDebug("BookAddedEventHandler. " + JsonSerializer.Serialize(evnt));

			_elasticSearchClient.Add(evnt.Book);

			return Task.CompletedTask;
		}
	}
}
