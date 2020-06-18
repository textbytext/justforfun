using Books.Common.Events;
using Books.Common.Models;
using Books.Core.Events;
using Books.Core.Models;
using Books.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Books.Core.Books
{
	public class GetBookQuery : IRequest<SetResult<BookDto>>
	{
		public class Handler : IRequestHandler<GetBookQuery, SetResult<BookDto>>
		{
			private readonly IBooksRepository _booksRepository;
			private readonly IEventBus _eventBus;

			public Handler(IBooksRepository booksRepository, IEventBus eventBus)
			{
				_booksRepository = booksRepository;
				_eventBus = eventBus;
			}

			public async Task<SetResult<BookDto>> Handle(GetBookQuery request, CancellationToken cancellationToken)
			{
				var b = await _dbGetBooks();
				_eventBus.Publish(new GetBooksEvent());

				return new SetResult<BookDto>(_transformToDto(b));
			}

			private async Task<IEnumerable<Book>> _dbGetBooks()
			{
				return await _booksRepository.GetBooks();
			}

			private IEnumerable<BookDto> _transformToDto(IEnumerable<Book> result)
			{
				return result.Select(b => new BookDto()
				{
					Id = b.Id,
					Title = b.Title
				});
			}
		}
	}
}
