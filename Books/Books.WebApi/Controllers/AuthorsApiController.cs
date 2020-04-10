using Books.Common.Models;
using Books.Core;
using Books.Core.Authors;
using Books.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Books.WebApi.Controllers
{
	[ApiController]
	[Route("api/author")]
	public class AuthorsApiController : ControllerBase
	{
		private readonly ILogger<AuthorsApiController> _logger;
		private readonly IMediator _mediator;

		public AuthorsApiController(ILogger<AuthorsApiController> logger, IMediator mediator)
		{
			_logger = logger;
			_mediator = mediator;
		}

		[HttpGet("all")]
		public async Task<SetResult<AuthorDto>> GetAuthors()
		{
			var cmd = new GetAuthorsQuery();
			return await _mediator.Send(cmd);
		}

		[HttpGet]
		[Route("{id:long}/books")]
		public async Task<SetResult<BookDto>> GetAuthorBooks([FromRoute] long id)
		{
			var cmd = new GetAuthorBooksQuery()
			{
				Id = id
			};
			return await _mediator.Send(cmd);
		}
	}
}
