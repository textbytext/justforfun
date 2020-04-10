using Books.Common.Models;
using Books.Core;
using Books.Core.Books;
using Books.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Books.WebApi.Controllers
{
	[ApiController]
	[Route("api/book")]
	public class BooksApiController : ControllerBase
	{
		private readonly ILogger<BooksApiController> _logger;
		private readonly IMediator _mediator;

		public BooksApiController(ILogger<BooksApiController> logger, IMediator mediator)
		{
			_logger = logger;
			_mediator = mediator;
		}

		[HttpGet("all")]
		public async Task<SetResult<BookDto>> GetBooks()
		{
			var cmd = new GetBookQuery();
			return await _mediator.Send(cmd);
		}
	}
}
