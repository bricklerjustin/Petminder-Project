using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Dtos
{
    public class PetCreateUpdateDtoBase
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Weight { get; set; }
        public int Age { get; set; }
        [StringLength(255)]
        public string Type { get; set; }
        [StringLength(255)]
        public string Breed { get; set; }
        [StringLength(50)]
        public string Gender { get; set; }
    }
}