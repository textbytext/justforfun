using Books.Core;
using Books.Core.Models;
using GraphQL;
using GraphQL.Types;

namespace Books.WebApi.GraphQL
{
	public class AddBookRequest : InputObjectGraphType
	{
		public class Book
		{
			public int? Id { get; set; }
			public string Title { get; set; }
		}

		public AddBookRequest()
		{
			Name = "addBook";
			Field<NonNullGraphType<StringGraphType>>("title");
		}
	}

	public class AddBookType : ObjectGraphType<AddBookRequest.Book>
	{
		public AddBookType()
		{
			Field(x => x.Title).Description("Title of an book");
		}
	}

	public class BookMutation : ObjectGraphType<AddBookRequest>
	{
		public BookMutation(IBooksRepository repo)
		{
			Name = "Mutation";

			Field<AddBookType>(
				"addBook", // name of this mutation
				arguments: new QueryArguments(
					new QueryArgument<NonNullGraphType<AddBookRequest>>
					{
						Name = "book"
					}), // Arguments
				resolve: context =>
				{
					// Get Argument 
					var req = context.GetArgument<AddBookRequest.Book>("book");

					var book = new BookDto()
					{
						Title = req.Title
					};

					req.Id = repo.AddBook(book).Result;
					return req;
				}
			);
		}
	}
}
