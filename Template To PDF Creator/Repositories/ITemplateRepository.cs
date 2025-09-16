using Template_To_PDF_Creator.Model;

namespace Template_To_PDF_Creator.Repositories
{
    public interface ITemplateRepository
    {
        Task<List<Template>> GetAllAsync();
        Task<Template?> GetByIdAsync(int id);
        Task AddAsync(Template template);
        Task UpdateAsync(Template template);
        Task DeleteAsync(int id);
    }
}
