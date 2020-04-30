using Books.Core.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.GraphQLApi.Models
{
	public class BookType: ObjectGraphType<BookDto>
    {
        public BookType()
        {
            Field(x => x.Id).Description("Id of an book");
            Field(x => x.Title).Description("Title of an book");
            Field<ListGraphType<AuthorType>>("authors");
        }
    }
}
