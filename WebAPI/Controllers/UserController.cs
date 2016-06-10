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
        ////Lưu ý khi gọi webapi = webclient, method POST thì data post lên phải dùng JsonConvert.Serialise(obj), 
        ////ko thì param ở webapi sẽ báo null
        //[AcceptVerbs("GET", "POST")]
        //public HttpResponseMessage TestMethod([FromBody]string s)
        //{
        //    try
        //    {
        //        HttpMethod method = Request.Method;
        //        //IEnumerable<string> headerValues = Request.Headers.GetValues("X-AUTH-TOKEN");
        //        //var id = headerValues.FirstOrDefault();
        //        return Request.CreateResponse(HttpStatusCode.OK, "test ok");
        //    }
        //    catch (Exception e)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, e.ToString());
        //    }
        //}

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
                    return new ErrorObject(HttpStatusCode.ServiceUnavailable, ErrorMessage.ServerOverloaded);
            }
            catch (Exception e)
            {
                //log lại để biết exception gì
                //trả về string.Empty vì lí do bảo mật
                return new ErrorObject(HttpStatusCode.InternalServerError, string.Empty);
            }
            finally
            {
                Counter.Instance.CheckCounter();
            }
        }
    }
}
