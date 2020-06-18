using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.GraphQLApi.Models
{
	public class BookSchema : Schema
	{
		public BookSchema(IServiceProvider resolver) : base(resolver)
		{
			Query = resolver.GetService(typeof(BookQuery)) as BookQuery;
			Mutation = resolver.GetService(typeof(BookMutation)) as BookMutation;
		}
	}
}
