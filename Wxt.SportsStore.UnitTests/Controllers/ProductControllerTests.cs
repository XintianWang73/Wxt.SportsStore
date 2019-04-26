namespace Wxt.SportsStore.WebApp.Controllers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Wxt.SportsStore.WebApp.Controllers;
    using System.Linq;
    using Wxt.SportsStore.Domain.Abstract;
    using Moq;
    using Wxt.SportsStore.Domain.Entities;
    using Wxt.SportsStore.WebApp.Models;

    [TestClass()]
    public class ProductControllerTests
    {
        [TestMethod()]
        public void ListTestForPaging()
        {
            // Arrange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
                new Product {ProductId = 4, Name = "P4"},
                new Product {ProductId = 5, Name = "P5"}
            });
            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };
            // Act
            var result = (controller.List(null, 1).Model as ProductsListViewModel).Products;
            // Assert
            Product[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 3);
            Assert.AreEqual(prodArray[0].Name, "P1");
            Assert.AreEqual(prodArray[1].Name, "P2");
            Assert.AreEqual(prodArray[2].Name, "P3");

            // Act
            result = (controller.List(null, 2).Model as ProductsListViewModel).Products;
            // Assert
            prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
            // Act
            result = (controller.List(null, 3).Model as ProductsListViewModel).Products;
            // Assert
            prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 0);
        }

        [TestMethod()]
        public void ListTestForCategory()
        {
            // Arrange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, Name = "P1", Category = "aaa"},
                new Product {ProductId = 2, Name = "P2", Category = "bbb"},
                new Product {ProductId = 3, Name = "P3", Category = "bbb"},
                new Product {ProductId = 4, Name = "P4", Category = "bbb"},
                new Product {ProductId = 5, Name = "P5", Category = "bbb"}
            });
            // Arrange
            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };
            // Act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;
            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);

            // Act
            result = (ProductsListViewModel)controller.List("aaa", 1).Model;
            // Assert
            pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 1);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 1);
            Assert.AreEqual(pageInfo.TotalPages, 1);

            // Act
            result = (ProductsListViewModel)controller.List("bbb", 2).Model;
            // Assert
            pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 4);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
    }
}