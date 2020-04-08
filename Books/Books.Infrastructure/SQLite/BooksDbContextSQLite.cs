using Books.Domain;
using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Books.Infrastructure.SQLite
{
	public class SQLiteBooksDbContext : DbContext, IBooksDbContext
	{
		public DbSet<Book> Books { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<AuthorBook> BookAuthors { get; set; }

		public SQLiteBooksDbContext(DbContextOptions<SQLiteBooksDbContext> options) : base(options)
		{ 
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}
	}
}
