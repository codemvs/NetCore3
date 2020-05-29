
using MiPrimerWebApiM3.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiPrimerWebApiM3.Entities
{
    public class Autor: IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        //[PrimeraLetraMayuscula]
        [StringLength(10,ErrorMessage ="El campo nombre debe tener {1} caracteres o menos")]
        public string Nombre { get; set; }
        [Range(18,120)] // La edad debe ser entre 18 y 120
        public int Edad { get; set; }
        [CreditCard]
        public string CreditCard { get; set; }
        [Url]
        public string  Url { get; set; }
        public IEnumerable<Libro> Libros { get; set; }
        // Validacion por objeto, se evalua despues de las validaciones por atributo
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {

                var firstLetter = Nombre[0].ToString();
                if (firstLetter != firstLetter.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula", new string[] { nameof(Nombre)});
                }
            }
        }
    }
}
