using Microsoft.AspNetCore.Identity;

namespace IslamicPOS.Domain.Identity;

public class ApplicationUser : IdentityUser
{
    // Additional user properties can be added here
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? LastLoginDate { get; set; }
}