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
            Users = new HashSet<Users>();
        }

        [Key]
        [Column("account_id")]
        public int AccountId { get; set; }

        [InverseProperty("Account")]
        public virtual ICollection<Pets> Pets { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
