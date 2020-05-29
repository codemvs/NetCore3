
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaBasica.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public IEnumerable<Libro> Libros { get; set; }
    }
}
