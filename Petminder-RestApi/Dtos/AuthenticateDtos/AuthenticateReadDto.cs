using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Dtos
{
    public class AuthenticateReadDto
    {
        [Required]
        public string ApiKey { get; set; }
        public Guid AccountId { get; set; }
    }
}