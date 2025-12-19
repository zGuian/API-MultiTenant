using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.ProductCore.Interfaces.Repositories;
using GearCore.Monolith.Core.ProductCore.Services;
using Moq;

namespace GearCore.Monolith.Tests.Unit.ProductTests.Services
{
    public class ProductQueryTests
    {
        private readonly Mock<IProductQueryRepository> _queryRepositoryMock;
        private readonly ProductQuery _productQuery;

        public ProductQueryTests()
        {
            _queryRepositoryMock = new Mock<IProductQueryRepository>();
            _productQuery = new ProductQuery(_queryRepositoryMock.Object);
        }

        #region CountAsync
        [Fact]
        public async Task CountAsync_ShouldReturnTotalFromRepository()
        {
            // Arrange
            _queryRepositoryMock
                .Setup(r => r.CountAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(10);

            // Act
            var result = await _productQuery.CountAsync();

            // Assert
            Assert.Equal(10, result);
        }
        #endregion

        #region GetAllAsync
        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedProductViewDtos()
        {
            // Arrange
            var products = new List<Product>
        {
            new() { Id = "1", Name = "Product 1" },
            new() { Id = "2", Name = "Product 2" }
        };

            _queryRepositoryMock
                .Setup(r => r.GetPagedAsync(1, 10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);

            // Act
            var result = await _productQuery.GetAllAsync(1, 10);

            // Assert
            var list = result.ToList();

            Assert.Equal(2, list.Count);
            Assert.Equal("Product 1", list[0].Name);
            Assert.Equal("Product 2", list[1].Name);
        }
        #endregion

        #region GetByIdAsync
        [Fact]
        public async Task GetByIdAsync_ShouldReturnMappedProductViewDto()
        {
            // Arrange
            var product = new Product
            {
                Id = "prod-1",
                Name = "Product Test"
            };

            _queryRepositoryMock
                .Setup(r => r.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            // Act
            var result = await _productQuery.GetByIdAsync(product.Id, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Name, result.Name);
        }
        #endregion
    }
}
