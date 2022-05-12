using Moq;
using Xunit;
using codetest.Services;
using codetest;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using System.Collections.Concurrent;
namespace codetests.Tests
{
    //a few simple tests
    public class CheeseyDataServiceTests
    {
        [Fact]
        public async void ListProductsTest()
        {
            var logger = new Mock<ILogger<CheeseyDataService>>();
            var controller = new CheeseyDataService(logger.Object);
            var res = await controller.ListProducts();
            Assert.Equal(5, res.Count());
        }

        [Fact]
        public async void AddToOrderTest()
        {
            //or use this short equivalent
            var logger = new Mock<ILogger<CheeseyDataService>>();
            var controller = new CheeseyDataService(logger.Object);
            CheeseOrder cheese = new CheeseOrder()
            {
                CheeseId = 1,
                Amount = 10.0,
                Cost = 100.0
            };
            var res = await controller.AddToOrder(cheese);

            Assert.True(res);
        }
        [Fact]
        public async void AddToOrderFalseTest()
        {
            //or use this short equivalent
            var logger = new Mock<ILogger<CheeseyDataService>>();
            var controller = new CheeseyDataService(logger.Object);
            CheeseOrder cheese = new CheeseOrder()
            {
                CheeseId = 0,
                Amount = 10.0,
                Cost = 100.0
            };
            var res = await controller.AddToOrder(cheese);

            Assert.False(res);
        }
    }

}