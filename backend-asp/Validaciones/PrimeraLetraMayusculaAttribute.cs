using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_asp.Validaciones
{
    public class PrimeraLetraMayusculaAttribute: ValidationAttribute
    {
        //Value es el valor de la propiedad
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) {
                return ValidationResult.Success;
            }

            var primeraLetra = value.ToString()[0].ToString();

            if (primeraLetra != primeraLetra.ToUpper()) {
                return new ValidationResult("La primera letra debe de ser mayúscula");
            }

            return ValidationResult.Success;
        }
    }
}
