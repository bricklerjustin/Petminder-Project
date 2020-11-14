using System;
using System.ComponentModel.DataAnnotations;

namespace Petminder_RestApi.Dtos
{
    public class UserCreateUpdateBaseDto
    {
        [Required]
        public string PasswordHash { get; set; }
        public string GoogleAuth { get; set; }
        [Required]
        public string Email {get; set; }

    }
}