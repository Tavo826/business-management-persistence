using Domain.Dtos.Request;
using Domain.Models;
using System.Data;

namespace Application.Interfaces
{
    public interface IMessageService
    {
        Task<Message> GetByIdAsync(Guid id);
        Task CreateMessageAsync(MessageDto messageDto);
        Task UpdateMessage(Guid id, MessageDto updatedMessage);
        Task DeleteMessage(Guid id);

    }
}
