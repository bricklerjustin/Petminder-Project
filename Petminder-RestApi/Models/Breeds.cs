using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Models
{
    [Table("breeds")]
    public partial class Breeds
    {
        [Key]
        [Column("breed_id")]
        public int BreedId { get; set; }
        [Column("breed")]
        [StringLength(500)]
        public string Breed { get; set; }
        [Column("pet_type_id")]
        public int PetTypeId { get; set; }

        [ForeignKey(nameof(PetTypeId))]
        [InverseProperty(nameof(PetTypes.Breeds))]
        public virtual PetTypes PetType { get; set; }
    }
}
