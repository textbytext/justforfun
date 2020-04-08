using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Common.Models
{
	public class SingleResult<T> : ErrorResult
	{
		private readonly T _result;

		public T Result
		{
			get
			{
				return _result;
			}
		}

		public SingleResult(T result)
		{
			_result = result;
		}		
	}
}
