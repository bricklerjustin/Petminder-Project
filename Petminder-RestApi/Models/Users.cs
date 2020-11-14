using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Models
{
    [Table("users")]
    public partial class Users
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("username")]
        [StringLength(250)]
        public string Username { get; set; }
        [Required]
        [Column("password_hash")]
        public string PasswordHash { get; set; }
        [Required]
        [Column("api_key")]
        public string ApiKey { get; set; }
        [Column("google_auth")]
        public string GoogleAuth { get; set; }
        [Column("account_id")]
        public Guid AccountId { get; set; }
        [Required]
        [Column("email")]
        [StringLength(500)]
        public string Email { get; set; }
    }
}
