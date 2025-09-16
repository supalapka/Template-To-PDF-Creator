using Template_To_PDF_Creator.Data;
using Template_To_PDF_Creator.Model;

namespace Template_To_PDF_Creator.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly IDbContext _context;

        public TemplateRepository(IDbContext context) => _context = context;

        public Task AddAsync(Template template)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Template>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Template?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Template template)
        {
            throw new NotImplementedException();
        }
    }
}
