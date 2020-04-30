using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Domain.Entities
{
	public class AuthorBook
	{
		public long AuthorId { get; set; }
		public long BookId { get; set; }
		public virtual Book Book { get; set; }
		public virtual Author Author { get; set; }
	}
}
