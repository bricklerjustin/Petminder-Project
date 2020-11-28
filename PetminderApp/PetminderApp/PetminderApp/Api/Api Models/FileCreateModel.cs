using System;
using System.Collections.Generic;
using System.Text;

namespace PetminderApp.Api.Api_Models
{
    class FileCreateModel
    {
        public Guid AccountId { get; set; }
        public Guid PetId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
    }
}
