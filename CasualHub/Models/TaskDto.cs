using CasualHub.UI.Helpers.Attributes;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CasualHub.UI.Models
{
    public class TaskDto
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "date is required")]
        public DateTime date { get; set; }
        [Required(ErrorMessage = "Category is required")]

        public string Category { get; set; }
        [Required(ErrorMessage = "IsComplited is required")]
        public bool IsComplited { get; set; }
        public virtual IdentityUser ApplicationUser { get; set; }
    }
}
