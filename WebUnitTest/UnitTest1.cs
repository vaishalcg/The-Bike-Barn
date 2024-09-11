using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebAPI.Controllers;
using WebAPI.Models;

namespace WebUnitTest
{
    [TestClass]
    public class BrandTest
    {

        private Mock<BikeStores_Team3Entities> _mockDbContext;
        private Mock<DbSet<brand>> _mockBrandSet;
        private brandController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Initialize the mock context and controller
            _mockDbContext = new Mock<BikeStores_Team3Entities>();
            _mockBrandSet = new Mock<DbSet<brand>>();

            _controller = new brandController
            {
                db = _mockDbContext.Object
            };
        }
        [TestMethod]
        public void Getorder_items_ReturnsAllOrderItems()
        {
            // Arrange
            var mockBrand = new List<brand>
            {
                new brand { order_id = 1, item_id = 1, product_id = 20, quantity = 5, list_price = 500, discount = 0.5m },
                new brand { order_id = 1, item_id = 2, product_id = 8, quantity = 2, list_price = 1799, discount = 0.07m }
            }.AsQueryable();

            _mockBrandSet.As<IQueryable<order_items>>().Setup(m => m.Provider).Returns(mockBrand.Provider);
            _mockBrandSet.As<IQueryable<order_items>>().Setup(m => m.Expression).Returns(mockBrand.Expression);
            _mockBrandSet.As<IQueryable<order_items>>().Setup(m => m.ElementType).Returns(mockBrand.ElementType);
            _mockBrandSet.As<IQueryable<order_items>>().Setup(m => m.GetEnumerator()).Returns(brand.GetEnumerator());

            _mockDbContext.Setup(db => db.order_items).Returns(_mockBrandSet.Object);

            // Act
            var result = _controller.Getorder_items() as IEnumerable<order_items>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
    }
}
