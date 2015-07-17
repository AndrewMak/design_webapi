
using System;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ToDo.Api.Filters
{
    public class HttpsRequire : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "Você precisa estar em uma conexão segura HTTPS"
                };
            }
            base.OnActionExecuting(actionContext);
        }
    }
}
