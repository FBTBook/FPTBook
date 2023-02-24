using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LoginFPTBook.Models;
using Microsoft.AspNetCore.Identity;

namespace LoginFPTBook.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Please, enter Fullname")]
        [StringLength(50, ErrorMessage = "Please, enter Fullname length must be between {2} and {1}.", MinimumLength = 1)]
        public string User_Fullname { get; set; }

        [Required]
        public DateTime User_Birthdate { get; set; }

        [Required(ErrorMessage = "Please, enter a Address")]
        [StringLength(50, ErrorMessage = "Please, enter Address length must be between {2} and {1}.", MinimumLength = 1)]
        public string User_Address { get; set; }

        [Required]
        public int User_Status { get; set; }

        [Required]
        public string User_Gender { get; set; }

        public virtual Cart? Cart { get; set; }
        public virtual ICollection<Order>? Order { get; set; }
    }
}
