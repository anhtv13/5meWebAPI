using Entity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebApi.Log4net;

namespace WebApi.Secure
{
    public class AuthorizeToken : AuthorizeAttribute
    {
        private IptLogger m_log = new IptLogger("AuthorizeToken");
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            try
            {
                IEnumerable<string> headerValues = actionContext.Request.Headers.GetValues("X-AUTH-TOKEN");
                string token = headerValues.FirstOrDefault();
                if (!UserManager.Instance.CheckToken(token))
                    return false;
                string role = "";
                if (Roles.Length == 0)
                    return true;
                else
                {
                    if (UserManager.Instance.IsAdmin(token))
                        role = UserRoles.Admin.ToString();
                    else if (UserManager.Instance.IsUser(token))
                        role = UserRoles.User.ToString();

                    if (Roles.ToUpper().Contains(role.ToUpper()))
                        return true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                m_log.Error(ex.ToString());
                return false;
            }
        }
    }
}
