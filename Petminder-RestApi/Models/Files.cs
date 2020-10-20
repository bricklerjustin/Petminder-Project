using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Models
{
    [Table("files")]
    public partial class Files
    {
        public Files()
        {
            PetFiles = new HashSet<PetFiles>();
        }

        [Key]
        [Column("file_id")]
        public int FileId { get; set; }
        [Column("data")]
        public string Data { get; set; }
        [Column("name")]
        [StringLength(500)]
        public string Name { get; set; }
        [Column("type")]
        [StringLength(255)]
        public string Type { get; set; }
        [Column("pet_file_id")]
        public int PetFileId { get; set; }

        [InverseProperty("File")]
        public virtual ICollection<PetFiles> PetFiles { get; set; }
    }
}
