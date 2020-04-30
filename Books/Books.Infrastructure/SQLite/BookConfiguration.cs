using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Infrastructure.SQLite
{
	public class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.ToTable("Books");

			builder.HasKey(b => b.Id);

			builder.Property(b => b.Id)		
				.IsRequired()
				.ValueGeneratedOnAdd();

			builder.HasMany(b => b.AuthorBooks)
				.WithOne(ab => ab.Book)
				.HasForeignKey(ab => ab.BookId);
		}
	}
}
