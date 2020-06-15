using CasualHub.UI.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CasualHub.UI.Models
{
    public class RegisterDto
    {
        [ExistsEmail(ErrorMessage = "Already exist")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Not valid email adress")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Length must be more than 5 symbols", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
