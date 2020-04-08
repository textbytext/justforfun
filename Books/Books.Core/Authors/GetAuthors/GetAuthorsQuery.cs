using Books.Common.Models;
using Books.Core.Models;
using Books.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Books.Core.Books.GetBooks
{
	public class GetAuthorsQuery : IRequest<SetResult<AuthorDto>>
	{
		public class Handler : IRequestHandler<GetAuthorsQuery, SetResult<AuthorDto>>
		{
			private readonly IBooksRepository _booksRepository;

			public Handler(IBooksRepository booksRepository)
			{
				_booksRepository = booksRepository;
			}

			public async Task<SetResult<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
			{
				var b = await _dbGetAuthors();
				return new SetResult<AuthorDto>(_transformToDto(b));
			}

			private async Task<IEnumerable<Author>> _dbGetAuthors()
			{
				return await _booksRepository.GetAuthors();
			}

			private IEnumerable<AuthorDto> _transformToDto(IEnumerable<Author> result)
			{
				return result.Select(b => new AuthorDto()
				{
					Id = b.Id,
					Name = b.Name
				});
			}
		}
	}
}
