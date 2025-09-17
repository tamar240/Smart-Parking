namespace SmartParking
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static OperationResult<T> Ok(T data, string msg = "") =>
            new() { Success = true, Data = data, Message = msg };

        public static OperationResult<T> Fail(string msg) =>
            new() { Success = false, Message = msg };

        public T GetValueOrThrow()
        {
            if (!Success) throw new InvalidOperationException(Message);
            return Data!;
        }
    }

}
