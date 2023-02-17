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

        [Required]
        public DateTime Book_PublishDate { get; set; }

        [Required]
        [Range(1, 1000000000)]
        public int Book_NoOfPages { get; set; }

        [Required]
        [Range(1, 1000000000)]
        public int Book_Quantity { get; set; }

        [Required]
        [Range(1, 1000000000)]
        public decimal Book_SalePrice { get; set; }

        [Required]
        [Range(1, 1000000000)]
        public decimal Book_Price { get; set; }

        [Required]
        public string Book_Image { get; set; }

        [Required]
        public int Book_Status { get; set; }

        [Required(ErrorMessage = "Please, enter Description ")]
        [StringLength(50, ErrorMessage = "Please, enter Description length must be between {2} and {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z]{1,50}$", 
        ErrorMessage = "Please, enter a valid Description ")]
        public string Book_Description { get; set; }

        [Required(ErrorMessage = "Please, enter Author Name")]
        [StringLength(50, ErrorMessage = "Please, enter Author Name length must be between {2} and {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z]{1,50}$", 
        ErrorMessage = "Please, enter a valid publisher name!")]
        public string Book_AuthorName { get; set; }

        [Required(ErrorMessage = "Please, enter Book Name")]
        [StringLength(50, ErrorMessage = "Please, enter Book Name length must be between {2} and {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-z]{1,50}$", 
        ErrorMessage = "Please, enter a valid Book Name")]
        public string Book_Name { get; set; }

        public virtual ICollection<OrderDetail>? OrderDetail { get; set; }
        public int Category_ID { get; set; }
        [ForeignKey("Category_ID")]
        public virtual Category? Category { get; set; }
        public int Publisher_ID { get; set; }
        [ForeignKey("Publisher_ID")]
        public virtual Publisher? Publisher { get; set; }
    }
}