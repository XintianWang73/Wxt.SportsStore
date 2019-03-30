using Autofac;
using Autofac.Integration.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wxt.SportsStore.Domain.Abstract;
using Wxt.SportsStore.Domain.Entities;

namespace Wxt.SportsStore.WebApp
{
    //IOC - Inversion of control
    public class IocConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            //builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterControllers(AppDomain.CurrentDomain.GetAssemblies()).PropertiesAutowired();
            // Set the dependency resolver to be Autofac.

            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { Name = "Football", Price = 25 },
                new Product { Name = "Surf board", Price = 179 },
                new Product { Name = "Running shoes", Price = 95 }
            });
            builder.RegisterInstance<IProductsRepository>(mock.Object).PropertiesAutowired();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


        }
    }
}