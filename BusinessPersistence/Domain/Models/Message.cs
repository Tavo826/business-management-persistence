using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }
        public string MessageId { get; set; } = string.Empty;
        public string ReceivedMessage { get; set; } = string.Empty;
        public string SenderPhone { get; set; } = string.Empty;
        public string ResponseMessage { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
