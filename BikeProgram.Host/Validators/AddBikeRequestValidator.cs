using FluentValidation;
using BikeProgram.Models.Requests;

namespace BikeProgram.Host.Validators
{
    public class AddBikeRequestValidator : AbstractValidator<AddBikeRequest>
    {
        public AddBikeRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.ManufacturerId)
                .NotEmpty().WithMessage("ManufacturerId is required.");
        }
    }
}