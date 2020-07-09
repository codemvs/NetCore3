using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PeliculasAPI.Servicios
{
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        // Permite obtener la ruta en donde se encuentra wwwroot, para poder colocar archivos
        private readonly IWebHostEnvironment env; 
        // Podemos determinar el dominio donde tenemos publicado el web api y asi poder construir la url, la cual se guardara a la bd
        private readonly IHttpContextAccessor httpContextAccessor;

        public AlmacenadorArchivosLocal(IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }
        public Task BorrarArchivo(string ruta, string contenedor)
        {
            if(ruta != null)
            {
                var nombreArchivo = Path.GetFileName(ruta);
                string directorioArchivo = Path.Combine(env.WebRootPath, contenedor, nombreArchivo);
                if (File.Exists(directorioArchivo))
                {
                    File.Delete(directorioArchivo);
                }
            }
            return Task.FromResult(0);
        }

        public async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType)
        {
            await BorrarArchivo(ruta, contenedor);
            return await GuardarArchivo(contenido, extension, contenedor, contentType);
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenendor, string contentType)
        {
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, contenendor);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string ruta = Path.Combine(folder, nombreArchivo);

            await File.WriteAllBytesAsync(ruta, contenido);
            // http or https
            var urlActual = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var urlParaBD = Path.Combine(urlActual, contenendor, nombreArchivo).Replace("\\", "/");

            return urlParaBD;
        }
    }
}
