using Application.Interfaces;
using AutoMapper;
using Domain.Dtos.Request;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Persistence;
using System.Data;

namespace Application.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly MessageDbContext _context;
        private readonly ILogger<MessageService> _logger;
        private readonly IMapper _mapper;

        public MessageService(MessageDbContext context, ILogger<MessageService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<Message> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateMessageAsync(MessageDto messageDto)
        {
            try
            {
                var message = _mapper.Map<Message>(messageDto);
                message.CreatedAt = DateTime.UtcNow;
                _context.Messages.Add(message);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while creating the message");
                throw new Exception("An error ocurred while creating the message");
            }
        }
        public Task UpdateMessage(Guid id, MessageDto updatedMessage)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMessage(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
