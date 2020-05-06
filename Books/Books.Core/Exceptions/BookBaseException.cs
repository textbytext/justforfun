using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Core.Exceptions
{
	public class BookBaseException : Exception
	{
		public BookBaseException()
		{
		}

		public BookBaseException(string message):base(message)
		{
		}
	}
}
