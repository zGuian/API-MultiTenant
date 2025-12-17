using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.TenantCore.DTOs;
using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.TenantCore.Interfaces.Repositories;
using GearCore.Monolith.Core.TenantCore.Services;
using GearCore.Monolith.Core.UserCore.Entities;
using Moq;

namespace GearCore.Monolith.Tests.Unit.TenantTests.Services
{
    public class TenantCommandTests
    {
        private readonly Mock<ITenantCommandRepository> _commandRepositoryMock;
        private readonly Mock<ITenantQueryRepository> _queryRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly TenantCommand _tenantCommand;

        public TenantCommandTests()
        {
            _commandRepositoryMock = new Mock<ITenantCommandRepository>();
            _queryRepositoryMock = new Mock<ITenantQueryRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _tenantCommand = new TenantCommand(
                _commandRepositoryMock.Object,
                _queryRepositoryMock.Object,
                _unitOfWorkMock.Object
            );
        }

        [Fact]
        public async Task RegisterAsync_ShouldAdaptDtoAndCallRepository()
        {
            // Arrange
            var dto = new TenantRegisterDto
            {
                Name = "Tenant Test",
                Subdomain = "tenant-test"
            };

            // Act
            await _tenantCommand.RegisterAsync(dto);

            // Assert
            _commandRepositoryMock.Verify(
                r => r.RegisterAsync(It.Is<Tenant>(t =>
                    t.Name == dto.Name &&
                    t.Subdomain == dto.Subdomain
                )),
                Times.Once
            );
        }

        [Fact]
        public async Task LinkToUserAsync_ShouldCallRepositoryWithCorrectParameters()
        {
            // Arrange
            var user = new User(
                "John",
                "Doe",
                "john@test.com",
                "123456",
                "11999999999"
            );

            var tenantId = "tenant-123";

            // Act
            await _tenantCommand.LinkToUserAsync(user, tenantId);

            // Assert
            _commandRepositoryMock.Verify(
                r => r.LinkToUserAsync(user, tenantId),
                Times.Once
            );
        }
    }
}
