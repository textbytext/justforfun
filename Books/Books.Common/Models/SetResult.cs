using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Books.Common.Models
{
	public class SetResult<T>: ErrorResult
	{
		private readonly IEnumerable<T> _result;
		private readonly int _count;
		private readonly int _page;
		private readonly int _pages;		
		private readonly int _pageSize;

		public IEnumerable<T> Result => _result;
		public int Count => _count;
		public int Page => _page;
		public int Pages => _pages;
		public int PageSize => _pageSize;
		public bool HasData => Count > 0;

		public SetResult(IEnumerable<T> result)
		{
			_result = result;
			_page = result?.Count() ?? 0;
			_pages = result?.Count() ?? 0;
			_pageSize = result?.Count() ?? 0;
			_count = result?.Count() ?? 0;
		}

		public SetResult(IEnumerable<T> result, int page, int pages, int pageSize)
		{
			_result = result;
			_page = page;
			_pages = pages;
			_pageSize = pageSize;
			_count = result?.Count() ?? 0;
		}
		
	}
}
