using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Request
{
    public class MessageRequestDto
    {
        [Required]
        public string MessageId { get; set; } = string.Empty;
        [Required]
        [StringLength(500)]
        public string ReceivedMessage { get; set; } = string.Empty;
        [Required]
        public string SenderPhone { get; set; } = string.Empty;
        [Required]
        public string ResponseMessage { get; set; } = string.Empty;
    }
}
