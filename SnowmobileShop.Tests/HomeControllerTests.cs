

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SnowmobileShop.Controllers;
using SnowmobileShop.Data;
using SnowmobileShop.Models;

namespace SnowmobileShop.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithListOfSnowmobiles()
        {
            // Arrange
            var snowmobiles = new List<Snowmobile>
            {
                new Snowmobile { Id = 1, Name = "Ski-Doo", Model = "MXZ" },
                new Snowmobile { Id = 2, Name = "Arctic Cat", Model = "ZR" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Snowmobile>>();
            mockSet.As<IQueryable<Snowmobile>>().Setup(m => m.Provider).Returns(snowmobiles.Provider);
            mockSet.As<IQueryable<Snowmobile>>().Setup(m => m.Expression).Returns(snowmobiles.Expression);
            mockSet.As<IQueryable<Snowmobile>>().Setup(m => m.ElementType).Returns(snowmobiles.ElementType);
            mockSet.As<IQueryable<Snowmobile>>().Setup(m => m.GetEnumerator()).Returns(snowmobiles.GetEnumerator);


            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(m => m.Snowmobiles).Returns(mockSet.Object);

            var controller = new HomeController(mockContext.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Snowmobile>>(result.Model);
            Assert.Equal(snowmobiles.Count(), model.Count);
        }
    }
}