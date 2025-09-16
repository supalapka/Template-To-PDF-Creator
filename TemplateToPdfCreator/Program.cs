using Microsoft.EntityFrameworkCore;
using PuppeteerSharp;
using Template_To_PDF_Creator.Data;
using Template_To_PDF_Creator.Repositories;
using Template_To_PDF_Creator.Services;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactDev", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDbContext>(provider => provider.GetRequiredService<LocalDbContext>());

// repositories
builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();
builder.Services.AddScoped<IPdfService, PdfService>();



var app = builder.Build();


{  // workaround tmp solution: added to run migrations when starting from Docker
   // if something is not working, check out to this commit 5188d2e1869eaf5b8c7c16ea46f8f6d017502299 this will revert docket setup changes
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<LocalDbContext>();

    var pendingMigrations = db.Database.GetPendingMigrations();
    if (pendingMigrations.Any())
    {
        var retries = 10;
        while (retries > 0)
        {
            try
            {
                db.Database.Migrate();
                break;
            }
            catch (Exception e)
            {
                retries--;
                Console.WriteLine("SQL Server not ready, retrying in 3 seconds...");
                Thread.Sleep(3000);
            }
        }
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowReactDev");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var browserFetcher = new BrowserFetcher();
await browserFetcher.DownloadAsync(); // downloading chromium once to work with Puppeteer

app.Run();
