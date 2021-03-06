[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BlogSystem.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(BlogSystem.Web.App_Start.NinjectWebCommon), "Stop")]

namespace BlogSystem.Web.App_Start
{
    using System;
    using System.Data.Entity;
    using System.Security.Principal;
    using System.Web;

    using BlogSystem.Data;
    using BlogSystem.Data.Common.Repository;
    using BlogSystem.Web.Infrastructure.BlogURL;
    using BlogSystem.Web.Infrastructure.Identity;
    using BlogSystem.Web.Infrastructure.Sanitizer;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<ApplicationDbContext>();
            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));
            kernel.Bind(typeof(IDeletableEntityRepository<>)).To(typeof(DeletableEntityRepository<>));
            kernel.Bind<ICurrentUser>().To<CurrentUser>();
            kernel.Bind<IIdentity>().ToMethod(c => HttpContext.Current.User.Identity);
            kernel.Bind<IBlogUrlGenerator>().To<BlogUrlGenerator>();
            kernel.Bind<ISanitizer>().To<HtmlSanitizerAdapter>();
            kernel.Bind<IBlogSystemData>().To<BlogSystemData>().WithConstructorArgument("context", c => new ApplicationDbContext());
        }
    }
}