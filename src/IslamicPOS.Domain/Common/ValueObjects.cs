namespace IslamicPOS.Domain.Common;

public record ValueObjects
{
    // Common value objects can be added here
    public static string GenerateUniqueIdentifier() => Guid.NewGuid().ToString();
}