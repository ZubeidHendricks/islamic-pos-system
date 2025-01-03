using FluentValidation;
using IslamicPOS.Core.DTOs;

namespace IslamicPOS.Core.Validators;

public class ProfitSharingValidator : AbstractValidator<ProfitSharingDto>
{
    public ProfitSharingValidator()
    {
        RuleFor(x => x.TotalAmount)
            .GreaterThan(0)
            .WithMessage("Total amount must be greater than 0");

        RuleFor(x => x.Period)
            .NotEmpty()
            .Matches(@"^\d{4}-(0[1-9]|1[0-2])$")
            .WithMessage("Period must be in format YYYY-MM");

        RuleFor(x => x.DistributionDate)
            .NotEmpty()
            .WithMessage("Distribution date is required");

        RuleFor(x => x.DistributionDetails)
            .NotEmpty()
            .WithMessage("At least one distribution detail is required");

        RuleForEach(x => x.DistributionDetails)
            .SetValidator(new ProfitDistributionDetailValidator());

        RuleFor(x => x.DistributionDetails)
            .Must(details => details.Sum(d => d.Percentage) == 100)
            .WithMessage("Total percentage must equal 100%");
    }
}