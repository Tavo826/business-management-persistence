using Application.Interfaces;
using AutoMapper;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

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

        public async Task<bool> GetDbStatus()
        {
            return await _context.Database.CanConnectAsync();
        }

        public async Task<IEnumerable<MessageResponseDto>> GetAllByMessageIdAsync(String id)
        {
            var messageList = await _context.Messages.Where(message => message.MessageId == id).ToListAsync();

            if (messageList == null)
            {
                throw new Exception("No messages found");
            }

            return _mapper.Map<IEnumerable<MessageResponseDto>>(messageList);
        }

        public async Task<MessageResponseDto> CreateMessageAsync(MessageRequestDto messageDto)
        {
            try
            {
                var message = _mapper.Map<Message>(messageDto);
                message.CreatedAt = DateTime.UtcNow;
                var savedMessage = _context.Messages.Add(message);

                await _context.SaveChangesAsync();

                message.Id = savedMessage.Entity.Id;

                return _mapper.Map<MessageResponseDto>(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while creating the message");
                throw new Exception("An error ocurred while creating the message");
            }
        }
        public async Task<MessageResponseDto> UpdateMessageAsync(Guid id, MessageRequestDto updatedMessage)
        {
            try
            {
                var message = await _context.Messages.FindAsync(id);
                if (message == null)
                {
                    throw new Exception($"No message found with id {id}");
                }

                if (updatedMessage.MessageId != null)
                {
                    message.MessageId = updatedMessage.MessageId;
                }
                if (updatedMessage.ReceivedMessage != null)
                {
                    message.ReceivedMessage = updatedMessage.ReceivedMessage;
                }
                if (updatedMessage.SenderPhone != null)
                {
                    message.SenderPhone = updatedMessage.SenderPhone;
                }
                if (updatedMessage.ResponseMessage != null)
                {
                    message.ResponseMessage = updatedMessage.ResponseMessage;
                }

                message.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return _mapper.Map<MessageResponseDto>(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error ocurred while updating the message with id {id}");
                throw;
            }
        }

        public async Task DeleteMessageAsync(Guid id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message != null)
            {
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($" No message found with id {id}");
            }
        }

        public async Task<MessageResponseDto> GetByIdAsync(Guid id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                throw new KeyNotFoundException($" No message with id {id} found");
            }

            return _mapper.Map<MessageResponseDto>(message);
        }
    }
}
