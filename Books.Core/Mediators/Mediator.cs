using System.Threading;
using System.Threading.Tasks;

namespace Books.Core
{
	public class Mediator : IMediator
	{
		private readonly MediatR.IMediator _mediatr;

		public Mediator(MediatR.IMediator mediatr)
		{
			_mediatr = mediatr;
		}

		public Task Publish(object notification, CancellationToken cancellationToken = default)
		{
			return _mediatr.Publish(notification, cancellationToken);
		}

		public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : MediatR.INotification
		{
			return _mediatr.Publish(notification, cancellationToken);
		}

		public Task<TResponse> Send<TResponse>(MediatR.IRequest<TResponse> request, CancellationToken cancellationToken = default)
		{
			return _mediatr.Send(request, cancellationToken);
		}

		public Task<object> Send(object request, CancellationToken cancellationToken = default)
		{
			return _mediatr.Send(request, cancellationToken);
		}
	}
}
