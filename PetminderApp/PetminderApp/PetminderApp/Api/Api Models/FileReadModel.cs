using System;
using System.Collections.Generic;
using System.Text;

namespace PetminderApp.Api.Api_Models
{
    public class FileReadModel
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid PetId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
