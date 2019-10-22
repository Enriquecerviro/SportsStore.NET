using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Tests
{
    /// <summary>
    /// the unit test for my ability to produce a category list is relatively simple. the goal is to create a list thatis sorted in alphabetical order and contains no duplicates, and the simplest way to do this is to supply
    /// some test data that does have duplicate categories and that is not in order, pass this to the tag helper 
    /// class, and assert that the data has been properly cleaned u
    /// </summary>
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            //ARRANGE
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                new Product {ProductID = 2, Name = "P2", Category = "Apples"},
                new Product {ProductID = 3, Name = "P3", Category = "Plums"},
                new Product {ProductID = 4, Name = "P4", Category = "Oranges"},
            }).AsQueryable<Product>());

            NavigationMenuViewComponent target =
                new NavigationMenuViewComponent(mock.Object);
            // Act = get the set of categories
            string[] results = ((IEnumerable<string>)(target.Invoke()
                as ViewViewComponentResult).ViewData.Model).ToArray();
            //ASSERT
            Assert.True(Enumerable.SequenceEqual(new string[]
            {
                "Apples",
                "Oranges",
                "Plums",
            },
            results));
        }
        void Indicates_Selected_Category()
        {
            // Arrange
            string categoryToSelect = "Apples";
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
            new Product {ProductID = 1, Name = "P1", Category = "Apples"},
            new Product {ProductID = 4, Name = "P2", Category = "Oranges"},
         }).AsQueryable<Product>());
            NavigationMenuViewComponent target =
            new NavigationMenuViewComponent(mock.Object);
            target.RouteData.Values["category"] = categoryToSelect;
            // Action
            string result = (string)(target.Invoke() as
            ViewViewComponentResult).ViewData["SelectedCategory"];
            // Assert
            Assert.Equal(categoryToSelect, result);
        }
    }
}
