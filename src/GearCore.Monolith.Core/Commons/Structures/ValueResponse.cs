namespace GearCore.Monolith.Core.Commons.Structures
{
    public struct ValueResponse<T>
    {
        public T? Value;
        public string? Message;

        public ValueResponse()
        {

        }

        public static ValueResponse<T> ReturnSuccess(T value, string message)
        {
            return new ValueResponse<T>
            {
                Value = value,
                Message = message
            };
        }

        public static ValueResponse<T> ReturnFalse(string message, T? value)
        {
            return new ValueResponse<T>
            {
                Value = value,
                Message = message
            };
        }
    }
}
