using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wxt.SportsStore.Domain.Abstract;

namespace Wxt.SportsStore.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository repository;
        public ProductController(IProductsRepository productsRepository)
        {
            this.repository = productsRepository;
        }


        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List()
        {
            return View(repository.Products);
        }
    }
}