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

        [Required(ErrorMessage = "Please, enter Category Name")]
        [StringLength(50, ErrorMessage = "Please, enter Category Name length must be between {2} and {1}.", MinimumLength = 1)]
        public string Category_Name { get; set; }

        [Required(ErrorMessage = "Please, enter Description")]
        [StringLength(50, ErrorMessage = "Please, enter Description length must be between {2} and {1}.", MinimumLength = 1)]
        public string Category_Description { get; set; }
        [Required]
        public int Category_Status { get; set; }
        public virtual ICollection<Book>? Book { get; set; }
    }
}