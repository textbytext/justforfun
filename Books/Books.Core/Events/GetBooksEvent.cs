using Books.Common.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.Core.Events
{
	public class GetBooksEvent : BaseEvent
	{
		public class Handler : GenericEventSubscriber<GetBooksEvent>
		{
			private readonly ILogger<Handler> _logger;

			public Handler(ILogger<Handler> logger)
			{
				_logger = logger;
			}

			public override Task Handle(GetBooksEvent evnt)
			{
				_logger.LogDebug(JsonSerializer.Serialize(evnt));
				return Task.CompletedTask;
			}
		}
	}
}
