namespace Books.Core
{
	public interface IRequest<out TResponse> : MediatR.IRequest<TResponse>
	{
	}
}
