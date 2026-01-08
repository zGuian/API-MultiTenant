using GearCore.Monolith.Core.UserCore.Entities;
using GearCore.Monolith.Core.UserCore.Interfaces.Repositories;
using GearCore.Monolith.Core.UserCore.Services;
using Moq;

namespace GearCore.Monolith.Tests.Unit.UserTests.Services
{
    public class UserQueryTests
    {
        private readonly Mock<IUserQueryRepository> _repositoryMock;
        private readonly UserQuery _userQuery;

        public UserQueryTests()
        {
            _repositoryMock = new Mock<IUserQueryRepository>();
            _userQuery = new UserQuery(_repositoryMock.Object);
        }

        #region CheckPasswordSignIn
        [Fact]
        public void CheckPasswordSignIn_WhenPasswordIsCorrect_ShouldReturnTrue()
        {
            // Arrange
            var password = "123456";
            var user = new User(
                "John",
                "Doe",
                "john@test.com",
                password,
                null
            );

            // Act
            var result = _userQuery.CheckPasswordSignIn(user, password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckPasswordSignIn_WhenPasswordIsInvalid_ShouldReturnFalse()
        {
            // Arrange
            var user = new User(
                "John",
                "Doe",
                "john@test.com",
                "correct-password",
                null
            );

            // Act
            var result = _userQuery.CheckPasswordSignIn(user, "wrong-password");

            // Assert
            Assert.False(result);
        }
        #endregion

        #region FindByEmailAsync
        [Fact]
        public async Task FindByEmailAsync_WhenUserExists_ShouldReturnUser()
        {
            // Arrange
            var email = "john@test.com";
            var user = new User(
                "John",
                "Doe",
                email,
                "123456",
                null
            );

            _repositoryMock
                .Setup(r => r.FindByEmailAsync(email))
                .ReturnsAsync(user);

            // Act
            var result = await _userQuery.FindByEmailAsync(email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(email, result!.Email);
            Assert.Equal(email.ToUpper(), result.NormalizedEmail);
        }

        [Fact]
        public async Task FindByEmailAsync_WhenUserDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _userQuery.FindByEmailAsync("notfound@test.com");

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region FindByNameAsync
        [Fact]
        public async Task FindByNameAsync_WhenUserExists_ShouldReturnUser()
        {
            // Arrange
            var name = "JOHN DOE";
            var user = new User(
                "John",
                "Doe",
                "john@test.com",
                "123456",
                null
            );

            _repositoryMock
                .Setup(r => r.FindByNameAsync(name))
                .ReturnsAsync(user);

            // Act
            var result = await _userQuery.FindByNameAsync(name);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("JOHN DOE", result!.CompleteName);
        }
        #endregion

        #region GetPageAsync
        [Fact]
        public async Task GetPageAsync_ShouldReturnMappedUserDtos()
        {
            // Arrange
            var users = new List<User>
        {
            new("John", "Doe", "john@test.com", "123", null),
            new("Jane", "Smith", "jane@test.com", "123", null)
        };

            _repositoryMock
                .Setup(r => r.GetPageAsync(0, 10))
                .ReturnsAsync(users);

            // Act
            var result = await _userQuery.GetPageAsync(0, 10);

            // Assert
            var list = result.ToList();

            Assert.Equal(2, list.Count);
            Assert.Equal("JOHN DOE", list[0].CompleteName);
            Assert.Equal("JANE SMITH", list[1].CompleteName);
        }
        #endregion

        #region GetRolesAsync
        [Fact]
        public async Task GetRolesAsync_ShouldReturnUserRoles()
        {
            // Arrange
            var user = new User(
                "John",
                "Doe",
                "john@test.com",
                "123456",
                null
            );

            var roles = new List<string> { "Admin", "User" };

            _repositoryMock
                .Setup(r => r.GetRolesAsync(user.Id))
                .ReturnsAsync(roles);

            // Act
            var result = await _userQuery.GetRolesAsync(user);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains("Admin", result);
            Assert.Contains("User", result);
        }
        #endregion
    }
}
