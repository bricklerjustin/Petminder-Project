﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PetminderApp.Api.Api_Models
{
    public class ExerciseReadModel
    {
        public Guid Id { get; set; }
        public Guid PetId { get; set; }
        public Guid AccountId { get; set; }
        public double Distance { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime EntryDate { get; set; }
        public string Name { get; set; }
        public string Um { get; set; }
    }
}
