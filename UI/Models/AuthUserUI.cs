using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Models
{
    [Serializable]
    public class AuthUserUI
    {
        public AuthUserUI()
        {
        }

        public AuthUserUI(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }

        public string Password { get; set; }

    }
}
