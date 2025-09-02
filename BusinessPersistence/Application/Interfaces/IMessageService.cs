using Domain.Dtos.Request;
using Domain.Dtos.Response;

namespace Application.Interfaces
{
    public interface IMessageService
    {
        Task<bool> GetDbStatus();
        Task<IEnumerable<MessageResponseDto>> GetAllByMessageIdAsync(String id);
        Task<MessageResponseDto> CreateMessageAsync(MessageRequestDto messageDto);
        Task<MessageResponseDto> UpdateMessageAsync(Guid id, MessageRequestDto updatedMessage);
        Task DeleteMessageAsync(Guid id);
    }
}
