﻿using Books.Core;
using Books.Core.Models;
using Books.Domain;
using Books.Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

		public async Task<int> GetBooksCount()
		{
			return await _context.Books.CountAsync();
		}

		public BookDto GetBookById(int id)
		{
			return _context
				.Books
				.Where(b => b.Id == id)
				.Select(_toBookDto)
				.FirstOrDefault();
		}

		public async Task<int> AddBooks(IEnumerable<BookDto> books)
		{
			return await _addBooks(books);
		}

		private async Task<int> _addBooks(IEnumerable<BookDto> books)
		{
			var db = books.Select(b => new Book()
			{
				Title = b.Title,
				DatePublish = DateTime.Now
			});

			await _context.Books.AddRangeAsync(db);
			return await _context.SaveChangesAsync();
		}

		public async Task<int> AddBook(BookDto book)
		{
			return await _addBooks(new[] { book });
		}

		private Expression<Func<Book, BookDto>> _toBookDto
		{
			get
			{
				return x => new BookDto()
				{
					Id = x.Id,
					Title = x.Title
				};
			}
		}
	}
}