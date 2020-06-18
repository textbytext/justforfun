using System;
using System.Collections;
using System.Collections.Generic;

namespace Books.WebApi.Models
{
	public class UpdateBookRequest
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public DateTime DatePublish { get; set; }
		public IEnumerable<long> Authors { get; set; }
	}
}
