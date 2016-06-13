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
namespace WebApi.Controllers
{
    public class NewsController : ApiController
    {
        private IptLogger m_log = new IptLogger("UserController");

        ////[HttpGet]
        //public object RequestNewsForHomePage()
        //{
        //    try
        //    {
        //        if (Counter.Instance.CheckCounter())
        //            throw new NotImplementedException();
        //        else
        //            return new ErrorObject(HttpStatusCode.ServiceUnavailable, ErrorMessage.ServerOverloaded);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ErrorObject(HttpStatusCode.InternalServerError, string.Empty);
        //    }
        //    finally
        //    {
        //        Counter.Instance.DecreaseCounter();
        //    }
        //}

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
    }
}
