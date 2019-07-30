using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using DataAcessLayer.Models;

namespace WebAPIDemo {
    //This redirection is not working need to BasicAuthentication with 
    //property injection and db context with instance rather than per request 
    public class BasicAuthenticationAttribute: AuthorizationFilterAttribute { }

    public class BasicAuthenticationRedirection: BasicAuthenticationAttribute
    {
        private readonly ISecurityProvider _securityProvider;
        public BasicAuthenticationRedirection(ISecurityProvider securityProvider) {
            _securityProvider = securityProvider;
        }

        public override void OnAuthorization(HttpActionContext actionContext) {
            var authorizationHeader = actionContext.Request.Headers.Authorization;
            if (authorizationHeader == null) {
                actionContext.Response = 
                    actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            } else {
                var token = authorizationHeader.Parameter;
                //User:Passward need to be in base64 encoded
                //Need to give Authorization Basic MzEwMTkzMTQzOlRlc3QxMjM= while making request in header
                var tokenValues = 
                    Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split(':');
                var userName = tokenValues[0];
                var password = tokenValues[1];

                if (_securityProvider.Login(userName, password)) {
                    System.Threading.Thread.CurrentPrincipal = 
                        new GenericPrincipal(new GenericIdentity(userName),null);
                    //Now this identity can be used to access logged-in current username
                } else {
                    actionContext.Response = 
                        actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}