using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CasualHub.UI.Helpers.Attributes
{
    public class ExistsUsername : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (UserManager<IdentityUser>)validationContext
                       .GetService(typeof(UserManager<IdentityUser>));
            var user = service.Users.FirstOrDefault(x => x.UserName == value.ToString());
            if (user != null)
            {
                return new ValidationResult(null);
            }
            return ValidationResult.Success;
        }
    }
}
