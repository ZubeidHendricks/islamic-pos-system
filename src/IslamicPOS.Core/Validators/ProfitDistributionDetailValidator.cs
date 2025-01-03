using FluentValidation;
using IslamicPOS.Core.DTOs;

namespace IslamicPOS.Core.Validators;

public class ProfitDistributionDetailValidator : AbstractValidator<ProfitDistributionDetailDto>
{
    public ProfitDistributionDetailValidator()
    {
        RuleFor(x => x.PartnerId)
            .NotEmpty()
            .WithMessage("Partner ID is required");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0");

        RuleFor(x => x.Percentage)
            .InclusiveBetween(0, 100)
            .WithMessage("Percentage must be between 0 and 100");

        RuleFor(x => x.PaymentReference)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.PaymentReference))
            .WithMessage("Payment reference cannot exceed 100 characters");
    }
}