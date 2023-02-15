using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoginFPTBook.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Category_ID { get; set; }
        public string Category_Name { get; set; }
        public string Category_Description { get; set; }
        public int Category_Status { get; set; }
        public virtual ICollection<Book>? Book { get; set; }
    }
}