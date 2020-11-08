using System;
using System.Collections.Generic;
using System.Text;

namespace PetminderApp.Models
{
    public class PetModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Weight { get; set; }
        public int? Age { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public Guid AccountId { get; set; }
    }
}
