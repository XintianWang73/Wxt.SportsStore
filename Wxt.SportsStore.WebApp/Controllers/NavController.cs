namespace Wxt.SportsStore.WebApp.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Wxt.SportsStore.Domain.Abstract;

    public class NavController : Controller
    {
        private IProductsRepository repository;
        public NavController(IProductsRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            //Cart cart = (Cart)Session["Cart"];
            //category = (string)ControllerContext.RouteData.Values["category"];
            IEnumerable<string> categories = repository
            .Products
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x);
            return PartialView("FlexMenu", categories);
        }
    }
}