using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Domain.Models;

namespace Application.Mapper
{
    public static class DtoMapper
    {
        public static Message toDomain(MessageRequestDto request)
        {
            return new Message
            {
                MessageId = request.MessageId,
                ReceivedMessage = request.ReceivedMessage,
                SenderPhone = request.SenderPhone,
                ResponseMessage = request.ResponseMessage,
            };
        }

        public static MessageResponseDto toDto(Message message)
        {
            return new MessageResponseDto
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

        public static IEnumerable<MessageResponseDto> toDtoList(IEnumerable<Message> messages)
        {
            List<MessageResponseDto> messageResponse = new List<MessageResponseDto>();
            foreach (var message in messages)
            {
                messageResponse.Add(toDto(message));
            }

            return messageResponse;
        }
    }
}
