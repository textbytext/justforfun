using Books.Core.Books;
using FluentValidation;

namespace Books.Core.Authors
{
	public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
	{
		public UpdateBookCommandValidator()
		{
			RuleFor(x => x.Id)
				.GreaterThan(0).WithMessage("Book ID must be!");

			RuleFor(x => x.Title)
				.NotEmpty().WithMessage("Book title must be!");
		}
	}
}
