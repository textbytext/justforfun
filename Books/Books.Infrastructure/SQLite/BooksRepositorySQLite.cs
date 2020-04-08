using Books.Core;
using Books.Domain;
using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Infrastructure.SQLite
{
	public class BooksRepositorySQLite : IBooksRepository
	{
		private readonly IBooksDbContext _context;

		public BooksRepositorySQLite()
		{
		}

		public BooksRepositorySQLite(IBooksDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Book>> GetAuthorBooks(long authorId)
		{
			var query = from a in _context.Authors
						join ab in _context.BookAuthors on a.Id equals ab.AuthorId
						where a.Id == authorId
						select ab.Book;

			return await query.AsNoTracking().ToListAsync();
		}

		public async Task<IEnumerable<Author>> GetAuthors()
		{
			return await _context.Authors.ToListAsync();
		}

		public async Task<IEnumerable<Book>> GetBooks()
		{
			return await _context.Books.ToListAsync();
		}
	}
}
