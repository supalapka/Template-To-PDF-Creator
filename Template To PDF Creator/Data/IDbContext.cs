using Microsoft.EntityFrameworkCore;
using Template_To_PDF_Creator.Model;

namespace Template_To_PDF_Creator.Data
{
    public interface IDbContext
    {
        DbSet<Template> Templates { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
