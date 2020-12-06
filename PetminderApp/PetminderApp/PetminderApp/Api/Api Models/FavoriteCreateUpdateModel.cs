using System;
using System.Collections.Generic;
using System.Text;

namespace PetminderApp.Api.Api_Models
{
    public class FavoriteCreateUpdateModel
    {
        public Guid AccountId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Url { get; set; }
        public string LocationType { get; set; }
        public string UrlType { get; set; }
        public string Phone { get; set; }
    }
}
