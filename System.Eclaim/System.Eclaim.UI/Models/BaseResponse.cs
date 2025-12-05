namespace System.Eclaim.UI.Models
{
    public class BaseResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string? ErrorCode { get; set; }
        public T? Result { get; set; }

    }
}
