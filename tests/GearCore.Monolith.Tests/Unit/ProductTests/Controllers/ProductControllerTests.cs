using GearCore.Monolith.Core.ProductCore.DTOs;
using GearCore.Monolith.Core.ProductCore.Interfaces.Services;
using GearCore.Monolith.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GearCore.Monolith.Tests.Unit.ProductTests.Controllers
{
    public class ProductControllerTests
    {
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _controller = new ProductController();
        }

        #region GetAll
        [Fact]
        public async Task GetAll_ShouldReturnOkWithPaginationAndItems()
        {
            // Arrange
            var products = new List<ProductViewDto>
        {
            new() { Id = "1", Name = "Product 1" },
            new() { Id = "2", Name = "Product 2" }
        };

            var queryMock = new Mock<IProductQuery>();

            queryMock
                .Setup(q => q.GetAllAsync(1, 15, It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);

            queryMock
                .Setup(q => q.CountAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(2);

            // Act
            var result = await _controller.GetAll(queryMock.Object, CancellationToken.None);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);

            var value = ok.Value!;
            var itemsProp = value.GetType().GetProperty("items");

            Assert.NotNull(itemsProp);

            var items = itemsProp!.GetValue(value) as IEnumerable<ProductViewDto>;
            Assert.NotNull(items);
            Assert.Equal(2, ((List<ProductViewDto>)items!).Count);
        }
        #endregion

        #region GetById
        [Fact]
        public async Task GetById_ShouldReturnOkWithProduct()
        {
            // Arrange
            var product = new ProductViewDto
            {
                Id = "prod-1",
                Name = "Product 1"
            };

            var queryMock = new Mock<IProductQuery>();

            queryMock
                .Setup(q => q.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            // Act
            var result = await _controller.GetById(queryMock.Object, product.Id, CancellationToken.None);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product, ok.Value);
        }
        #endregion

        #region Register
        [Fact]
        public async Task Register_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var dto = new ProductRegisterDto
            {
                Name = "New Product"
            };

            var createdProduct = new ProductViewDto
            {
                Id = "prod-1",
                Name = "New Product"
            };

            var commandMock = new Mock<IProductCommand>();

            commandMock
                .Setup(c => c.RegisterAsync(dto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdProduct);

            // Act
            var result = await _controller.Register(commandMock.Object, dto, CancellationToken.None);

            // Assert
            var created = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(ProductController.GetById), created.ActionName);
            Assert.Equal(createdProduct, created.Value);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_ShouldCallCommandAndReturnOk()
        {
            // Arrange
            var dto = new ProductUpdateDto
            {
                Id = "prod-1",
                Name = "Updated Product"
            };

            var commandMock = new Mock<IProductCommand>();

            commandMock
                .Setup(c => c.UpdateAsync(dto, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(commandMock.Object, dto, CancellationToken.None);

            // Assert
            Assert.IsType<OkResult>(result);

            commandMock.Verify(
                c => c.UpdateAsync(dto, It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_ShouldCallCommandAndReturnNoContent()
        {
            // Arrange
            var id = "prod-1";

            var commandMock = new Mock<IProductCommand>();

            commandMock
                .Setup(c => c.DeleteAsync(id, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(commandMock.Object, id, CancellationToken.None);

            // Assert
            Assert.IsType<NoContentResult>(result);

            commandMock.Verify(
                c => c.DeleteAsync(id, It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
        #endregion
    }
}
