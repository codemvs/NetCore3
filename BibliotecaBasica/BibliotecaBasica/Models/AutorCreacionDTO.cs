using System;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaBasica.Models
{
    public class AutorCreacionDTO
    {
        [Required]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
