using Books.Core;
using Books.Core.Exceptions;
using Books.Core.Models;
using Books.Domain;
using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Books.Infrastructure.SQLite
{
	public class BooksRepositorySQLite : IBooksRepository
	{
		private readonly IBooksDbContext _context;
		private readonly ILogger<BooksRepositorySQLite> _logger;

		public BooksRepositorySQLite(ILogger<BooksRepositorySQLite> logger)
		{
			_logger = logger;
		}

		public BooksRepositorySQLite(IBooksDbContext context, ILogger<BooksRepositorySQLite> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<IEnumerable<Book>> GetAuthorBooks(long authorId)
		{
			var query = from a in _context.Authors
						join ab in _context.BookAuthors on a.Id equals ab.AuthorId
						where a.Id == authorId
						select ab.Book;

			return await query.AsNoTracking().ToListAsync();
		}

		public async Task<IEnumerable<AuthorDto>> GetAuthors()
		{
			return await _context
				.Authors
				.Select(_toAuthorDto)
				.ToListAsync();
		}

		public async Task<IEnumerable<Book>> GetBooks()
		{
			return await _context.Books.ToListAsync();
		}

		public async Task<int> GetBooksCount()
		{
			return await _context.Books.CountAsync();
		}

		public BookDto GetBookById(long id)
		{
			return _context
				.Books
				.Where(b => b.Id == id)
				.Select(_toBookDto)
				.FirstOrDefault();
		}

		public async Task<IEnumerable<BookDto>> AddBooks(IEnumerable<BookDto> books)
		{
			return await _addBooks(books);
		}

		private async Task<IEnumerable<BookDto>> _addBooks(IEnumerable<BookDto> books)
		{
			var db = books.Select(b => new Book()
			{
				Title = b.Title,
				DatePublish = b.DatePublish
			}).ToList();

			await _context.Books.AddRangeAsync(db);
			await _context.SaveChangesAsync();

			return db
				.AsQueryable()
				.Select(_toBookDto)
				.ToList();
		}

		public async Task<BookDto> AddBook(BookDto book)
		{
			var result = await _addBooks(new[] { book });
			return result.FirstOrDefault();
		}

		public async Task<BookDto> UpdateBook(BookDto bookDto)
		{
			var book = _context
				.Books
				.Where(b => b.Id == bookDto.Id)
				.FirstOrDefault();

			if (null == book)
			{
				throw new BookNotFoundException(bookDto.Id);
			}

			book.Title = bookDto.Title;
			await _context.SaveChangesAsync();

			return _toBookDto.Compile()(book);
		}

		public async Task<IEnumerable<AuthorDto>> GetAuthors(IEnumerable<long> authors)
		{
			if (null == authors || authors.Count() == 0)
			{
				return new List<AuthorDto>();
			}

			var query = from a in _context.Authors
						where authors.Contains(a.Id)
						select a;

			return await query
				.Select(_toAuthorDto)
				.ToListAsync() ?? new List<AuthorDto>();
		}

		private bool _disposed = false;
		public void Dispose()
		{
			_logger.LogDebug("Dispose");
			if (!_disposed)
			{
				//throw new Exception("Dispose exception");
				//_context.SaveChanges();
			}
			_disposed = true;			
		}

		private Expression<Func<Book, BookDto>> _toBookDto
		{
			get
			{
				return x => new BookDto()
				{
					Id = x.Id,
					Title = x.Title,
					DatePublish = x.DatePublish
				};
			}
		}

		private Expression<Func<Author, AuthorDto>> _toAuthorDto
		{
			get
			{
				return x => new AuthorDto()
				{
					Id = x.Id,
					Name = x.Name
				};
			}
		}
	}
}
