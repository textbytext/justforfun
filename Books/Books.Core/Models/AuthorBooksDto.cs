using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Core.Models
{
	public class AuthorBooksDto
	{
		public AuthorDto Author { get; set; }
		public IEnumerable<BookDto> Books { get; set; }
	}
}
