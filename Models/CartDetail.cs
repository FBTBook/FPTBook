using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoginFPTBook.Models
{
    public class CartDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartDetail_ID { get; set; }

        [Required]
        [Range(1, 1000000000)]
        public int Cart_Quantity { get; set; }

        public int Cart_ID { get; set; }
        [ForeignKey("Cart_ID")]
        public virtual Cart? Cart { get; set; }

        public int Book_ID { get; set; }
        [ForeignKey("Book_ID")]
        public virtual Book? Book { get; set; }
    }
}