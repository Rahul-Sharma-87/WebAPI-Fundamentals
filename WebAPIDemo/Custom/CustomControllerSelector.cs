using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebAPIDemo.Custom {
    /// <summary>
    /// This class shows usage of custom header to hold version information
    /// </summary>
    public class CustomControllerSelector:DefaultHttpControllerSelector {

        public CustomControllerSelector(HttpConfiguration configuration) : base(configuration) {
        }
        
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request) {
            const string customHeader = "x-employeeserviceversion";
            string version = "1";
            //This is dictionary of all the controllers
            var controllers = GetControllerMapping();
            // To fetch the controller information
            var routeData = request.GetRouteData();
            var controllerName = routeData.Values["Controller"].ToString();

            if (request.Headers.Contains(customHeader)) {
                version = request.Headers.GetValues(customHeader).FirstOrDefault();
            }
            //Selects specific version of controller and returns descriptor
            if (controllers.TryGetValue(controllerName + version,
                out HttpControllerDescriptor httpControllerDescriptor)) {
                return httpControllerDescriptor;
            }
            return base.SelectController(request);
        }
    }
}