using System.Threading.Tasks;

namespace Books.Common.Events
{
	public delegate void GenericEventAction<T>(T evnt) where T : BaseEvent;

	public interface IEventBus
	{
		void Publish<T>(in T evnt) where T : BaseEvent;
	}
}
