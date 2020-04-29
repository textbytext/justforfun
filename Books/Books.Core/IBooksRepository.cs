using Books.Core.Models;
using Books.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Core
{
	public interface IBooksRepository
	{
		Task<IEnumerable<Book>> GetBooks();

		Task<IEnumerable<Author>> GetAuthors();

		Task<IEnumerable<Book>> GetAuthorBooks(long authorId);
		BookDto GetBookById(int id);
		Task<int> GetBooksCount();
		Task<int> AddBooks(IEnumerable<BookDto> book);
		Task<int> AddBook(BookDto book);
	}
}
