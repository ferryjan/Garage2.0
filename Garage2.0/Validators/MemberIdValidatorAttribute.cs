using Garage2._0.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0.Validators
{
    public class MemberIdValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object memberIdInput, ValidationContext validationContext)
        {
            try
            {
                Garage2_0Context db = new Garage2_0Context();

                Vehicle vehicle = (Vehicle)validationContext.ObjectInstance;

                var result = db.Members.FirstOrDefault(v => v.MembershipNr == vehicle.Member.MembershipNr);
                if (result == null)
                {
                    return new ValidationResult("Could not find customer with that ID");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            catch (Exception)
            {
                return new ValidationResult("Invalid input");
            }
        }
    }
}