using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.User
{
    public class AuthenRequest
    {
        private string m_username;
        private string m_password;

        [JsonProperty(PropertyName = "password")]
        public string Password
        {
            get { return m_password; }
            set { m_password = value; }
        }

        [JsonProperty(PropertyName = "username")]
        public string Username
        {
            get { return m_username; }
            set { m_username = value; }
        }

        public AuthenRequest() { }
    }
}
