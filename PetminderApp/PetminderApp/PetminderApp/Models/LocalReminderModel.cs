using System;
using System.Collections.Generic;
using System.Text;

namespace PetminderApp.Models
{
    public class LocalReminderModel
    {
        public LocalReminderModel()
        {
            calendarIds = new List<string>();
        }

        public Guid databaseId { get; set; }
        public List<string> calendarIds { get; set; }
        public bool? toBeRemoved { get; set; }
    }
}
