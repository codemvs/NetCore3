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
    }
}
