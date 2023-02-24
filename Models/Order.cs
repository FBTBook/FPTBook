using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LoginFPTBook.Data;

namespace LoginFPTBook.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order_ID { get; set; }
        [Required]
        public DateTime Order_OrderDate { get; set; }

        public DateTime? Order_DeliveryDate { get; set; }
        [Required]
        public int Order_Status { get; set; }
        public string? User_ID { get; set; }
        [ForeignKey("User_ID")]
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetail { get; set; }
    }
}