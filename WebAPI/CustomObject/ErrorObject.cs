using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.CustomObject
{
    public class ErrorObject
    {
        private string m_message;

        public string Message
        {
            get { return m_message; }
            set { m_message = value; }
        }

        public ErrorObject(string message)
        {
            m_message = message;
        }
    }
}
