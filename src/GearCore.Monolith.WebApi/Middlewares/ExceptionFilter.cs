using GearCore.Monolith.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GearCore.Monolith.WebApi.Middlewares
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is GearCoreExceptions gearCoreExceptions)
                HandleProjectException(gearCoreExceptions, context);
            else
                ThrowUnknowException(context);
        }

        private static void HandleProjectException(GearCoreExceptions gearCoreExceptions, ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)gearCoreExceptions.GetStatusCode();
            context.Result = new ObjectResult(new
            {
                gearCoreExceptions.Message,
                gearCoreExceptions.InnerException,
                gearCoreExceptions.StackTrace
            });
        }

        private static void ThrowUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new
            {
                context.Exception.Message,
                context.Exception.InnerException,
                context.Exception.StackTrace
            });
        }
    }
}
