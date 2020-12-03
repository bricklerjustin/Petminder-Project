using System;
using System.ComponentModel.DataAnnotations;

namespace Petminder_RestApi.Dtos
{
    public class ReminderUpdateCreateBaseDto
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Message { get; set; }
        public bool Complete { get; set; }
        public bool Repeat { get; set; }
        public Guid AccountId { get; set; }
        public string Frequency {get; set;}
        public string Type {get; set;}
        public DateTime? StartDate { get; set; }
        public DateTime? Time { get; set; }
    }
}
