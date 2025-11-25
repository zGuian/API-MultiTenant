using System.Net;

namespace GearCore.Monolith.Core.Exceptions
{
    public abstract class GearCoreExceptions(string message) : SystemException(message)
    {
        public abstract HttpStatusCode GetStatusCode();
        public abstract string GetStackTrace();
        public abstract string GetInnerException();
    }
}
