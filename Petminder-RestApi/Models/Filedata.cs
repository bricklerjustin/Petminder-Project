using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Models
{
    [Table("filedata")]
    public partial class Filedata
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("data")]
        public string Data { get; set; }
    }
}
