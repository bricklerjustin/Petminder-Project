using System;
using System.ComponentModel.DataAnnotations;

namespace Petminder_RestApi.Dtos
{
    public class UserReadDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string GoogleAuth { get; set; }
        public Guid AccountId { get; set; }
        public string Email {get; set; }
    }
}