using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Models
{
    [Table("accounts")]
    public partial class Accounts
    {
        public Accounts()
        {
            Pets = new HashSet<Pets>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("create_date", TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        [Required]
        [Column("api_key")]
        public string ApiKey { get; set; }

        [InverseProperty("Account")]
        public virtual ICollection<Pets> Pets { get; set; }
    }
}
