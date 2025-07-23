using Domain.Dtos.Request;
using Domain.Models;
using System.Data;

namespace Application.Interfaces
{
    public interface IMessageService
    {
        Task<Message> GetByIdAsync(Guid id);
        Task<IEnumerable<Message>> GetAllByMessageIdAsync(String id);
        Task CreateMessageAsync(MessageDto messageDto);
        Task UpdateMessageAsync(Guid id, MessageDto updatedMessage);
        Task DeleteMessageAsync(Guid id);

    }
}
