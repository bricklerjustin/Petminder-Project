using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Models
{
    [Table("reminders")]
    public partial class Reminders
    {
        [Key]
        [Column("reminder_id")]
        public int ReminderId { get; set; }
        [Required]
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }
        [Column("datetime", TypeName = "datetime")]
        public DateTime Datetime { get; set; }
        [Column("message")]
        [StringLength(500)]
        public string Message { get; set; }
        [Column("type")]
        [StringLength(255)]
        public string Type { get; set; }
        [Column("bln_processed")]
        public bool BlnProcessed { get; set; }
        [Column("bln_repeat")]
        public bool BlnRepeat { get; set; }
        [Column("pet_id")]
        public int PetId { get; set; }

        [ForeignKey(nameof(PetId))]
        [InverseProperty(nameof(Pets.Reminders))]
        public virtual Pets Pet { get; set; }
    }
}
