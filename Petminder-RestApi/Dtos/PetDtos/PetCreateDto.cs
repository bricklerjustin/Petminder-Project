using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Dtos
{
    public class PetCreateDto : PetCreateUpdateDtoBase
    {
        [Required]
        public Guid AccountId { get; set; }

    }
}