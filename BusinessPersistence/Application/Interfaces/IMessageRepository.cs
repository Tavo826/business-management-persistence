using Domain.Models;

namespace Application.Interfaces
{
    public interface IMessageRepository
    {
        Task<bool> GetStatus();
        Task<Message> GetById(Guid id);
        Task<IEnumerable<Message>> GetAllByMessageIdAsync(String id);
        Task<Message> Add(Message message);
        Task<Message> Update(Message messageToUpdate, Message message);
        Task Delete(Message message);
    }
}
