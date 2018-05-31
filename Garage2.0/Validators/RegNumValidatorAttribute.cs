using Garage2._0.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0.Validators
{
    public class RegNumValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object regnumInput, ValidationContext validationContext)
        {
            try
            {
                Garage2_0Context db = new Garage2_0Context();

                var result = db.Vehicles.FirstOrDefault(v => v.RegNum == regnumInput.ToString());
                if (result == null)
                {
                      return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("This registration number already exists");
                }
            }
            catch (Exception)
            {
                return new ValidationResult("Invalid input");
            }
        }
    }
}