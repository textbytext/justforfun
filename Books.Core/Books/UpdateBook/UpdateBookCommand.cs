using Books.Common.Events;
using Books.Common.Models;
using Books.Core.Events;
using Books.Core.Exceptions;
using Books.Core.Models;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Books.Core.Books
{	
	public class UpdateBookCommand : IRequest<SingleResult<BookDto>>
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public DateTime DatePublish { get; set; }
		public IEnumerable<long> Authors { get; set; }

		public class Handler : IRequestHandler<UpdateBookCommand, SingleResult<BookDto>>
		{
			private readonly IBooksRepository _booksRepository;
			private readonly IEventBus _eventBus;

			public Handler(IBooksRepository booksRepository, IEventBus eventBus)
			{
				_booksRepository = booksRepository;
				_eventBus = eventBus;
			}

			public async Task<SingleResult<BookDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
			{
				var authorsIds = request.Authors ?? new long[] { };
				if (authorsIds.Count() > 0)
				{
					var authors = await _booksRepository.GetAuthors(authorsIds);
					if (authors.Count() != authorsIds.Count())
					{
						var notfound = authorsIds.Where(a => !authors.Any(au => au.Id == a)).ToArray();
						throw new AuthorNotFoundException(notfound);
					}
				}

				var bookDto = new BookDto
				{
					Id = request.Id,
					Title = request.Title,
					DatePublish = request.DatePublish
				};

				var result = await _booksRepository.UpdateBook(bookDto);
				
				_eventBus.Publish(new BookUpdatedEvent()
				{
					Book = result
				});

				return new SingleResult<BookDto>(result);
			}
		}
	}
}
