using PetminderApp.Api.Api_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PetminderApp.Models.ListViewGroups
{
    public class FavoriteGrouped : ObservableCollection<FavoriteReadModel>
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
    }
}

