using Microsoft.EntityFrameworkCore;
using IslamicPOS.Infrastructure.Data;
using IslamicPOS.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add MudBlazor
builder.Services.AddMudServices();

// Add application services
builder.Services.AddScoped<IReceiptService, ReceiptService>();
builder.Services.AddScoped<PrinterIntegrationService>();
builder.Services.AddScoped<PrinterConfigurationService>();

var app = builder.Build();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();