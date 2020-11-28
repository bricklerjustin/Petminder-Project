using System;
using System.ComponentModel.DataAnnotations;

namespace Petminder_RestApi.Dtos
{
    public class FileCreateDto
    {
        public Guid AccountId { get; set; }
        public Guid PetId { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Data { get; set; }
    }
}