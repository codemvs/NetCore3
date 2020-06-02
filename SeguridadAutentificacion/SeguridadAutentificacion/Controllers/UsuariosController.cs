using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SeguridadAutentificacion.Context;
using SeguridadAutentificacion.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SeguridadAutentificacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public UsuariosController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        //POST: /api/usuarios/asignarusuariorol
        [HttpPost("AsignarUsuarioRol")]
        /*{
	        "userid":"f0cd3007-8d0d-4bf3-9410-c3eb7c0f5ca1",
	        "rolename":"admin"
        }*/
    public async Task<ActionResult> AsignarRolUsuario(EditarRolDTO editarRolDTO)
        {
            var usuario = await userManager.FindByIdAsync(editarRolDTO.UserId);
            if(usuario == null)
            {
                return NotFound();
            }
            // Autentificacion clasica con Identity
            await userManager.AddClaimAsync(usuario, new Claim(ClaimTypes.Role, editarRolDTO.RoleName));
            // JWT
            await userManager.AddToRoleAsync(usuario, editarRolDTO.RoleName);
            return Ok();
        }
        //POST: /api/usuarios/asignarusuariorol
        [HttpPost("RemoverUsuarioRol")]
        public async Task<ActionResult> RemoverUsuarioRol(EditarRolDTO editarRolDTO)
        {
            var usuario = await userManager.FindByNameAsync(editarRolDTO.UserId);
            if (usuario == null)
            {
                return NotFound();
            }
            await userManager.RemoveClaimAsync(usuario, new Claim(ClaimTypes.Role, editarRolDTO.RoleName));
            await userManager.RemoveFromRoleAsync(usuario, editarRolDTO.RoleName);
            return Ok();
        }
    }
}
