using GearCore.Monolith.WebApi.Middlewares;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GearCore.Monolith.Tests.Unit.Middlewares
{
    public class TenantMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_WhenTenantIdIsInHeader_ShouldSetTenantAndCallNext()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.Headers["X-Tenant-ID"] = "tenant-header";

            bool nextCalled = false;

            RequestDelegate next = ctx =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            };

            var middleware = new TenantMiddleware(next);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.True(nextCalled);
            Assert.True(context.Items.ContainsKey("TenantId"));
            Assert.Equal("tenant-header", context.Items["TenantId"]);
        }

        [Fact]
        public async Task InvokeAsync_WhenTenantIdIsInClaim_ShouldSetTenantAndCallNext()
        {
            // Arrange
            var context = new DefaultHttpContext();

            var identity = new ClaimsIdentity(new[]
            {
                new Claim("tenant", "tenant-claim")
            }, "TestAuth");

            context.User = new ClaimsPrincipal(identity);

            bool nextCalled = false;

            RequestDelegate next = ctx =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            };

            var middleware = new TenantMiddleware(next);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.True(nextCalled);
            Assert.True(context.Items.ContainsKey("TenantId"));
            Assert.Equal("tenant-claim", context.Items["TenantId"]);
        }

        [Fact]
        public async Task InvokeAsync_WhenTenantIdIsMissing_ShouldReturnBadRequestAndNotCallNext()
        {
            // Arrange
            var context = new DefaultHttpContext();

            var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            bool nextCalled = false;

            RequestDelegate next = ctx =>
            {
                nextCalled = true;
                return Task.CompletedTask;
            };

            var middleware = new TenantMiddleware(next);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.False(nextCalled);
            Assert.Equal(StatusCodes.Status400BadRequest, context.Response.StatusCode);

            responseBody.Seek(0, SeekOrigin.Begin);
            var bodyText = await new StreamReader(responseBody).ReadToEndAsync();

            Assert.Equal("TenantId is missing.", bodyText);
        }
    }
}
