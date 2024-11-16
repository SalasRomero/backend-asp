using backend_asp.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_asp.Entidades
{
    //Esta clase representa el genero de la pelicula a nivel de base de datos
    public class Genero : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")] //hace un atributo requerido
        [StringLength(maximumLength:10)]
        //[PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        [Range(18,120)]
        public int Edad { get; set; }

        [CreditCard]
        public string TarjetaDeCredito { get; set; }
        [Url]
        public string URL { get; set; }
         
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre)) {
                var primeraLetra = Nombre[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper()) {
                    yield return new ValidationResult("La primera letra debe de ser mayúscula",
                        new string[] { nameof(Nombre)});
                }
            } 
        }
    }
}
