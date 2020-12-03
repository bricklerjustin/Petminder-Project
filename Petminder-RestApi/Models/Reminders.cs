using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Models
{
    [Table("reminders")]
    public partial class Reminders
    {
        [Required]
        [Column("name")]
        [StringLength(500)]
        public string Name { get; set; }
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("message")]
        [StringLength(500)]
        public string Message { get; set; }
        [Column("complete")]
        public bool Complete { get; set; }
        [Column("repeat")]
        public bool Repeat { get; set; }
        [Column("account_id")]
        public Guid AccountId { get; set; }
        [Column("frequency")]
        [StringLength(250)]
        public string Frequency { get; set; }
        [Column("type")]
        [StringLength(250)]
        public string Type { get; set; }
        [Column("start_date", TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column("time", TypeName = "datetime")]
        public DateTime? Time { get; set; }
    }
}
