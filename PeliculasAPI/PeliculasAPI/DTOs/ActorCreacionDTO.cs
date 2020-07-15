using Microsoft.AspNetCore.Http;
using PeliculasAPI.Validaciones;

namespace PeliculasAPI.DTOs
{
    public class ActorCreacionDTO: ActorPatchDTO
    {

        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 1)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }
    }
}
