using Microsoft.EntityFrameworkCore;
using Template_To_PDF_Creator.Model;

namespace Template_To_PDF_Creator.Data
{
    public class LocalDbContext : DbContext, IDbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }
        public DbSet<Template> Templates => Set<Template>();
    }
}
