using Microsoft.EntityFrameworkCore;
using PuppeteerSharp;
using Template_To_PDF_Creator.Data;
using Template_To_PDF_Creator.Repositories;
using Template_To_PDF_Creator.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Local")));

builder.Services.AddScoped<IDbContext>(provider => provider.GetRequiredService<LocalDbContext>());

// repositories
builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();
builder.Services.AddScoped<IPdfService, PdfService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var browserFetcher = new BrowserFetcher();
await browserFetcher.DownloadAsync(); // downloading chromium once to work with Puppeteer

app.Run();
