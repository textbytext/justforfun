using Books.Common.Events;
using Books.Core;
using Books.Core.Events;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.Elasticsearch
{
	public class BookAddedEventHandler : GenericEventSubscriber<BookAddedEvent>
	{
		private readonly ILogger<BookAddedEventHandler> _logger;
		private readonly IBooksSearchService _booksSearchService;

		public BookAddedEventHandler(ILogger<BookAddedEventHandler> logger, IBooksSearchService booksSearchService)
		{
			_logger = logger;
			_booksSearchService = booksSearchService;
		}

		public override Task Handle(BookAddedEvent evnt)
		{
			_logger.LogDebug("BookAddedEventHandler. " + JsonSerializer.Serialize(evnt));

			_booksSearchService.AddBook(evnt.Book);

			return Task.CompletedTask;
		}
	}
}
