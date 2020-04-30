using Books.Domain.Entities;

namespace Books.Core.Models
{
	public class BookDto : IMapFrom<Book>
	{
		public long Id { get; set; }
		public string Title { get; set; }
	}
}
