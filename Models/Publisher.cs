using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoginFPTBook.Models
{
    public class Publisher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Publisher_ID { get; set; }

        [Required(ErrorMessage = "Please, enter a Publisher Name!")]
        [StringLength(50, ErrorMessage = "Please, enter the Publisher Name length must be between {2} and {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z]{1,50}$", 
        ErrorMessage = "Please, enter a valid Publisher Name!")]
        public string Publisher_Name { get; set; }
        [Required]
        [EmailAddress]
        public string Publisher_Email { get; set; }

        [Required(ErrorMessage = "Please, Enter the Phone Number!")]
        [RegularExpression(@"^0[0-9]{9}",
        ErrorMessage = "Please, enter a valid Phone Number!")]
        public string Publisher_Telephone { get; set; }

        [Required]
        public int Publisher_Status { get; set; }
        public virtual ICollection<Book>? Book { get; set; }
    }
}