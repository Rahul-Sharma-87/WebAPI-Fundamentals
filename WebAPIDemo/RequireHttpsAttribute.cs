using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPIDemo {
    /// <summary>
    /// To intercept use of https or not 
    /// And can be enabled with the attribute as well as global configuration
    /// </summary>
    public class RequireHttpsAttribute: AuthorizationFilterAttribute {
        public override void OnAuthorization(HttpActionContext actionContext) {
            if (!actionContext.Request.RequestUri.Scheme.Equals(
                "https",
                StringComparison.CurrentCultureIgnoreCase)
            ) {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Found);
                actionContext.Response.Content = new StringContent("Service is secured. Please use Https instead.");

                //Browser will redirect it automatically on 
                //seeing found status and header wil different location set.
                UriBuilder uriBuilder = 
                    new UriBuilder(actionContext.Request.RequestUri) {
                        Scheme = "https",
                        Port = 44301
                    };
                actionContext.Response.Headers.Location = uriBuilder.Uri;

            } else {
                base.OnAuthorization(actionContext);
            }
            
        }
    }
}