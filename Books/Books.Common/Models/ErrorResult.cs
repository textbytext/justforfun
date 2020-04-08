using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Books.Common.Models
{
	public class ErrorResult
	{
		private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

		public ImmutableDictionary<string, string[]> Errors
		{
			get
			{
				if (_errors.Count == 0)
				{
					return null;
				}

				return _errors.ToImmutableDictionary(i => i.Key, i => i.Value.ToArray());
			}
		}

		public bool HasErrors
		{
			get
			{
				return _errors.Count > 0;
			}
		}

		public void SetError(string category, string message)
		{
			_setError(category, message);
		}

		private void _setError(string category, string message)
		{
			category = category ?? throw new ArgumentNullException(nameof(category));
			message = message ?? throw new ArgumentNullException(nameof(message));

			if (_errors.ContainsKey(category))
			{
				_errors[category].Add(message);
			}
			else
			{
				_errors[category] = new List<string>() { message };
			}
		}

		public void SetSummaryError(string message)
		{
			_setError("_summary", message);
		}
	}
}
