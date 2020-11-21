using System;
using System.Collections.Generic;
using System.Text;

namespace PetminderApp.Api.Api_Models
{
    public class ReminderReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public bool Complete { get; set; }
        public bool Repeat { get; set; }
        public Guid AccountId { get; set; }
        public string Frequency { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
    }
}
