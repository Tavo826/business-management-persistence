using Application.Interfaces;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BusinessPersistence.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public IActionResult GetApiStatus()
        {
            return Ok(new ResponseDto<string>
            {
                Success = true,
                Message = "Status OK!",
                StatusCode = (int)HttpStatusCode.OK,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetDbStatus()
        {

            var response = await _messageService.GetDbStatus();
            return Ok(new ResponseDto<string>
            {
                Success = response,
                Message = response ? "Status OK!" : "Status Bad",
                StatusCode = response ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError,
            });
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageByIdAsync(String id)
        {
            
            var messageList = await _messageService.GetAllByMessageIdAsync(id);
            if (messageList == null || !messageList.Any())
            {
                return NotFound(new ErrorResponseDto
                {
                    Title = HttpStatusCode.NotFound.ToString(),
                    Success = false,
                    Message = "Messages not found",
                    StatusCode = (int)HttpStatusCode.NotFound
                });
            }

            return Ok(new ResponseDto<IEnumerable<MessageResponseDto>>
            {
                Success = true,
                Message = $"Messages associated to {id} successfully retrieved",
                StatusCode = (int)HttpStatusCode.OK,
                Data = messageList
            });            
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessageAsync(MessageRequestDto messageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = await _messageService.CreateMessageAsync(messageDto);

            return Created(message.Id.ToString(), new ResponseDto<MessageResponseDto>
            {
                Success = true,
                Message = "Message successfully created",
                StatusCode = (int)HttpStatusCode.OK,
                Data = message
            });

        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateMessageAsync(Guid id, MessageRequestDto messageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedMessage = await _messageService.UpdateMessageAsync(id, messageDto);

            return Ok(new ResponseDto<MessageResponseDto>
            {
                Success = true,
                Message = $"Message with id {id} successfully updated",
                StatusCode = (int)HttpStatusCode.OK,
                Data = updatedMessage
            });
            
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMessageAsync(Guid id)
        {

            await _messageService.DeleteMessageAsync(id);

            return Ok(new ResponseDto<Guid>
            {
                Success = true,
                Message = $"Message with id {id} successfully deleted",
                StatusCode = (int)HttpStatusCode.OK,
                Data = id
            });

        }
    }
}
