using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Request
{
    public class MessageDto
    {
        [Required]
        public string MessageId { get; set; }
        [Required]
        [StringLength(500)]
        public string ReceivedMessage { get; set; }
        [Required]
        public string SenderPhone { get; set; }
        [Required]
        public string ResponseMessage { get; set; }
    }
}
