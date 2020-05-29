using Microsoft.EntityFrameworkCore;
using MiPrimerWebApiM3.Entities;

namespace MiPrimerWebApiM3.Contexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}
