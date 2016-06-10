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
        private HttpStatusCode m_code;

        public HttpStatusCode Code
        {
            get { return m_code; }
            set { m_code = value; }
        }

        private string m_message;

        public string Message
        {
            get { return m_message; }
            set { m_message = value; }
        }

        public ErrorObject(HttpStatusCode code, string message)
        {
            m_code = code;
            m_message = message;
        }
    }
}
