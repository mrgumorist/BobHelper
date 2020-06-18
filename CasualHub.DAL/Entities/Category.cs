using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CasualHub.DAL
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int ID { get; set; }
        public string CategoryName { get; set; }
    }
}
