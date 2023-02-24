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
        public string Publisher_Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Publisher_Email { get; set; }

        [Required(ErrorMessage = "Please, Enter the Phone Number!")]
        [DataType(DataType.PhoneNumber)]
        public string Publisher_Telephone { get; set; }

        [Required]
        public int Publisher_Status { get; set; }
        public virtual ICollection<Book>? Book { get; set; }
    }
}