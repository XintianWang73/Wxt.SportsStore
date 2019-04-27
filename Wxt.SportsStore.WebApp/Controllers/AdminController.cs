namespace Wxt.SportsStore.WebApp.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Wxt.SportsStore.Domain.Abstract;
    using Wxt.SportsStore.Domain.Entities;

    [Authorize]
    public class AdminController : Controller
    {
        private IProductsRepository repository;
        public AdminController(IProductsRepository repo)
        {
            repository = repo;
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            Product product = repository
            .Products
            .FirstOrDefault(p => p.ProductId == productId);
            ViewBag.Title = "Edit " + product.Name;
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (product.Price>10000)
            {
                ModelState.AddModelError("Price", "Too expensive!");
            }
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create()
        {
            ViewBag.Title = "Add new product";
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}