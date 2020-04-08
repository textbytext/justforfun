using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.Infrastructure.SQLite
{
	public class AuthorBookConfiguration : IEntityTypeConfiguration<AuthorBook>
	{
		public void Configure(EntityTypeBuilder<AuthorBook> builder)
		{
			builder.ToTable("AuthorBooks");

			builder.Property(ab => ab.AuthorId)
				.IsRequired();

			builder.Property(ab => ab.BookId)
				.IsRequired();

			builder.HasKey(ab => new { ab.AuthorId, ab.BookId });
			builder.HasIndex(ab => ab.AuthorId);
			builder.HasIndex(ab => ab.BookId);

			builder.HasOne(ab => ab.Author)
				.WithMany(a => a.AuthorBooks)
				.HasForeignKey(ab => ab.AuthorId);

			builder.HasOne(ab => ab.Book)
				.WithMany(a => a.AuthorBooks)
				.HasForeignKey(ab => ab.BookId);
		}
	}
}
