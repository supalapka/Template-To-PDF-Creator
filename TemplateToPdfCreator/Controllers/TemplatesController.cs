using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Template_To_PDF_Creator.Model;
using Template_To_PDF_Creator.Repositories;
using Template_To_PDF_Creator.Services;

namespace Template_To_PDF_Creator.Controllers
{
    [Route("[controller]")]
    public class TemplatesController : Controller
    {
        private readonly ITemplateRepository _templateRepo;
        private readonly IPdfService _pdfService;

        public TemplatesController(ITemplateRepository repo, IPdfService pdfService)
        {
            _templateRepo = repo;
            _pdfService = pdfService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var all = await _templateRepo.GetAllAsync();
                return Ok(all);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Template>> GetById(int id)
        {
            try
            {
                var template = await _templateRepo.GetByIdAsync(id);
                if (template == null) return NotFound();
                return template;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Template t)
        {
            try
            {
                await _templateRepo.AddAsync(t);
                return Ok(t);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Template t)
        {
            try
            {
                await _templateRepo.UpdateAsync(t);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var template = await _templateRepo.GetByIdAsync(id);
            if (template == null) return NotFound();

            try
            {
                await _templateRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{id}/pdf")]
        public async Task<IActionResult> GeneratePdf(int id)
        {
            var template = await _templateRepo.GetByIdAsync(id);
            if (template == null) return NotFound();

            try
            {
                var pdfBytes = await _pdfService.GeneratePdfFromHTMLAsync(template.HtmlContent);
                return File(pdfBytes, "application/pdf", "template.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{id}/pdf/substitution")]
        public async Task<IActionResult> GeneratePdfWithSubstitution(int id, [FromBody] JsonElement data)
        {
            var template = await _templateRepo.GetByIdAsync(id);
            if (template == null) return NotFound();

            try
            {
                var pdfBytes = await _pdfService.GeneratePdfFromHTMLAsync(template.HtmlContent, data);
                return File(pdfBytes, "application/pdf", "template.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
