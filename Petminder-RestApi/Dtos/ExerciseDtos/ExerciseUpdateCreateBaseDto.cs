using System;
using System.ComponentModel.DataAnnotations;

namespace Petminder_RestApi.Dtos
{
    public class ExerciseUpdateCreateBaseDto
    {
        public Guid? PetId { get; set; }
        public Guid? AccountId { get; set; }
        public double? Distance { get; set; }
        public TimeSpan? Time { get; set; }
        public DateTime? EntryDate { get; set; }
        public string Um { get; set; }
    }
}