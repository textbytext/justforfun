namespace Books.Core
{
	public interface IRequestHandler<in TRequest, TResponse> : MediatR.IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
	}
}
