namespace WebAPI.Services
{
    /// <summary>
    /// Описывает результат работы сервиса.
    /// </summary>
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Description { get; set; } = string.Empty;
        public ServiceResult? InnerServiceResult { get; set; }

        public static ServiceResult Ok(string description) => new() { Success = true, Description = description };
        public static ServiceResult Ok(string description, ServiceResult innerServiceResult)
            => new() { Success = true, Description = description, InnerServiceResult = innerServiceResult };

        public static ServiceResult Fail(string description) => new() { Success = false, Description = description };
        public static ServiceResult Fail(string description, ServiceResult innerServiceResult)
            => new() { Success = false, Description = description, InnerServiceResult = innerServiceResult };
    }

    /// <summary>
    /// Описывает результат работы сервиса и может даже что-то вернуть.
    /// </summary>
    /// <typeparam name="T">Тип возвращаемого значения</typeparam>
    public class ServiceResult<T> : ServiceResult
    {
        public T? Value { get; set; }

        public static ServiceResult<T> Ok(string description, T value) => new() { Success = true, Description = description, Value = value };
        public static ServiceResult<T> Ok(string description, T value, ServiceResult innerServiceResult)
            => new() { Success = true, Description = description, Value = value , InnerServiceResult = innerServiceResult };

        public static ServiceResult<T> Fail(string description, T value) => new() { Success = false, Description = description, Value = value };
        public static ServiceResult<T> Fail(string description, T value,  ServiceResult innerServiceResult)
            => new() { Success = false, Value = value, Description = description, InnerServiceResult = innerServiceResult };
    }
}
