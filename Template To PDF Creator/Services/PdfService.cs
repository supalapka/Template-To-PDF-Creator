using PuppeteerSharp;
using PuppeteerSharp.Media;
using System.Text.Json;

namespace Template_To_PDF_Creator.Services
{
    public class PdfService : IPdfService
    {
        public async Task<byte[]> GeneratePdfFromHTMLAsync(string htmlTemplate)
        {
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            await using var page = await browser.NewPageAsync();

            await page.SetContentAsync(htmlTemplate);

            var pdfBytes = await page.PdfDataAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true
            });

            return pdfBytes;
        }

        public async Task<byte[]> GeneratePdfFromHTMLAsync(string htmlTemplate, JsonElement data)
        {
            foreach (var prop in data.EnumerateObject())
            {
                htmlTemplate = htmlTemplate.Replace($"{{{{{prop.Name}}}}}", prop.Value.ToString());
            }
            return await GeneratePdfFromHTMLAsync(htmlTemplate);
        }
    }
}
