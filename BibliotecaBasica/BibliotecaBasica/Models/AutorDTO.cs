
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BibliotecaBasica.Models
{
    /**
     * De este modelo se hereda Recuros para poder configurar los HATEOAS en el controller
     */
    public class AutorDTO: Recurso
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }        
        public DateTime FechaNacimiento { get; set; }
        public IEnumerable<LibroDTO> Libros { get; set; }
    }
}
