using GearCore.Monolith.WebApi.Attributes;
using GearCore.Monolith.WebApi.Middlewares;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GearCore.Monolith.Tests.Unit.Middlewares
{
    public class TenantMiddlewareTests
    {
        private static DefaultHttpContext CreateContextWithEndpoint(bool requireTenant)
        {
            var context = new DefaultHttpContext();

            Endpoint? endpoint = null;

            if (requireTenant)
            {
                endpoint = new Endpoint(
                    _ => Task.CompletedTask,
                    new EndpointMetadataCollection(new RequireTenantAttribute()),
                    "Test endpoint with tenant"
                );
            }
            else
            {
                endpoint = new Endpoint(
                    _ => Task.CompletedTask,
                    new EndpointMetadataCollection(),
                    "Test endpoint without tenant"
                );
            }

            context.SetEndpoint(endpoint);
            return context;
        }

        [Fact]
        public async Task InvokeAsync_WhenEndpointDoesNotRequireTenant_ShouldCallNextAndIgnoreTenant()
        {
            // Arrange
            var context = CreateContextWithEndpoint(requireTenant: false);

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
            Assert.False(context.Items.ContainsKey("TenantID"));
        }

        [Fact]
        public async Task InvokeAsync_WhenTenantInHeader_ShouldSetTenantAndCallNext()
        {
            // Arrange
            var context = CreateContextWithEndpoint(requireTenant: true);
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
            Assert.Equal("tenant-header", context.Items["TenantID"]);
        }

        [Fact]
        public async Task InvokeAsync_WhenTenantInClaim_ShouldSetTenantAndCallNext()
        {
            // Arrange
            var context = CreateContextWithEndpoint(requireTenant: true);

            var identity = new ClaimsIdentity(
                new[] { new Claim("tenant", "tenant-claim") },
                "TestAuth"
            );

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
            Assert.Equal("tenant-claim", context.Items["TenantID"]);
        }

        [Fact]
        public async Task InvokeAsync_WhenTenantIsMissingAndEndpointRequiresTenant_ShouldReturnBadRequest()
        {
            // Arrange
            var context = CreateContextWithEndpoint(requireTenant: true);

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
            var body = await new StreamReader(responseBody).ReadToEndAsync();

            Assert.Equal("TenantId is missing.", body);
        }
    }
}
