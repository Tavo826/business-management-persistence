namespace Domain.Dtos.Response
{
    public class MessageResponseDto
    {
        public Guid Id { get; set; }
        public string MessageId { get; set; }
        public string ReceivedMessage { get; set; }
        public string SenderPhone { get; set; }
        public string ResponseMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
