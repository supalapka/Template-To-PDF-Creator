using Microsoft.EntityFrameworkCore;
using Template_To_PDF_Creator.Data;
using Template_To_PDF_Creator.Model;

namespace Template_To_PDF_Creator.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly IDbContext _context;

        public TemplateRepository(IDbContext context) => _context = context;

        public async Task AddAsync(Template template)
        {
            _context.Templates.Add(template);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Templates.FindAsync(id);
            if (entity != null)
            {
                _context.Templates.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Template with Id {id} not found");
            }
        }

        public async Task<List<Template>> GetAllAsync()
        {
            return await _context.Templates.ToListAsync();
        }

        public async Task<Template?> GetByIdAsync(int id)
        {
            return await _context.Templates.FindAsync(id);
        }

        public async Task UpdateAsync(Template template)
        {
            var existing = await _context.Templates.FindAsync(template.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Template with Id {template.Id} not found");

            existing.Name = template.Name;
            existing.HtmlContent = template.HtmlContent;

            await _context.SaveChangesAsync();
        }
    }
}
