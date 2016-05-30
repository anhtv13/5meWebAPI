using _5meProjects.CustomObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
namespace _5meProjects.Controllers
{
    public class NewsController : ApiController
    {
        private int m_counter = 0;
        private object m_lock = new object();

        #region Counter
        private bool CheckCounter()
        {
            lock (m_lock)
            {
                if (m_counter < 100)
                {
                    m_counter++;
                    return true;
                }
                return false;
            }
        }

        private void DecreaseCounter()
        {
            lock (m_lock)
            {
                m_counter--;
            }
        }
        #endregion

        [HttpGet]
        public void RequestNewsForHomePage()
        {
            if (CheckCounter())
            {
                Request.CreateResponse(HttpStatusCode.OK, "test ok");
            }
            else
                Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, "Server Overloaded");

            DecreaseCounter();
        }
    }
}
