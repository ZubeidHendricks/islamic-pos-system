namespace IslamicPOS.Infrastructure.Data.Seed;

public class DbSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<DbSeeder> _logger;

    public DbSeeder(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        ILogger<DbSeeder> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        try
        {
            // Create admin user
            if (!await _userManager.Users.AnyAsync())
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@islamicpos.com",
                    FirstName = "Admin",
                    LastName = "User",
                    Role = "Admin",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await _userManager.CreateAsync(adminUser, "Admin123!");
            }

            // Seed initial categories
            if (!await _context.Set<ProductCategory>().AnyAsync())
            {
                var categories = new[]
                {
                    new ProductCategory { Name = "Electronics", IsActive = true },
                    new ProductCategory { Name = "Clothing", IsActive = true },
                    new ProductCategory { Name = "Books", IsActive = true },
                    new ProductCategory { Name = "Food", IsActive = true }
                };

                await _context.AddRangeAsync(categories);
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during database seeding");
            throw;
        }
    }
}