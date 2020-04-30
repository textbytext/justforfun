using Books.Core.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.GraphQLApi.Models
{
	public class AuthorType: ObjectGraphType<AuthorDto>
    {
        public AuthorType()
        {
            Field(x => x.Id).Description("Id of an author");
            Field(x => x.Name).Description("Name of an author");
            Field<ListGraphType<BookType>>("books");
        }
    }
}
