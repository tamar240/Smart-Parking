namespace SmartParking
{
    public class OperationResult<T>
    {
        public bool Success { get;private set; }
        public string? Message { get; private set; }
        public T? Data { get; private set; }

        public static OperationResult<T> Ok(T data, string msg = "") =>
            new() { Success = true, Data = data, Message = msg };

        public static OperationResult<T> Fail(string msg) =>
            new() { Success = false, Message = msg };
       
    }

}
