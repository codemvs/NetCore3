using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeguridadAutentificacion.Models;

namespace SeguridadAutentificacion.Context
{
    // IdentityDbContext Preconfigurado para trabajar con tablas basicas para sistema de login
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var roleAdmin = new IdentityRole()
            {
                Id = "997e4996-8e53-4727-a4e3-01449e9f0e49",
                Name = "admin",
                NormalizedName = "admin"
            };
            // Agregar role Admin a la base de datos
            builder.Entity<IdentityRole>().HasData(roleAdmin);
            base.OnModelCreating(builder);
        }
    }
}
