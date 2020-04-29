using Books.Core;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.WebApi.GraphQL
{
	public class BookQuery : ObjectGraphType
    {
        public BookQuery(IBooksRepository repos)
        {
            Field<BookType>(
                name: "book",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return repos.GetBookById(id);
                }
            );
        }
    }
}
