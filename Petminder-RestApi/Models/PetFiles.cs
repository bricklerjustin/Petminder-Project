using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Models
{
    [Table("pet_files")]
    public partial class PetFiles
    {
        [Key]
        [Column("pet_file_id")]
        public int PetFileId { get; set; }
        [Column("file_id")]
        public int FileId { get; set; }
        [Column("pet_id")]
        public int PetId { get; set; }

        [ForeignKey(nameof(FileId))]
        [InverseProperty(nameof(Files.PetFiles))]
        public virtual Files File { get; set; }
        [ForeignKey(nameof(PetId))]
        [InverseProperty(nameof(Pets.PetFiles))]
        public virtual Pets Pet { get; set; }
    }
}
