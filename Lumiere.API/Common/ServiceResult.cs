namespace Lumiere.API.Services
{
    public class ServiceResult<T>
    {
        public bool Ok { get; private set; }
        public string? Error { get; private set; }
        public T? Data { get; private set; }
        public int StatusCode { get; private set }

        public static ServiceResult<T> Success(T data, int statusCode = 200)
            => new() { Ok = true, Data = data, StatusCode = statusCode };  

        public static ServiceResult<T> Fail(string error, int statusCode = 400)
            => new() { Ok = false, Error = error, StatusCode = statusCode };
    }
}
