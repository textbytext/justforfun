using Books.Core.Books;
using FluentValidation;

namespace Books.Core.Authors
{
	public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
	{
		public AddBookCommandValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty().WithMessage("Book title must be!");
		}
	}
}
