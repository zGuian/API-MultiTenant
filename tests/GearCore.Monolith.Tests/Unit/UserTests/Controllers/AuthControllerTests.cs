using GearCore.Monolith.Core.UserCore.DTOs;
using GearCore.Monolith.Core.UserCore.Entities;
using GearCore.Monolith.Core.UserCore.Interfaces.Services;
using GearCore.Monolith.Infra.Data.Commons.TokenJwt.Interfaces;
using GearCore.Monolith.WebApi.Controllers;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GearCore.Monolith.Tests.Unit.UserTests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IUserCommand> _commandMock;
        private readonly Mock<IUserQuery> _queryMock;
        private readonly Mock<IJwtServices> _jwtMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _commandMock = new Mock<IUserCommand>();
            _queryMock = new Mock<IUserQuery>();
            _jwtMock = new Mock<IJwtServices>();
            _mapperMock = new Mock<IMapper>();

            _controller = new AuthController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
        }

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
            var result = await _controller.Login(_queryMock.Object, _jwtMock.Object, dto);

            // Assert - valida o tipo do retorno
            var okResult = Assert.IsType<OkObjectResult>(result);

            dynamic value = okResult.Value!;

            Assert.Equal(user.NormalizedFirstName, value.FirstName);
            Assert.Equal(user.NormalizedLastName, value.LastName);
            Assert.Equal(token, value.AccessToken);
            Assert.False(string.IsNullOrEmpty((string)value.RefreshToken));

            // Valida headers
            Assert.True(_controller.Response.Headers.ContainsKey("X-Auth-AccessToken"));
            Assert.True(_controller.Response.Headers.ContainsKey("X-Auth-RefreshToken"));

            Assert.Equal(token, _controller.Response.Headers["X-Auth-AccessToken"]);
        }
        #endregion
    }
}
