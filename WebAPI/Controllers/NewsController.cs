using WebApi.CustomObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Secure;
using WebApi.Log4net;
using Entity.News;
using Entity.User;
using System.Web;
namespace WebApi.Controllers
{
    public class NewsController : ApiController
    {
        private IptLogger m_log = new IptLogger("UserController");

        #region Get Data
        [HttpGet]
        public object Category()
        {
            if (Counter.Instance.CheckCounter())
            {
                try
                {                    
                    return NewsManager.Instance.GetCategoryList();
                }
                catch (Exception ex)
                {
                    m_log.Error(ex.ToString());
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ErrorMessage.InternalServerError);
                }
                finally
                {
                    Counter.Instance.DecreaseCounter();
                }
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, ErrorMessage.ServerOverloaded);
        }

        [HttpGet]
        public object NewsByTag(string tag, int index = 0, int offset = 10)
        {
            if (Counter.Instance.CheckCounter())
            {
                try
                {
                    var data = NewsManager.Instance.GetNewsByTag(tag, index, offset, false);
                    return data;
                }
                catch (Exception ex)
                {
                    m_log.Error(ex.ToString());
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ErrorMessage.InternalServerError);
                }
                finally
                {
                    Counter.Instance.DecreaseCounter();
                }
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, ErrorMessage.ServerOverloaded);
        }

        [HttpGet]
        public object News(string idCategory, int index = 0, int offset = 10)
        {
            if (Counter.Instance.CheckCounter())
            {
                try
                {
                    var data = NewsManager.Instance.GetNewsByCategory(idCategory, index, offset, false);
                    return data;
                }
                catch (Exception ex)
                {
                    m_log.Error(ex.ToString());
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ErrorMessage.InternalServerError);
                }
                finally
                {
                    Counter.Instance.DecreaseCounter();
                }
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, ErrorMessage.ServerOverloaded);
        }

        [HttpGet]
        public object News(string id)
        {
            if (Counter.Instance.CheckCounter())
            {
                try
                {
                    return NewsManager.Instance.GetDetailedNewsById(id);
                }
                catch (Exception ex)
                {
                    m_log.Error(ex.ToString());

                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ErrorMessage.InternalServerError);
                }
                finally
                {
                    Counter.Instance.DecreaseCounter();
                }
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, ErrorMessage.ServerOverloaded);
        }
        #endregion

        #region Normal Writer
        [AcceptVerbs("POST", "GET")]
        [AuthorizeToken(Roles = "User")]
        public object Upload([FromBody]object news)
        {
            if (Counter.Instance.CheckCounter())
            {
                try
                {
                    IEnumerable<string> headerValues = Request.Headers.GetValues("X-AUTH-TOKEN");
                    string token = headerValues.FirstOrDefault();
                    string userId = UserManager.Instance.GetUserIdByToken(token);
                    if (string.IsNullOrEmpty(userId))
                        return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ErrorMessage.Unauthorized);
                    else
                        return NewsManager.Instance.RequestUploadNews(news.ToString(), userId);
                }
                catch (Exception ex)
                {
                    m_log.Error(ex.ToString());

                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ErrorMessage.InternalServerError);
                }
                finally
                {
                    Counter.Instance.DecreaseCounter();
                }
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, ErrorMessage.ServerOverloaded);
        }

        [AcceptVerbs("POST", "GET")]
        [AuthorizeToken(Roles = "Writer")]
        public object Edit([FromBody]object news)
        {
            if (Counter.Instance.CheckCounter())
            {
                try
                {
                    throw new NotImplementedException();
                }
                catch (Exception ex)
                {
                    m_log.Error(ex.ToString());

                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ErrorMessage.InternalServerError);
                }
                finally
                {
                    Counter.Instance.DecreaseCounter();
                }
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, ErrorMessage.ServerOverloaded);
        }
        #endregion

        #region Admin User
        #endregion
    }
}
