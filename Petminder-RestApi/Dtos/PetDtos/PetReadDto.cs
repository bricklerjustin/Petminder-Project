using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Dtos
{
    public class PetReadDto
    {
        [Key]
        [Column("pet_id")]
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
        [Column("account_id")]
        public Guid AccountId { get; set; }
        [Column("breed")]
        [StringLength(255)]
        public string Breed { get; set; }
    }
}