using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebAPIDemo {
    public static class WebAPIConfig {

        /// <summary>
        /// This is global configuration for this webAPI project
        /// called from Global.asax on application load.
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config) {
            //this enables http attribute based route option
            config.MapHttpAttributeRoutes();

            //configure route template
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate:"webapi/{controller}/{id}",
                defaults:new { id=RouteParameter.Optional } );

            //This will remove xml formatter and and even if user set accept header as application/xml
            //we ll get json data as response
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //To make json appear in readable format.
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            //To make json property appear in camel case
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            
            //For JSONP - uncomment below -when cors is not enabled
            //Callback is javascript wrapper function name when calling from Postman
            //http://localhost:61175/webapi/employees?callback=test
            //Header Accept:Application/Javascript
            //var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            //config.Formatters.Insert(0,jsonpFormatter);

            //This is allow selective enabling - even enable and disable CORS can be possible at API level
            // Below will allow all operations/verbs and all domains we also can have domain selections
            config.EnableCors();

            //To enable Https - security - uncomment below
            config.Filters.Add(new RequireHttpsAttribute());

            //To enable versioning - uncomment below
            //config.Services.Replace(
            //    typeof(IHttpControllerSelector), 
            //    new CustomControllerSelector(config));
        }
    }
}