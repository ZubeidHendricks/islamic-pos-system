using Microsoft.AspNetCore.Identity;

namespace IslamicPOS.Core.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Organization { get; set; }
    public bool IsActive { get; set; } = true;
}