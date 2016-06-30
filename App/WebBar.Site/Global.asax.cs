using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebBar.Site.CompositionRoot;

namespace WebBar.Site
{
    public class MvcApplication : HttpApplication
    {
        public MvcApplication()
        {
            DependencyInjectionConfig.Start();
            Disposed += MvcApplication_Disposed;
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void MvcApplication_Disposed(object sender, EventArgs e)
        {
            DependencyInjectionConfig.Stop();
        }

    }
}
