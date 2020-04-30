using System.Threading.Tasks;

namespace Books.Common.Events
{
	public abstract class BaseEventSubscriber
	{
	}

	public abstract class GenericEventSubscriber<T> : BaseEventSubscriber where T : BaseEvent
	{
		public abstract Task Handle(T evnt);
	}
}
