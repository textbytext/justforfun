using Books.Domain.Entities;
using System;

namespace Books.Core.Models
{
	public class BookDto : IMapFrom<Book>
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public DateTime DatePublish { get; set; }
	}
}
