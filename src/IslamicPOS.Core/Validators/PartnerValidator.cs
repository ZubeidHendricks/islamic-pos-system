using FluentValidation;
using IslamicPOS.Core.DTOs;

namespace IslamicPOS.Core.Validators;

public class PartnerValidator : AbstractValidator<PartnerDto>
{
    public PartnerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Partner name is required and cannot exceed 100 characters");

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Invalid email format");

        RuleFor(x => x.SharePercentage)
            .InclusiveBetween(0, 100)
            .WithMessage("Share percentage must be between 0 and 100");

        RuleFor(x => x.ContactNumber)
            .MaximumLength(20)
            .When(x => !string.IsNullOrEmpty(x.ContactNumber))
            .WithMessage("Contact number cannot exceed 20 characters");
    }
}