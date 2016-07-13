using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.User
{
    public class RegisterUser
    {
        private string m_username;

        public string Username
        {
            get { return m_username; }
            set { m_username = value; }
        }
        private string m_password;

        public string Password
        {
            get { return m_password; }
            set { m_password = value; }
        }
        private DateTime m_dob;

        public DateTime Dob
        {
            get { return m_dob; }
            set { m_dob = value; }
        }

        public RegisterUser() { }
    }
}
