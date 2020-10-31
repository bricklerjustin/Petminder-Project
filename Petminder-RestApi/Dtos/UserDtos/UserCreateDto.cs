using System;
using System.ComponentModel.DataAnnotations;

namespace Petminder_RestApi.Dtos
{
    public class UserCreateDto : UserCreateUpdateBaseDto
    {
        [Required]
        [StringLength(250)]
        public string Username { get; set; }
        public Guid AccountId { get; set; }
    }
}