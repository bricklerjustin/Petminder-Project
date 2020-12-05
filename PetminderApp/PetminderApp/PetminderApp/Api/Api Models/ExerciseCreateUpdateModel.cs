using System;
using System.Collections.Generic;
using System.Text;

namespace PetminderApp.Api.Api_Models
{
    class ExerciseCreateUpdateModel
    {
        public Guid PetId { get; set; }
        public Guid AccountId { get; set; }
        public double Distance { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime EntryDate { get; set; }
        public string Um { get; set; }
    }
}
