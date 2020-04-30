using Books.Core;
using Books.Core.Books;
using Books.Core.Models;
using GraphQL;
using GraphQL.Types;
using System;

namespace Books.GraphQLApi.Models
{
	public class AddBookRequest : InputObjectGraphType
	{
		public class Book
		{
			public long Id { get; set; }
			public string Title { get; set; }
			public DateTime DatePublish { get; set; }
			public long[] Authors { get; set; }
		}

		public AddBookRequest()
		{
			Name = "addBook";
			Field<LongGraphType>("id");
			Field<NonNullGraphType<StringGraphType>>("title");
			Field<NonNullGraphType<DateTimeGraphType>>("datePublish");
			Field<ListGraphType<LongGraphType>>("authors");
		}
	}

	public class AddBookType : ObjectGraphType<AddBookRequest.Book>
	{
		public AddBookType()
		{
			Field(x => x.Id).Description("Id of a book");
			Field(x => x.Title).Description("Title of a book");
			Field(x => x.DatePublish).Description("DatePublish of a book");
			Field(x => x.Authors).Description("Author ids of a book");

			IsTypeOf = obj =>
			{
				var type = obj.GetType();
				if (type == typeof(BookDto))
				{
					return true;
				}

				if (type == typeof(AddBookRequest.Book))
				{
					return true;
				}
				return false;
			};
		}
	}

	public class BookMutation : ObjectGraphType<AddBookRequest>
	{
		public BookMutation(IMediator mediator)
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

					var cmd = new AddBookCommand()
					{
						Title = req.Title,
						DatePublish = req.DatePublish
					};

					var book = mediator.Send(cmd).Result.Result;

					req.Id = book.Id;
					return req;
				}
			);
		}
	}
}
