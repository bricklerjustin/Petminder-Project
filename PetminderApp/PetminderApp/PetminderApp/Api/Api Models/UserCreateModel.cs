using System;
using System.Collections.Generic;
using System.Text;

namespace PetminderApp.Api.Api_Models
{
    class UserCreateModel
    {
        public string username { get; set; }
        public string googleAuth { get; set; }
        public string passwordHash { get; set; }
        public Guid accountId { get; set; }
        public string email { get; set; }
    }
}
