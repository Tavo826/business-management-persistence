namespace Domain.Dtos.Response
{
    public class ErrorResponseDto
    {
        public string Title { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
