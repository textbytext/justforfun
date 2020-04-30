using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Core.Exceptions
{
	public class AuthorNotFoundException: Exception
	{
		public readonly IEnumerable<long> Authors;
		public AuthorNotFoundException(IEnumerable<long> ids)
			:base("Authors with ids: " + string.Join(",", ids) + " not found!")
		{
			Authors = ids;
		}
	}
}
