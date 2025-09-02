using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Mapper;

namespace Persistence
{
    public class MessageRepository : IMessageRepository
    {

        private readonly MessageDbContext _context;

        public MessageRepository(MessageDbContext context)
        {
            _context = context;
        }

        public async Task<bool> GetStatus()
        {
            return await _context.Database.CanConnectAsync();
        }

        public async Task<Message> GetById(Guid id)
        {
            var message = await _context.Messages
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            return DataMapper.toDomain(message);
        }

        public async Task<IEnumerable<Message>> GetAllByMessageIdAsync(String id)
        {
            var messageList = await _context.Messages.Where(message => message.MessageId == id).ToListAsync();

            return DataMapper.toDomainList(messageList);
        }

        public async Task<Message> Add(Message message)
        {
            var messageData = DataMapper.toData(message);
            messageData.CreatedAt = DateTime.UtcNow;

            var savedMessage = _context.Messages.Add(messageData);
            await _context.SaveChangesAsync();

            messageData.Id = savedMessage.Entity.Id;

            return DataMapper.toDomain(messageData);
        }

        public async Task<Message> Update(Message messageToUpdate, Message message)
        {
            var messageData = DataMapper.toData(message);

            if (messageToUpdate.MessageId != null)
            {
                messageData.MessageId = messageToUpdate.MessageId;
            }
            if (messageToUpdate.ReceivedMessage != null)
            {
                messageData.ReceivedMessage = messageToUpdate.ReceivedMessage;
            }
            if (messageToUpdate.SenderPhone != null)
            {
                messageData.SenderPhone = messageToUpdate.SenderPhone;
            }
            if (messageToUpdate.ResponseMessage != null)
            {
                messageData.ResponseMessage = messageToUpdate.ResponseMessage;
            }
            messageData.UpdatedAt = DateTime.UtcNow;

            _context.Messages.Update(messageData);

            await _context.SaveChangesAsync();

            return DataMapper.toDomain(messageData);
        }

        public async Task Delete(Message message)
        {
            var messageData = DataMapper.toData(message);

            _context.Messages.Remove(messageData);
            await _context.SaveChangesAsync();
        }
    }
}
