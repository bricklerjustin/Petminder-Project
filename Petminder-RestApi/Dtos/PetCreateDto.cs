using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Dtos
{
    public class PetCreateDto
    {
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
        [Required]
        [Column("account_id")]
        public int AccountId { get; set; }
        [Column("breed")]
        [StringLength(255)]
        public string Breed { get; set; }
    }
}