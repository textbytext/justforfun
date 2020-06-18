using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Core.Models
{
	public class BookAuthorsDto
	{
		public BookDto Book { get; set; }
		public IEnumerable<AuthorDto> Authors { get; set; }		
	}
}
