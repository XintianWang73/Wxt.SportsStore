namespace Wxt.SportsStore.WebApp.Controllers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Linq;
    using System.Web.Mvc;
    using Wxt.SportsStore.Domain.Abstract;
    using Wxt.SportsStore.Domain.Entities;
    using Wxt.SportsStore.WebApp.Controllers;
    using Wxt.SportsStore.WebApp.Models;

    [TestClass()]
    public class CartControllerTests
    {
        [TestMethod()]
        public void AddToCartTest()
        {
            // Arrange - create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, Name = "P1", Category = "Apples"},
            }.AsQueryable());
            // Arrange - create a Cart
            Cart cart = new Cart();
            // Arrange - create the controller
            CartController target = new CartController(mock.Object, null);
            // Act - add a product to the cart
            var result = target.AddToCart(cart, 1, "MyUrl");
            // Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductId, 1);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("MyUrl", result.RouteValues["returnUrl"]);
        }

        [TestMethod()]
        public void IndexTest()
        {
            // Arrange - create a Cart
            Cart cart = new Cart();
            // Arrange - create the controller
            CartController target = new CartController(null, null);
            // Act - call the Index action method
            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;
            // Assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }

        [TestMethod()]
        public void CheckoutTestForEmptyCart()
        {
            // Arrange - create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            // Arrange - create a Cart
            Cart cart = new Cart();
            // Arrange - create the controller
            CartController target = new CartController(null, mock.Object);
            // Arrange - create shipping details
            ShippingDetails shippingDetails = new ShippingDetails();
            // Act
            ViewResult result = target.Checkout(cart, shippingDetails);
            // Assert - check that the order hasn't been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());
            // Assert - check that the method is returning the default view
            Assert.AreEqual("", result.ViewName);
            // Assert - check that I am passing an invalid model to the view
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod()]
        public void CheckoutTestForInvalidShippingDetail()
        {
            // Arrange - create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            // Arrange - create a cart with an item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            // Arrange - create an instance of the controller
            CartController target = new CartController(null, mock.Object);
            // Arrange - add an error to the model
            target.ModelState.AddModelError("Name", "error");
            // Act - try to checkout
            ViewResult result = target.Checkout(cart, new ShippingDetails());
            // Assert - check that the order hasn't been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());
            // Assert - check that the method is returning the default view
            Assert.AreEqual("", result.ViewName);
            // Assert - check that I am passing an invalid model to the view
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod()]
        public void CheckoutTestForValid()
        {
            // Arrange - create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            // Arrange - create a cart with an item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            // Arrange - create an instance of the controller
            CartController target = new CartController(null, mock.Object);
            // Act - try to checkout
            ViewResult result = target.Checkout(cart, new ShippingDetails());
            // Assert - check that the order has been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());
            // Assert - check that the method is returning the Completed view
            Assert.AreEqual("Completed", result.ViewName);
            // Assert - check that I am passing a valid model to the view
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }
    }
}
