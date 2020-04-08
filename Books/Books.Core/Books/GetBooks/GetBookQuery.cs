using Books.Common.Models;
using Books.Core.Models;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Books.Core.Books.GetBooks
{
	public class GetBookQuery: IRequest<SetResult<BookDto>>
	{
		public class Handler : IRequestHandler<GetBookQuery, SetResult<BookDto>>
		{
			private readonly IBooksRepository _booksRepository;

			public Handler(IBooksRepository booksRepository)
			{
				_booksRepository = booksRepository;
			}

			public async Task<SetResult<BookDto>> Handle(GetBookQuery request, CancellationToken cancellationToken)
			{
				var b = await _dbGetBooks();
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
