using Books.Common.Events;
using Books.Core;
using Books.Core.Events;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.Elasticsearch
{
	public class BookUpdatedEventHandler : GenericEventSubscriber<BookUpdatedEvent>
	{
		private readonly ILogger<BookUpdatedEventHandler> _logger;
		private readonly IBooksSearchService _booksSearchService;

		public BookUpdatedEventHandler(ILogger<BookUpdatedEventHandler> logger, IBooksSearchService booksSearchService)
		{
			_logger = logger;
			_booksSearchService = booksSearchService;
		}

		public override Task Handle(BookUpdatedEvent evnt)
		{
			_logger.LogDebug("BookAddedEventHandler. " + JsonSerializer.Serialize(evnt));
			_booksSearchService.UpdateBook(evnt.Book);

			return Task.CompletedTask;
		}
	}
}
