using AutoMapper;
using Books.Common.Models;
using Books.Core.Models;
using Books.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Books.Core.Books.GetBooksByAuthor
{
	public class GetAuthorBooksQuery : IRequest<SetResult<BookDto>>
	{
		public long Id { get; set; }

		public class Handler : IRequestHandler<GetAuthorBooksQuery, SetResult<BookDto>>
		{
			private readonly IBooksRepository _booksRepository;
			private readonly IMapper _mapper;

			public Handler(IBooksRepository booksRepository, IMapper mapper)
			{
				_booksRepository = booksRepository;
				_mapper = mapper;
			}

			public async Task<SetResult<BookDto>> Handle(GetAuthorBooksQuery request, CancellationToken cancellationToken)
			{
				var data = await _booksRepository.GetAuthorBooks(request.Id);
				return await Task.FromResult(_transform(data));
			}

			private SetResult<BookDto> _transform(IEnumerable<Book> books)
			{
				var bks = books.Select(_mapper.Map<BookDto>);
				return new SetResult<BookDto>(bks);
			}
		}
	}
}
