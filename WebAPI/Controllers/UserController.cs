using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class UserController : ApiController
    {
        //Lưu ý khi gọi webapi = webclient, method POST thì data post lên phải dùng JsonConvert.Serialise(obj), 
        //ko thì param ở webapi sẽ báo null
        [AcceptVerbs("GET", "POST")]
        public HttpResponseMessage TestMethod([FromBody]string s)
        {
            try
            {
                HttpMethod method = Request.Method;
                //IEnumerable<string> headerValues = Request.Headers.GetValues("X-AUTH-TOKEN");
                //var id = headerValues.FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, "test ok");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
            }
        }
    }
}
