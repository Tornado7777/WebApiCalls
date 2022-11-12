using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace WebApiCalls.Data
{

        [Table("ContactSessions")]
        public class ContactSession
    {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int SessionId { get; set; }

            [Required]
            [StringLength(384)]
            public string SessionToken { get; set; }

            [ForeignKey(nameof(Contact))]
            public int ContactId { get; set; }

            [Column(TypeName = "timestamp")]
            public DateTime TimeCreated { get; set; }

            [Column(TypeName = "timestamp")]
            public DateTime TimeLastRequest { get; set; }

            public bool IsClosed { get; set; }

            [Column(TypeName = "timestamp")]
            public DateTime? TimeClosed { get; set; }

            public virtual Contact Contact { get; set; }
        }

    
}
