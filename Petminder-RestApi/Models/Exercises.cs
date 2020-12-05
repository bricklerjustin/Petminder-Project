using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Models
{
    [Table("exercises")]
    public partial class Exercises
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("pet_id")]
        public Guid PetId { get; set; }
        [Column("account_id")]
        public Guid AccountId { get; set; }
        [Column("distance")]
        public double? Distance { get; set; }
        [Column("time")]
        public TimeSpan? Time { get; set; }
        [Column("entry_date", TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }
        [Column("um")]
        [StringLength(50)]
        public string Um { get; set; }
        [NotMapped]
        public string Name { get; set; } //Join Prop

        [ForeignKey(nameof(PetId))]
        [InverseProperty(nameof(Pets.Exercises))]
        public virtual Pets Pet { get; set; }
    }
}
