﻿namespace Domain.Dtos.Response
{
    public class ResponseDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }
}
