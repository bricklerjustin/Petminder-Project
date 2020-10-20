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
        [Column("petexercise_id")]
        public int PetexerciseId { get; set; }
        [Column("start_time", TypeName = "datetime")]
        public DateTime? StartTime { get; set; }
        [Column("end_time", TypeName = "datetime")]
        public DateTime? EndTime { get; set; }
        [Column("distance")]
        public float? Distance { get; set; }
        [Column("pet_id")]
        public int PetId { get; set; }
    }
}
