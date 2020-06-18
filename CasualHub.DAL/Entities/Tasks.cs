using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CasualHub.DAL.Entities
{
    [Table("Tasks")]
    public class Task
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime date { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryID { get; set; }
        public bool IsComplited { get; set; } = false;
        public string ApplicationUserId { get; set; }
    }
}
