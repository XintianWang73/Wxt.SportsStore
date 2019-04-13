using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Wxt.SportsStore.Domain.Entities;
using Wxt.SportsStore.WebApp.Infrastructure.Binders;

namespace Wxt.SportsStore.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            IocConfig.Register();
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
