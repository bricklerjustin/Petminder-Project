using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petminder_RestApi.Models
{
    [Table("favorites")]
    public partial class Favorites
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("account_id")]
        public Guid AccountId { get; set; }
        [Column("type")]
        [StringLength(500)]
        public string Type { get; set; }
        [Column("name")]
        [StringLength(500)]
        public string Name { get; set; }
        [Column("location")]
        [StringLength(500)]
        public string Location { get; set; }
        [Column("url")]
        [StringLength(500)]
        public string Url { get; set; }
        [Column("location_type")]
        [StringLength(500)]
        public string LocationType { get; set; }
        [Column("url_type")]
        [StringLength(500)]
        public string UrlType { get; set; }
        [Column("phone")]
        [StringLength(500)]
        public string Phone { get; set; }
    }
}
