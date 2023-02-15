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
        public string Publisher_Name { get; set; }
        public string Publisher_Email { get; set; }
        public string Publisher_Telephone { get; set; }
        public int Publisher_Status { get; set; }
        public virtual ICollection<Book>? Book { get; set; }
    }
}