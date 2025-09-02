using Application.Interfaces;
using Application.Mapper;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ILogger<MessageService> _logger;

        public MessageService(IMessageRepository messageRepository, ILogger<MessageService> logger)
        {
            _messageRepository = messageRepository;
            _logger = logger;
        }

        public async Task<bool> GetDbStatus()
        {
            return await _messageRepository.GetStatus();
        }

        public async Task<IEnumerable<MessageResponseDto>> GetAllByMessageIdAsync(String id)
        {
            var messageList = await _messageRepository.GetAllByMessageIdAsync(id);

            if (!messageList.Any())
            {
                throw new MessageNotFoundException($"No messages found with id {id}");
            }

            return DtoMapper.toDtoList(messageList);
        }

        public async Task<MessageResponseDto> CreateMessageAsync(MessageRequestDto messageDto)
        {
            try
            {
                var message = DtoMapper.toDomain(messageDto);
                message.CreatedAt = DateTime.UtcNow;

                var savedMessage = await _messageRepository.Add(message);

                return DtoMapper.toDto(savedMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred while creating the message");
                throw new Exception("An error ocurred while creating the message");
            }
        }
        public async Task<MessageResponseDto> UpdateMessageAsync(Guid id, MessageRequestDto messageToUpdateDto)
        {
            try
            {
                var message = await _messageRepository.GetById(id);

                if (message == null)
                {
                    throw new MessageNotFoundException($"No message found with id {id}");
                }

                var messageToUpdate = DtoMapper.toDomain(messageToUpdateDto);

                var updatedMessage = await _messageRepository.Update(messageToUpdate, message);

                return DtoMapper.toDto(updatedMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error ocurred while updating the message with id {id}");
                throw;
            }
        }

        public async Task DeleteMessageAsync(Guid id)
        {
            var message = await _messageRepository.GetById(id);

            if (message == null)
            {
                throw new MessageNotFoundException($"No message found with id {id}");
            }

            await _messageRepository.Delete(message);
        }
    }
}
