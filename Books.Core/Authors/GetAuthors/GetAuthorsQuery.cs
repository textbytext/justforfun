using Books.Common.Events;
using Books.Common.Models;
using Books.Core.Events;
using Books.Core.Models;
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
				var authors = await _booksRepository.GetAuthors();

				_eventBus.Publish(new GetAuthorsEvent());

				return new SetResult<AuthorDto>(authors);
			}
		}
	}
}
