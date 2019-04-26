﻿namespace Wxt.SportsStore.WebApp
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using System;
    using System.Web.Mvc;
    using Wxt.SportsStore.Domain.Abstract;
    using Wxt.SportsStore.Domain.Concrete;

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

            //Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>{
            //    new Product { Name = "Football", Price = 25 },
            //    new Product { Name = "Surf board", Price = 179 },
            //    new Product { Name = "Running shoes", Price = 95 }
            //});
            //builder.RegisterInstance<IProductsRepository>(mock.Object).PropertiesAutowired();
            //builder.RegisterType<EFDbContext>().PropertiesAutowired();
            builder.RegisterType<EFProductRepository>().As<IProductsRepository>().PropertiesAutowired();

            builder.RegisterType<EmailOrderProcessor>().As<IOrderProcessor>().PropertiesAutowired();
            builder.RegisterType<EmailSettings>().PropertiesAutowired();

            var container = builder.Build();


            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}