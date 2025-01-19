using Autofac.Extensions.DependencyInjection;
using CMS.API;
using CMS.Application;
using CMS.Application.Configurations;
using CMS.Infrastructure;
using CMS.Infrastructure.Data.Extensions;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Set up Autofac as the Service Provider Factory
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApiServices();

var appConfig = app.Services.GetRequiredService<ApplicationConfiguration>();
var uploadedDirectory = builder.Configuration["StaticFiles:UploadedDirectory"];
var targetDirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
var uploadedPath = Path.Combine(targetDirectoryPath!, appConfig.UploadedDirectory);
if (!Directory.Exists(uploadedPath))
{
    Directory.CreateDirectory(uploadedPath);
}
Console.WriteLine($"UploadedPath:{uploadedPath}");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadedPath),
    RequestPath = $"/{appConfig.FileProviderRequestPath}"
});

await app.Services.InitializeDatabaseAsync(appConfig);



app.Run();


