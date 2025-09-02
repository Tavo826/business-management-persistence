using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.data
{
    [Table("messages")]
    public class MessageData
    {
        [Key]
        public Guid Id { get; set; }
        [Column("message_id")]
        public string MessageId { get; set; } = string.Empty;
        [Column("received_message")]
        public string ReceivedMessage { get; set; } = string.Empty;
        [Column("sender_phone")]
        public string SenderPhone { get; set; } = string.Empty;
        [Column("response_message")]
        public string ResponseMessage { get; set; } = string.Empty;
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
