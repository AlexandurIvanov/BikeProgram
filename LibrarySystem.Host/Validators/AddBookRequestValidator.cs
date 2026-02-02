using FluentValidation;
using LibrarySystem.Models.Requests;

namespace LibrarySystem.Host.Validators
{
    public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.AuthorId)
                .NotEmpty().WithMessage("AuthorId is required.");
        }
    }
}