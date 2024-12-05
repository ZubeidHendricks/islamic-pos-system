using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Configure HttpClient for API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["API_URL"] ?? builder.HostEnvironment.BaseAddress)
});

// Add authentication state provider
builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
