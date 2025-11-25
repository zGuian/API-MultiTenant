using System.Net;

namespace GearCore.Monolith.Core.Exceptions
{
    public class NotFoundException(string message) : GearCoreExceptions(message)
    {
        public override string GetInnerException()
        {
            if (InnerException != null)
            {
                return InnerException.ToString();
            }
            return string.Empty;
        }

        public override string GetStackTrace()
        {
            if (StackTrace != null)
            {
                return StackTrace;
            }
            return string.Empty;
        }

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}
