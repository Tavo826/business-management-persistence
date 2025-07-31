namespace Domain.Dtos.Response
{
    public class ErrorResponseDto
    {
        public string Title { get; set; } = string.Empty;
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
    }
}
