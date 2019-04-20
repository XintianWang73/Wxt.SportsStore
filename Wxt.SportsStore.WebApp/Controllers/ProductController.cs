using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wxt.SportsStore.Domain.Abstract;
using Wxt.SportsStore.WebApp.Models;

namespace Wxt.SportsStore.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository repository;
        public const int PageSize = 10;

        public ProductController(IProductsRepository productsRepository)
        {
            this.repository = productsRepository;
        }
        

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List(string category, int page = 1)
        {
             ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository
                                .Products
                                .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository
                                .Products
                                .Where(p => category == null || p.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}