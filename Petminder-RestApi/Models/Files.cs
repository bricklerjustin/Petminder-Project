using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Models
{
    [Table("files")]
    public partial class Files
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("account_id")]
        public Guid AccountId { get; set; }
        [Column("pet_id")]
        public Guid PetId { get; set; }
        [Required]
        [Column("type")]
        [StringLength(50)]
        public string Type { get; set; }
        [Required]
        [Column("name")]
        [StringLength(500)]
        public string Name { get; set; }

        [ForeignKey(nameof(PetId))]
        [InverseProperty(nameof(Pets.Files))]
        public virtual Pets Pet { get; set; }
    }
}
