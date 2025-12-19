using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.ProductCore.DTOs;
using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.ProductCore.Interfaces.Repositories;
using GearCore.Monolith.Core.ProductCore.Services;
using MapsterMapper;
using Moq;

namespace GearCore.Monolith.Tests.Unit.ProductTests.Services
{
    public class ProductCommandTests
    {
        private readonly Mock<IProductCommandRepository> _commandRepoMock;
        private readonly Mock<IProductQueryRepository> _queryRepoMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly ProductCommand _command;

        public ProductCommandTests()
        {
            _commandRepoMock = new Mock<IProductCommandRepository>();
            _queryRepoMock = new Mock<IProductQueryRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();

            _command = new ProductCommand(
                _commandRepoMock.Object,
                _queryRepoMock.Object,
                _unitOfWorkMock.Object,
                _mapperMock.Object
            );
        }

        #region RegisterAsync
        [Fact]
        public async Task RegisterAsync_ShouldInsertProductCommitAndReturnViewDto()
        {
            // Arrange
            var dto = new ProductRegisterDto
            {
                Name = "Product 1",
                Price = 10
            };

            _commandRepoMock
                .Setup(r => r.InsertAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock
                .Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _command.RegisterAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);

            _commandRepoMock.Verify(
                r => r.InsertAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()),
                Times.Once
            );

            _unitOfWorkMock.Verify(
                u => u.CommitAsync(It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
        #endregion

        #region UpdateAsync
        [Fact]
        public async Task UpdateAsync_WhenProductExists_ShouldUpdateAndCommit()
        {
            // Arrange
            var product = new Product
            {
                Id = "prod-1",
                Name = "Old Name"
            };

            var dto = new ProductUpdateDto
            {
                Id = product.Id,
                Name = "New Name"
            };

            _queryRepoMock
                .Setup(q => q.GetByIdAsync(dto.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            _unitOfWorkMock
                .Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _command.UpdateAsync(dto);

            // Assert
            Assert.Equal("New Name", product.Name);
            Assert.True(product.UpdatedBy <= DateTime.Now);

            _commandRepoMock.Verify(
                r => r.Update(product),
                Times.Once
            );

            _unitOfWorkMock.Verify(
                u => u.CommitAsync(It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [Fact]
        public async Task UpdateAsync_WhenProductDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var dto = new ProductUpdateDto { Id = "not-found" };

            _queryRepoMock
                .Setup(q => q.GetByIdAsync(dto.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _command.UpdateAsync(dto)
            );
        }
        #endregion

        #region DeleteAsync
        [Fact]
        public async Task DeleteAsync_ShouldDeleteProductAndCommit()
        {
            // Arrange
            var id = "prod-1";

            _unitOfWorkMock
                .Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _command.DeleteAsync(id);

            // Assert
            _commandRepoMock.Verify(
                r => r.Delete(id),
                Times.Once
            );

            _unitOfWorkMock.Verify(
                u => u.CommitAsync(It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
        #endregion
    }
}
