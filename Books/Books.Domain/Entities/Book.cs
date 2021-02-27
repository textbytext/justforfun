using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Domain.Entities
{
	public class Book
	{
		public Book()
		{
			AuthorBooks = new HashSet<AuthorBook>();
		}

		public long Id { get; set; }
		public string Title { get; set; }
		public DateTime DatePublish { get; set; } = DateTime.Now;
		public ICollection<AuthorBook> AuthorBooks { get; set; }
	}
}
