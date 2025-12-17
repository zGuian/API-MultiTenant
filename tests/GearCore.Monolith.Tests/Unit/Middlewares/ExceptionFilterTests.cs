using GearCore.Monolith.Core.Exceptions;
using GearCore.Monolith.WebApi.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Net;

namespace GearCore.Monolith.Tests.Unit.Middlewares
{
    public class ExceptionFilterTests
    {
        private static ExceptionContext CreateExceptionContext(Exception exception)
        {
            var httpContext = new DefaultHttpContext();

            var actionContext = new ActionContext(
                httpContext,
                new RouteData(),
                new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()
            );

            return new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = exception
            };
        }

        [Fact]
        public void OnException_WhenGearCoreException_ShouldSetStatusCodeAndReturnMessage()
        {
            // Arrange
            var exception = new FakeGearCoreException(
                "Erro de domínio",
                HttpStatusCode.BadRequest
            );

            var context = CreateExceptionContext(exception);
            var filter = new ExceptionFilter();

            // Act
            filter.OnException(context);

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, context.HttpContext.Response.StatusCode);

            var result = Assert.IsType<ObjectResult>(context.Result);

            var messageProperty = result.Value!
                .GetType()
                .GetProperty("Message");

            Assert.NotNull(messageProperty);
            Assert.Equal("Erro de domínio", messageProperty!.GetValue(result.Value));
        }

        [Fact]
        public void OnException_WhenUnknownException_ShouldReturnInternalServerError()
        {
            // Arrange
            var exception = new Exception("Erro inesperado");
            var context = CreateExceptionContext(exception);
            var filter = new ExceptionFilter();

            // Act
            filter.OnException(context);

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, context.HttpContext.Response.StatusCode);

            var result = Assert.IsType<ObjectResult>(context.Result);

            var messageProperty = result.Value!
                .GetType()
                .GetProperty("Message");

            Assert.NotNull(messageProperty);
            Assert.Equal("Erro inesperado", messageProperty!.GetValue(result.Value));
        }

        #region Fake Exception

        private sealed class FakeGearCoreException : GearCoreExceptions
        {
            private readonly HttpStatusCode _statusCode;

            public FakeGearCoreException(string message, HttpStatusCode statusCode)
                : base(message)
            {
                _statusCode = statusCode;
            }

            public override HttpStatusCode GetStatusCode()
                => _statusCode;

            public override string GetStackTrace()
                => "fake-stack-trace";

            public override string GetInnerException()
                => "fake-inner-exception";
        }

        #endregion
    }
}
