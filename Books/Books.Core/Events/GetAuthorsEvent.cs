using Books.Common.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.Core.Events
{
	public class GetAuthorsEvent : BaseEvent
	{
		public class Handler : GenericEventSubscriber<GetAuthorsEvent>
		{
			private readonly ILogger<Handler> _logger;

			public Handler(ILogger<Handler> logger)
			{
				_logger = logger;
			}

			public override Task Handle(GetAuthorsEvent evnt)
			{
				_logger.LogDebug("Handle. " + JsonSerializer.Serialize(evnt));
				return Task.CompletedTask;
			}
		}
	}
}
