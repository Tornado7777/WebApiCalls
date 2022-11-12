using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApiCalls.Data
{
    [Table("Contacts")]
    public class Contact
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }


        [Required]
        [Phone] 
        public string Phone { get; set; }

        [Required]
        [StringLength(128)]
        public string FIO { get; set; }

        [StringLength(128)]
        public string Company { get; set; }

        [Required]
        [StringLength(384)]
        public string Description { get; set; }

        public bool Locked { get; set; }
    }
}
