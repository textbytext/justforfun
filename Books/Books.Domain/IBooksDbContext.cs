using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Domain
{
	public interface IBooksDbContext: IDisposable
	{
		public DbSet<Book> Books { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<AuthorBook> BookAuthors { get; set; }
	}
}
