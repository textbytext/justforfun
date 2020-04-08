using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Books.Core
{
	public interface IRequest<out TResponse> : MediatR.IRequest<TResponse>
	{
	}

	public interface IRequestHandler<in TRequest, TResponse> : MediatR.IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
	}

	public interface IMediator: MediatR.IMediator
	{		
	}

	public class Mediator : IMediator
	{
		private readonly  MediatR.IMediator _mediatr;

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

	/// <summary>
	/// Intersepter
	/// </summary>
	/// <typeparam name="TRequest"></typeparam>
	/// <typeparam name="TResponse"></typeparam>
	public class RequestValidationBehavior<TRequest, TResponse> : MediatR.IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;
		private readonly ILogger<RequestValidationBehavior<TRequest, TResponse>> _logger;

		public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<RequestValidationBehavior<TRequest, TResponse>> logger)
		{
			_validators = validators;
			_logger = logger;
		}

		public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, MediatR.RequestHandlerDelegate<TResponse> next)
		{
			var context = new ValidationContext(request);

			_logger.LogDebug(typeof(TRequest).FullName);

			var failures = _validators
				.Select(v => v.Validate(context))
				.SelectMany(result => result.Errors)
				.Where(f => f != null)
				.ToList();

			if (failures.Count != 0)
			{
				throw new ValidationException(failures);
			}

			return next();
		}

	}
}
