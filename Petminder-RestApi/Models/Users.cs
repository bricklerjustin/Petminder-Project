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
        [Column("user_id")]
        public int UserId { get; set; }
        [Required]
        [Column("username")]
        [StringLength(500)]
        public string Username { get; set; }
        [Required]
        [Column("pass_encrypt")]
        [StringLength(500)]
        public string PassEncrypt { get; set; }
        [Column("google_auth_token")]
        [StringLength(500)]
        public string GoogleAuthToken { get; set; }
        [Column("api_key")]
        [StringLength(500)]
        public string ApiKey { get; set; }
        [Column("account_id")]
        public int AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty(nameof(Accounts.Users))]
        public virtual Accounts Account { get; set; }
    }
}
