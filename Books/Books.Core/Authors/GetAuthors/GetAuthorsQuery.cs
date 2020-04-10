using Books.Common.Events;
using Books.Common.Models;
using Books.Core.Events;
using Books.Core.Models;
using Books.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Books.Core.Authors
{
	public class GetAuthorsQuery : IRequest<SetResult<AuthorDto>>
	{
		public class Handler : IRequestHandler<GetAuthorsQuery, SetResult<AuthorDto>>
		{
			private readonly IBooksRepository _booksRepository;
			private readonly IEventBus _eventBus;

			public Handler(IBooksRepository booksRepository, IEventBus eventBus)
			{
				_booksRepository = booksRepository;
				_eventBus = eventBus;
			}

			public async Task<SetResult<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
			{
				var b = await _dbGetAuthors();
				_eventBus.Publish(new GetAuthorsEvent());
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
