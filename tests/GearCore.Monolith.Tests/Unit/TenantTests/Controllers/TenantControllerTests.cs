using GearCore.Monolith.Core.TenantCore.DTOs;
using GearCore.Monolith.Core.TenantCore.Interfaces.Services;
using GearCore.Monolith.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GearCore.Monolith.Tests.Unit.TenantTests.Controllers
{
    public class TenantControllerTests
    {
        private readonly TenantController _controller;

        public TenantControllerTests()
        {
            _controller = new TenantController();
        }

        #region GetAll
        [Fact]
        public async Task GetAll_ShouldReturnOkWithTenants()
        {
            // Arrange
            var tenants = new HashSet<TenantViewDto?>
            {
                new() { Id = "1", Name = "Tenant One" },
                new() { Id = "2", Name = "Tenant Two" }
            };

            var queryMock = new Mock<ITenantQuery>();
            queryMock
                .Setup(q => q.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(tenants);

            // Act
            var result = await _controller.GetAll(queryMock.Object, CancellationToken.None);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);

            var value = Assert.IsAssignableFrom<IEnumerable<TenantViewDto?>>(ok.Value);

            Assert.Equal(2, value.Count());
            Assert.Contains(value, t => t!.Id == "1");
            Assert.Contains(value, t => t!.Id == "2");
        }
        #endregion

        #region GetByIdAsync
        [Fact]
        public async Task GetByIdAsync_ShouldReturnOkWithTenant()
        {
            // Arrange
            var tenantId = "tenant-1";
            var tenantDto = new TenantViewDto
            {
                Id = tenantId,
                Name = "Tenant One",
                Subdomain = "tenant-one"
            };

            var queryMock = new Mock<ITenantQuery>();
            queryMock
                .Setup(q => q.GetByIdAsync(tenantId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(tenantDto);

            // Act
            var result = await _controller.GetByIdAsync(queryMock.Object, tenantId, CancellationToken.None);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(tenantDto, ok.Value);
        }
        #endregion

        #region Register
        [Fact]
        public async Task Register_ShouldCallCommandAndReturnOk()
        {
            // Arrange
            var dto = new TenantRegisterDto
            {
                Name = "Tenant Test",
                Subdomain = "tenant-test"
            };

            var commandMock = new Mock<ITenantCommand>();

            commandMock
                .Setup(c => c.RegisterAsync(dto))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Register(commandMock.Object, dto);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Registrado com sucesso", ok.Value);

            commandMock.Verify(
                c => c.RegisterAsync(dto),
                Times.Once
            );
        }
        #endregion
    }
}
