using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LoginFPTBook.Models;
using Microsoft.AspNetCore.Identity;

namespace LoginFPTBook.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string User_Fullname { get; set; }
        public DateTime User_Birthdate { get; set; }
        public string User_Address { get; set; }
        public int User_Status { get; set; }
        public string User_Gender { get; set; }

        public virtual Cart? Cart { get; set; }
        public virtual ICollection<Order>? Order { get; set; }
    }
}
