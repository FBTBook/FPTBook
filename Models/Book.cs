using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoginFPTBook.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Book_ID { get; set; }
        public DateTime Book_PublishDate { get; set; }
        public int Book_NoOfPages { get; set; }
        public int Book_Quantity { get; set; }
        public int Book_SalePrice { get; set; }
        public int Book_Price { get; set; }
        public string Book_Image { get; set; }
        public int Book_Status { get; set; }
        public string Book_Description { get; set; }
        public string Book_AuthorName { get; set; }
        public string Book_Name { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetail { get; set; }
        public int Category_ID { get; set; }
        [ForeignKey("Category_ID")]
        public virtual Category? Category { get; set; }
        public int Publisher_ID { get; set; }
        [ForeignKey("Publisher_ID")]
        public virtual Publisher? Publisher { get; set; }
    }
}