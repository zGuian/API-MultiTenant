using GearCore.Monolith.Core.UserCore.DTOs;
using GearCore.Monolith.Core.UserCore.Entities;
using GearCore.Monolith.Core.UserCore.Interfaces.Services;
using GearCore.Monolith.Infra.CC.TokenJwt.Interfaces;
using GearCore.Monolith.WebApi.Controllers;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GearCore.Monolith.Tests.Unit.UserTests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserCommand> _commandMock;
        private readonly Mock<IUserQuery> _queryMock;
        private readonly Mock<IJwtServices> _jwtMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly UserController _controller;

        public UserControllerTests()
        {
            _commandMock = new Mock<IUserCommand>();
            _queryMock = new Mock<IUserQuery>();
            _jwtMock = new Mock<IJwtServices>();
            _mapperMock = new Mock<IMapper>();

            _controller = new UserController(
                _commandMock.Object,
                _queryMock.Object,
                _jwtMock.Object
            );

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        #region GetByFilter
        [Fact]
        public async Task GetByFilter_WhenEmailExists_ShouldReturnOk()
        {
            // Arrange
            var user = new User("John", "Doe", "john@test.com", "123", null);
            var dto = new UserDto { NormalizedEmail = user.Email };

            _queryMock
                .Setup(q => q.FindByEmailAsync(user.Email))
                .ReturnsAsync(user);

            _mapperMock
                .Setup(m => m.Map<UserDto>(user))
                .Returns(dto);

            // Act
            var result = await _controller.GetByFilter(_mapperMock.Object, user.Email);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(dto, ok.Value);
        }

        [Fact]
        public async Task GetByFilter_WhenUserNotFound_ShouldReturnNotFound()
        {
            // Arrange
            _queryMock
                .Setup(q => q.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _controller.GetByFilter(_mapperMock.Object, "notfound@test.com");

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion

        #region GetPaged
        [Fact]
        public async Task GetPaged_ShouldReturnOkWithUsers()
        {
            // Arrange
            var users = new List<UserDto>
            {
                new() { NormalizedEmail = "u1@test.com" },
                new() { NormalizedEmail = "u2@test.com" }
            };

            _queryMock
                .Setup(q => q.GetPageAsync(1, 15))
                .ReturnsAsync(users);

            // Act
            var result = await _controller.GetPaged();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(users, ok.Value);
        }
        #endregion

        #region Login
        [Fact]
        public async Task Login_WhenCredentialsAreValid_ShouldReturnTokenAndSetHeaders()
        {
            // Arrange
            var dto = new UserLoginDto
            {
                Email = "john@test.com",
                Password = "123"
            };

            var user = new User("John", "Doe", dto.Email, dto.Password, null);
            var roles = new List<string> { "Admin" };
            var token = "jwt-token";

            _queryMock.Setup(q => q.FindByEmailAsync(dto.Email)).ReturnsAsync(user);
            _queryMock.Setup(q => q.CheckPasswordSignIn(user, dto.Password)).Returns(true);
            _queryMock.Setup(q => q.GetRolesAsync(user)).ReturnsAsync(roles);
            _jwtMock.Setup(j => j.GenerateToken(user, roles)).Returns(token);

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(token, ok.Value);

            Assert.True(_controller.Response.Headers.ContainsKey("X-Auth-AccessToken"));
            Assert.True(_controller.Response.Headers.ContainsKey("X-Auth-RefreshToken"));
        }
        #endregion

        #region Register
        [Fact]
        public async Task Register_WhenUserDoesNotExist_ShouldCreateUser()
        {
            // Arrange
            var dto = new UserRegisterDto
            {
                Email = "new@test.com",
                Password = "123"
            };

            _queryMock
                .Setup(q => q.FindByEmailAsync(dto.Email))
                .ReturnsAsync((User?)null);

            _commandMock
                .Setup(c => c.CreateAsync(dto, "tenant-1", It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Register("tenant-1", dto, CancellationToken.None);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(ok.Value);
        }
        #endregion
    }
}
