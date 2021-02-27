using System.Collections.Generic;

namespace Books.Domain.Entities
{
	public class Author
	{
		public Author()
		{
			AuthorBooks = new HashSet<AuthorBook>();
		}

		public long Id { get; set; }
		public string Name { get; set; }
		public ICollection<AuthorBook> AuthorBooks { get; set; }
	}
}
