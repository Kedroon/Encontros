using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Encontros.Models
{
    public class LocalNotZero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var encontro = (Encontro)validationContext.ObjectInstance;
            if (encontro.LocalId == 0)
            {
                return new ValidationResult("Por favor escolha um local valido.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}