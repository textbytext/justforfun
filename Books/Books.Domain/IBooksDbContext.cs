using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Books.Domain
{
	public interface IBooksDbContext: IDisposable
	{
		public DbSet<Book> Books { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<AuthorBook> BookAuthors { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
