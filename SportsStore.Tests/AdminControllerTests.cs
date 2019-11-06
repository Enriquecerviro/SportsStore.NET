using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Tests
{
    
    public class AdminControllerTests
    {
        /// <summary>
        /// Indexes the contains all products.
        /// Me voy a preocupar que el método Index() devulve correctamente
        /// los objetos producto que hay en el repositorio, para ello puedo textearlo
        /// creando un "mock" repositorio y comparo lo devuelto con el mock
        /// </summary>
        [Fact]
        public void Index_Contains_All_Products()
        {
            //ARRANGE - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P2" },
                new Product {ProductID = 3, Name = "P3" },
            }.AsQueryable<Product>());
            //ARRANGE - create a controller
            AdminController target = new AdminController(mock.Object);

            //ACTION
            Product[] result
                = GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();

            //ASSERT
            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
        }
        [Fact]
        public void Can_Edit_Product()
        {
            //ARRANGE - create mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P2" },
                new Product {ProductID = 3, Name = "P3" },
            }.AsQueryable<Product>());
            //ARRANGE - create the controller
            AdminController target = new AdminController(mock.Object);

            //ACT
            Product p1 = GetViewModel<Product>(target.Edit(1));
            Product p2 = GetViewModel<Product>(target.Edit(2));
            Product p3 = GetViewModel<Product>(target.Edit(3));

            //ASSERT
            Assert.Equal(1, p1.ProductID);
            Assert.Equal(2, p2.ProductID);
            Assert.Equal(3, p3.ProductID);

        }
        [Fact]
        public void Cannot_Edit_Nonexisten_Product()
        {
            //ARRANGE - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P2" },
                new Product {ProductID = 3, Name = "P3" },
            }.AsQueryable<Product>());

            //ARRANGE - create the controller
            AdminController target = new AdminController(mock.Object);
            //ACT
            Product result = GetViewModel<Product>(target.Edit(4));
            //ASSERT
            Assert.Null(result);
        }



        /// <summary>
        /// para sacar el resultado del action method y recibir la vista del
        /// modelo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        private T GetViewModel<T>(IActionResult result) where T: class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
