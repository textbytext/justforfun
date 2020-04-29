using Books.Core;
using Books.WebApi.GraphQL;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.WebApi.Controllers
{
	[ApiController]
	[Route("graphql")]
	public class GraphQlController : ControllerBase
	{
		private readonly IBooksRepository _booksRepository;
		private readonly ILogger<GraphQlController> _logger;

		public GraphQlController(IBooksRepository booksRepository, ILogger<GraphQlController> logger)
		{
			_booksRepository = booksRepository;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] GraphQlQuery query)
		{
			_logger.LogDebug($"Post: {JsonSerializer.Serialize(query)}");

			var schema = new Schema { Query = new BookQuery(_booksRepository) };

			var result = await new DocumentExecuter().ExecuteAsync(x =>
			{
				x.Schema = schema;
				x.Query = query.Query;
				x.Inputs = query.Variables;
				x.EnableMetrics = false;				
			});

			if (result.Errors?.Count > 0)
			{
				return BadRequest();
			}

			//result.Document = null;
			//result.Operation = null;
			//result.Query = null;

			return Ok(result);
		}

		/*public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            if (query == null) { throw new ArgumentNullException(nameof(query)); }
            var inputs = query.Variables.ToInputs();
            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query,
                Inputs = inputs
            };

            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }*/
	}
}