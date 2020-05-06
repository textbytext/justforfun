using Books.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core
{
	public interface IBooksSearchService
	{
		Task<IEnumerable<long>> FindBookIdsByTitle(string title);
		Task AddBook(BookDto book);
		Task UpdateBook(BookDto book);
	}
}
