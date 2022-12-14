using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCalls.Data
{
    [Table("Calls")]
    public class Call
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CallId { get; set; }

        [ForeignKey(nameof(FromContacts))]
        [Required]
        public int FromId { get; set; }
        public virtual Contact FromContacts { get; set; }

        [ForeignKey(nameof(ToContacts))]
        [Required]
        public int ToId { get; set; }
        public virtual Contact ToContacts { get; set; }

        [Column(TypeName = "timestamp")]
        [Required]
        public DateTime TimeStart { get; set; }

        [Column(TypeName = "timestamp")]
        [Required] 
        public DateTime TimeEnd { get; set; }

        
    }
    
}
