using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Logistics;

public class Address : ValueObject
{
    public string Line1 { get; }
    public string? Line2 { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCode { get; }
    public string Country { get; }
    
    private Address(
        string line1,
        string? line2,
        string city,
        string state,
        string postalCode,
        string country)
    {
        Line1 = line1;
        Line2 = line2;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }
    
    public static Address Create(
        string line1,
        string? line2,
        string city,
        string state,
        string postalCode,
        string country)
    {
        if (string.IsNullOrWhiteSpace(line1))
            throw new ArgumentException("Address line 1 is required");
            
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City is required");
            
        if (string.IsNullOrWhiteSpace(state))
            throw new ArgumentException("State is required");
            
        if (string.IsNullOrWhiteSpace(postalCode))
            throw new ArgumentException("Postal code is required");
            
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country is required");
            
        return new Address(line1, line2, city, state, postalCode, country);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Line1;
        yield return Line2 ?? string.Empty;
        yield return City;
        yield return State;
        yield return PostalCode;
        yield return Country;
    }
}