using Ninject.Web.WebApi.FilterBindingSyntax;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebAPIDemo.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebAPIDemo.App_Start.NinjectWebCommon), "Stop")]

namespace WebAPIDemo.App_Start {
    using System;
    using System.Web;
    using System.Web.Http;
    using DataAccessLayer.Models;
    using DataAcessLayer;
    using DataAcessLayer.Models;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Ninject.Web.WebApi;

    public static class NinjectWebCommon {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop() {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel() {
            var kernel = new StandardKernel();
            try {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                //To Resolve controller dependencies
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            } catch {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel) {
            //Db context will be prepared as per request and disposed there after
            kernel.Bind<PracticeSQLEntities>().ToSelf().InRequestScope();
            //Model for the service
            kernel.Bind<IEmployeesModel>().To<EmployeeModel>();
            kernel.Bind<ISecurityProvider>().To<BasicSecurityProvider>();
            kernel.Bind<BasicAuthenticationAttribute>().To<BasicAuthenticationRedirection>();
        }        
    }
}