using lab1.Models;
using lab1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddSingleton(builder.Configuration.GetSection(nameof(AzureStorageSettings)).Get<AzureStorageSettings>());
builder.Services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();

var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();

app.Run();
