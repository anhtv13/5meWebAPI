using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApi.Secure
{
    public class AuthorizeToken : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            try
            {
                IEnumerable<string> headerValues = actionContext.Request.Headers.GetValues("X-AUTH-TOKEN");
                string token = headerValues.FirstOrDefault();
                if (SecureHelper.Instance.VerifySignature(token))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
