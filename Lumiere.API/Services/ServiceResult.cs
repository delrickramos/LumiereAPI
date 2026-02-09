namespace Lumiere.API.Services
{
    public class ServiceResult<T>
    {
        public bool Ok { get; private set; }
        public string? Error { get; private set; }
        public T? Data { get; private set; }

        public static ServiceResult<T> Success(T data)
            => new() { Ok = true, Data = data };

        public static ServiceResult<T> Fail(string error)
            => new() { Ok = false, Error = error };
    }
}
