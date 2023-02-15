using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoginFPTBook.Models
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetail_ID { get; set; }
        public int OrderDetail_Quantity { get; set; }
        public int OrderDetail_Price { get; set; }
        public int Order_ID { get; set; }
        [ForeignKey("Order_ID")]
        public virtual Order? Order { get; set; }
        public int Book_ID { get; set; }
        [ForeignKey("Book_ID")]
        public virtual Book? Book { get; set; }
    }
}