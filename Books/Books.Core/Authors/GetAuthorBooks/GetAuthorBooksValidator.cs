using FluentValidation;

namespace Books.Core.Authors
{
	public class GetAuthorBooksValidator : AbstractValidator<GetAuthorBooksQuery>
	{
		public GetAuthorBooksValidator()
		{
			RuleFor(x => x.Id)
				.GreaterThan(0).WithMessage("Author Id must be greater then 0 (zero).");
		}
	}
}
