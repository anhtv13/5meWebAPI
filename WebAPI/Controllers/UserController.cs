using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.CustomObject;
using WebApi.Log4net;
using WebApi.Secure;

namespace WebApi.Controllers
{
    public class UserController : ApiController
    {
        private IptLogger m_log = new IptLogger("UserController");

        [AcceptVerbs("POST")]
        public object Authen([FromBody]object authenObj)
        {
            try
            {
                if (Counter.Instance.CheckCounter())
                {
                    var data = UserManager.Instance.AuthenRequest(authenObj);
                    return data;
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, ErrorMessage.ServerOverloaded);
            }
            catch (Exception e)
            {
                m_log.Error(e.ToString());

                if (e.Message == ErrorMessage.AuthenFailed)
                    return Request.CreateErrorResponse(HttpStatusCode.Forbidden, e.Message);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ErrorMessage.InternalServerError);
            }
            finally
            {
                Counter.Instance.CheckCounter();
            }
        }
    }
}
