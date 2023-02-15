using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LoginFPTBook.Data;

namespace LoginFPTBook.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cart_ID { get; set; }
        public int Cart_Quantity { get; set; }
        public string User_ID { get; set; }
        [ForeignKey("User_ID")]
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public int Book_ID { get; set; }
        [ForeignKey("Book_ID")]
        public virtual Book? Book { get; set; }
    }
}