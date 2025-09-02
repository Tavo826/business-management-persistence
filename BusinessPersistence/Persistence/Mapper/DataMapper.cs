using Domain.Models;
using Persistence.data;

namespace Persistence.Mapper
{
    public static class DataMapper
    {
        public static Message toDomain(MessageData messageData)
        {
            if (messageData == null)
                return null;

            return new Message
            {
                Id = messageData.Id,
                MessageId = messageData.MessageId,
                ReceivedMessage = messageData.ReceivedMessage,
                SenderPhone = messageData.SenderPhone,
                ResponseMessage = messageData.ResponseMessage,
                CreatedAt = messageData.CreatedAt,
                UpdatedAt = messageData.UpdatedAt,
            };
        }

        public static MessageData toData(Message message)
        {
            return new MessageData
            {
                Id = message.Id,
                MessageId = message.MessageId,
                ReceivedMessage = message.ReceivedMessage,
                SenderPhone = message.SenderPhone,
                ResponseMessage = message.ResponseMessage,
                CreatedAt = message.CreatedAt,
                UpdatedAt = message.UpdatedAt,
            };
        }

        public static IEnumerable<Message> toDomainList(IEnumerable<MessageData> messageDataList)
        {
            if (!messageDataList.Any())
                return Enumerable.Empty<Message>();

            List<Message> messages = new List<Message>();

            foreach (var message in messageDataList)
            {
                messages.Add(toDomain(message));
            }

            return messages;
        }
    }
}
