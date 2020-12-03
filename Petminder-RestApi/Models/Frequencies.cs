using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Models
{
    [Table("frequencies")]
    public partial class Frequencies
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("frequency")]
        [StringLength(250)]
        public string Frequency { get; set; }
    }
}
