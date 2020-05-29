
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BibliotecaBasica.Models
{
    public class AutorDTO
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }        
        public DateTime FechaNacimiento { get; set; }
        public IEnumerable<LibroDTO> Libros { get; set; }
    }
}
