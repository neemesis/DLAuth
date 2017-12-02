using System;

namespace DL.Models
{
    public class UserLogin
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserId { get; set; }
        public string ProviderDisplayName { get; set; }
    }
}