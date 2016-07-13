using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.User
{
    public class User
    {
        private string m_id;
        private string m_username;
        private byte[] m_hashedPassword;
        private DateTime m_dob;
        private DateTime m_joinedAt;
        private int m_role; 

        public int Role
        {
            get { return m_role; }
            set { m_role = value; }
        }

        public DateTime JoinedAt
        {
            get { return m_joinedAt; }
            set { m_joinedAt = value; }
        }

        public DateTime Dob
        {
            get { return m_dob; }
            set { m_dob = value; }
        }

        public byte[] HashedPassword
        {
            get { return m_hashedPassword; }
            set { m_hashedPassword = value; }
        }

        public string Username
        {
            get { return m_username; }
            set { m_username = value; }
        }

        public string Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public User() { }
    }
    public enum UserRoles
    {
        User = 0,
        Admin = 1,
        Writer = 2
    }
}
