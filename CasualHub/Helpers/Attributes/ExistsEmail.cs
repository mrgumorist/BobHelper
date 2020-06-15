using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CasualHub.UI.Helpers.Attributes
{
    public class ExistsEmail : ValidationAttribute
    {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var service = (UserManager<IdentityUser>)validationContext
                           .GetService(typeof(UserManager<IdentityUser>));
                var user = service.FindByEmailAsync(value.ToString()).Result;
                if (user != null)
                {
                    return new ValidationResult(null);
                }
                return ValidationResult.Success;
            }
    }
}
