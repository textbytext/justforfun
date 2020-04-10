using Books.Common.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.Infrastructure.Events
{
	public class EventBus : IEventBus
	{
		private readonly IServiceProvider _services;
		private readonly ILogger<EventBus> _logger;

		public EventBus(IServiceProvider services, ILogger<EventBus> logger)
		{
			_services = services;
			_logger = logger;
		}

		public void Publish<T>(in T evnt) where T : BaseEvent
		{
			_logger.LogDebug($"Send. T: {typeof(T).FullName}, evnt: {JsonSerializer.Serialize(evnt)}");

			var handlers = _services
				.GetServices<GenericEventSubscriber<T>>()
				.ToList();

			if (null != handlers && handlers.Count() > 0)
			{
				foreach (var handler in handlers)
				{
					try
					{
						handler.Handle(evnt);	
					}
					catch (Exception e)
					{
					}
				}
			}
		}
	}
}
