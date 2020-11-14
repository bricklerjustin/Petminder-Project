using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Models
{
    [Table("pets")]
    public partial class Pets
    {
        public Pets()
        {
            Files = new HashSet<Files>();
            Reminders = new HashSet<Reminders>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }
        [Column("weight")]
        [StringLength(255)]
        public string Weight { get; set; }
        [Column("age")]
        public int? Age { get; set; }
        [Column("type")]
        [StringLength(255)]
        public string Type { get; set; }
        [Column("breed")]
        [StringLength(255)]
        public string Breed { get; set; }
        [Column("account_id")]
        public Guid AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty(nameof(Accounts.Pets))]
        public virtual Accounts Account { get; set; }
        [InverseProperty("Pet")]
        public virtual ICollection<Files> Files { get; set; }
        [InverseProperty("Pet")]
        public virtual ICollection<Reminders> Reminders { get; set; }
    }
}
