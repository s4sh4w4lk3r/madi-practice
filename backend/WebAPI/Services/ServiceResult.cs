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
}
