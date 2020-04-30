using Books.Domain.Entities;

namespace Books.Core.Models
{
	public class AuthorDto : IMapFrom<Author>
	{
		public long Id { get; set; }
		public string Name { get; set; }
	}
}