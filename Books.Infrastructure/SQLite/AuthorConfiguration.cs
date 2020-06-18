using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Infrastructure.SQLite
{
	public class AuthorConfiguration : IEntityTypeConfiguration<Author>
	{
		public void Configure(EntityTypeBuilder<Author> builder)
		{
			builder.ToTable("Authors");

			builder.HasKey(a => a.Id);

			builder.Property(a => a.Id)				
				.ValueGeneratedOnAdd();

			builder.HasMany(a => a.AuthorBooks)
				.WithOne(ab => ab.Author)
				.HasForeignKey(ab => ab.AuthorId);
		}
	}
}
