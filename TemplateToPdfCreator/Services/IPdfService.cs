using System.Text.Json;

namespace Template_To_PDF_Creator.Services
{
    public interface IPdfService
    {
        Task<byte[]> GeneratePdfFromHTMLAsync(string htmlTemplate);
        Task<byte[]> GeneratePdfFromHTMLAsync(string htmlTemplate, JsonElement data);

    }
}
