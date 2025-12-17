using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.TenantCore.Interfaces.Services;
using GearCore.Monolith.Core.UserCore.DTOs;
using GearCore.Monolith.Core.UserCore.Entities;
using GearCore.Monolith.Core.UserCore.Interfaces.Repositories;
using GearCore.Monolith.Core.UserCore.Services;
using MapsterMapper;
using Moq;

namespace GearCore.Monolith.Tests.Unit.UserTests.Services
{
    public class UserCommandTests
    {
        private readonly Mock<IUserCommandRepository> _commandRepositoryMock;
        private readonly Mock<ITenantCommand> _tenantCommandMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly UserCommand _userCommand;

        public UserCommandTests()
        {
            _commandRepositoryMock = new Mock<IUserCommandRepository>();
            _tenantCommandMock = new Mock<ITenantCommand>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();

            _userCommand = new UserCommand(
                _commandRepositoryMock.Object,
                _tenantCommandMock.Object,
                _unitOfWorkMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task CreateAsync_WhenMapperReturnsUser_ShouldCreateUserAndLinkTenant()
        {
            // Arrange
            var dto = new UserRegisterDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@test.com",
                Password = "123456",
                PhoneNumber = "11999999999"
            };

            var tenantId = "tenant-123";

            var user = new User(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                dto.Password,
                dto.PhoneNumber
            );

            _mapperMock
                .Setup(m => m.Map<User>(dto))
                .Returns(user);

            // Act
            var result = await _userCommand.CreateAsync(dto, tenantId);

            // Assert
            Assert.True(result);

            _commandRepositoryMock.Verify(
                r => r.InsertAsync(user, It.IsAny<CancellationToken>()),
                Times.Once
            );

            _tenantCommandMock.Verify(
                t => t.LinkToUserAsync(user, tenantId),
                Times.Once
            );

            _unitOfWorkMock.Verify(
                u => u.CommitAsync(It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [Fact]
        public async Task CreateAsync_WhenMapperReturnsNull_ShouldReturnFalseAndNotCallDependencies()
        {
            // Arrange
            var dto = new UserRegisterDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@test.com",
                Password = "123456"
            };

            _mapperMock
                .Setup(m => m.Map<User>(dto))
                .Returns((User?)null);

            // Act
            var result = await _userCommand.CreateAsync(dto, "tenant-123");

            // Assert
            Assert.False(result);

            _commandRepositoryMock.Verify(
                r => r.InsertAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()),
                Times.Never
            );

            _tenantCommandMock.Verify(
                t => t.LinkToUserAsync(It.IsAny<User>(), It.IsAny<string>()),
                Times.Never
            );

            _unitOfWorkMock.Verify(
                u => u.CommitAsync(It.IsAny<CancellationToken>()),
                Times.Never
            );
        }
    }
}
