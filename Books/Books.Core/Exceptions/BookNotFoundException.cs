using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Core.Exceptions
{
	public class BookNotFoundException : BookBaseException
	{
		public readonly long Id;
		public BookNotFoundException(long id)
			:base("Book with id: " + string.Join(",", id) + " not found!")
		{
			Id = id;
		}
	}
}
