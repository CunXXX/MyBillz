namespace DTOs;

public class BaseModel
{
    /// <summary>
    /// API回傳
    /// </summary>
    public class ApiResponse<T>
    {
        public ApiStatus Status { get; set; } = new ApiStatus();
        public T? Data { get; set; } = default;
    }

    public class ApiStatus
    {
        public int Code { get; set; } = 0;  // 0:成功, 其他:失敗
        public string Message { get; set; } = "Success";
    }
}
