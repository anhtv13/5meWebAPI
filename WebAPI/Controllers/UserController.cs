using RSATest;
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
                    string encryptedData = (string)authenObj;
                    string authenData = AsymEncryptDecrypt.DecryptText(encryptedData, AsymEncryptDecrypt.keySize, AsymEncryptDecrypt.privateKey);
                    string token = UserManager.Instance.AuthenRequest(authenData);
                    return token;
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, ErrorMessage.ServerOverloaded);
            }
            catch (Exception e)
            {
                m_log.Error(e.ToString());

                if (e.Message == HttpStatusCode.Unauthorized.ToString())
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ErrorMessage.AuthenFailed);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ErrorMessage.InternalServerError);
            }
            finally
            {
                Counter.Instance.CheckCounter();
            }
        }

        public object CheckName([FromBody] string username)
        {
            try
            {
                if (Counter.Instance.CheckCounter())
                    return UserManager.Instance.CheckName(username);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, ErrorMessage.ServerOverloaded);
            }
            catch (Exception e)
            {
                m_log.Error(e.ToString());

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ErrorMessage.InternalServerError);
            }
            finally
            {
                Counter.Instance.CheckCounter();
            }
        }

        [AcceptVerbs("POST")]
        public object Register([FromBody] object userInfo)
        {
            try
            {
                if (Counter.Instance.CheckCounter())
                {
                    string encryptedData = (string)userInfo;
                    string decryptedData = AsymEncryptDecrypt.DecryptText(encryptedData, AsymEncryptDecrypt.keySize, AsymEncryptDecrypt.privateKey);
                    string token = UserManager.Instance.RegisterNewUser(decryptedData);
                    return token;
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, ErrorMessage.ServerOverloaded);
            }
            catch (Exception e)
            {
                m_log.Error(e.ToString());

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ErrorMessage.InternalServerError);
            }
            finally
            {
                Counter.Instance.CheckCounter();
            }
        }
    }
}
