using Books.Common.Events;
using Books.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.Core.Events
{
	public class BookUpdatedEvent : BaseEvent
	{
		public BookDto Book { get; set; }

		public class Handler : GenericEventSubscriber<BookUpdatedEvent>
		{
			private readonly ILogger<Handler> _logger;

			public Handler(ILogger<Handler> logger)
			{
				_logger = logger;
			}

			public override Task Handle(BookUpdatedEvent evnt)
			{
				_logger.LogDebug(JsonSerializer.Serialize(evnt));
				return Task.CompletedTask;
			}
		}
	}
}
