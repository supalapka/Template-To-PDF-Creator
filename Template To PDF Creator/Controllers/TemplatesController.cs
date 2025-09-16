using Microsoft.AspNetCore.Mvc;
using Template_To_PDF_Creator.Model;
using Template_To_PDF_Creator.Repositories;

namespace Template_To_PDF_Creator.Controllers
{
    [Route("[controller]")]
    public class TemplatesController : Controller
    {
        private readonly ITemplateRepository _templateRepo;

        public TemplatesController(ITemplateRepository repo)
        {
            _templateRepo = repo;
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
        public async Task<IActionResult> Create(Template t)
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
        public async Task<IActionResult> Update(Template t)
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

    }
}
