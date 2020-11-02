using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Models
{
    [Table("reminders")]
    public partial class Reminders
    {
        [Required]
        [Column("name")]
        [StringLength(500)]
        public string Name { get; set; }
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("message")]
        [StringLength(500)]
        public string Message { get; set; }
        [Column("complete")]
        public bool Complete { get; set; }
        [Column("repeat")]
        public bool Repeat { get; set; }
        [Column("pet_id")]
        public Guid PetId { get; set; }
        [Column("account_id")]
        public Guid AccountId { get; set; }

        [ForeignKey(nameof(PetId))]
        [InverseProperty(nameof(Pets.Reminders))]
        public virtual Pets Pet { get; set; }
    }
}
