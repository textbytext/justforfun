using Books.Core.Models;
using Books.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Core
{
	public interface IBooksRepository
	{
		Task<IEnumerable<Book>> GetBooks();

		Task<IEnumerable<AuthorDto>> GetAuthors();
		Task<IEnumerable<AuthorDto>> GetAuthors(IEnumerable<long> authors);

		Task<IEnumerable<Book>> GetAuthorBooks(long authorId);
		BookDto GetBookById(int id);
		Task<int> GetBooksCount();
		Task<IEnumerable<BookDto>> AddBooks(IEnumerable<BookDto> book);
		Task<BookDto> AddBook(BookDto book);
	}
}
