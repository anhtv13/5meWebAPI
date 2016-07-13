using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.User
{
    public class TokenObject
    {
        private string m_id;
        private string m_username;
        private DateTime m_createAt;
        private Guid m_sessionId;

        public Guid SessionId
        {
            get { return m_sessionId; }
            set { m_sessionId = value; }
        }

        public DateTime CreateAt
        {
            get { return m_createAt; }
            set { m_createAt = value; }
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

        public TokenObject() { }
    }
}
