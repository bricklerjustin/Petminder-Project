using System;
using System.Collections.Generic;
using System.Text;

namespace PetminderApp.Api
{
    public static class UserInfo
    {
        public static string Username { get; set; }
        public static string Token { get; set; }
        public static Guid AccountId { get; set; }
        public static bool LoggedIn { get; set; }

    }
}
