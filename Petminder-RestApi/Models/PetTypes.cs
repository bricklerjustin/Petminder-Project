using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Models
{
    [Table("pet_types")]
    public partial class PetTypes
    {
        public PetTypes()
        {
            Breeds = new HashSet<Breeds>();
        }

        [Key]
        [Column("pet_type_id")]
        public int PetTypeId { get; set; }
        [Required]
        [Column("type")]
        [StringLength(500)]
        public string Type { get; set; }

        [InverseProperty("PetType")]
        public virtual ICollection<Breeds> Breeds { get; set; }
    }
}
