namespace Wxt.SportsStore.WebApp.Controllers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using Wxt.SportsStore.Domain.Abstract;
    using Wxt.SportsStore.Domain.Entities;

    [TestClass()]
    public class NavControllerTests
    {
        [TestMethod()]
        public void MenuTest()
        {
            // Arrange
            // - create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, Name = "P1", Category = "Apples"},
                new Product {ProductId = 2, Name = "P2", Category = "Apples"},
                new Product {ProductId = 3, Name = "P3", Category = "Plums"},
                new Product {ProductId = 4, Name = "P4", Category = "Oranges"},
                new Product {ProductId = 5, Name = "P5", Category = "Oranges"},
                new Product {ProductId = 6, Name = "P6", Category = "Apples"},
                new Product {ProductId = 7, Name = "P7", Category = "Plums"},
            });
            // Arrange - create the controller
            NavController target = new NavController(mock.Object);
            // Act = get the set of categories
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();
            // Assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Apples");
            Assert.AreEqual(results[1], "Oranges");
            Assert.AreEqual(results[2], "Plums");
        }

        [TestMethod()]
        public void MenuTestForSelectedCategory()
        {
            // Arrange
            // - create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, Name = "P1", Category = "Apples"},
                new Product {ProductId = 4, Name = "P2", Category = "Oranges"},
            });
            // Arrange - create the controller
            NavController target = new NavController(mock.Object);
            // Arrange - define the category to selected
            string categoryToSelect = "Apples";
            // Action
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;
            // Assert
            Assert.AreEqual(categoryToSelect, result);
        }
    }
}