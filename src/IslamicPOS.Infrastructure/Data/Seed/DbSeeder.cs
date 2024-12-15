using Microsoft.AspNetCore.Identity;
using IslamicPOS.Domain.Identity;

namespace IslamicPOS.Infrastructure.Data.Seed;

public class DbSeeder
{
    private readonly UserManager<ApplicationUser> _userManager;

    public DbSeeder(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task SeedDataAsync()
    {
        // Implement seeding logic
    }
}