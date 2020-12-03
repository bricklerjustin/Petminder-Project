using PetminderApp.Api.Api_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PetminderApp.Models.ListViewGroups
{
    public class ReminderGrouped : ObservableCollection<ReminderReadModel>
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
    }
}
