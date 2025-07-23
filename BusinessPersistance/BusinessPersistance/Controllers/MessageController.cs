using Application.Interfaces;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BusinessPersistance.Controllers
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

        /*[HttpGet]
        public IActionResult GetStatus()
        {

        }*/

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageByIdAsync(String id)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDto
                {
                    Title = HttpStatusCode.InternalServerError.ToString(),
                    Success = false,
                    Message = "An error ocurred while retrieving the message " + ex.Message,
                    StatusCode= (int)HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessageAsync(MessageRequestDto messageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var message = await _messageService.CreateMessageAsync(messageDto);
                return Created(message.Id.ToString(), new ResponseDto<MessageResponseDto>
                {
                    Success = true,
                    Message = "Message successfully created",
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDto
                {
                    Title = HttpStatusCode.InternalServerError.ToString(),
                    Success = false,
                    Message = "An error ocurred while creating the message " + ex.Message,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                });
            }            
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateMessageAsync(Guid id, MessageRequestDto messageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var message = await _messageService.GetByIdAsync(id);
                if (message == null)
                {
                    return NotFound(new ErrorResponseDto
                    {
                        Title = HttpStatusCode.NotFound.ToString(),
                        Success = false,
                        Message = $"Message with id {id} not found",
                        StatusCode = (int)HttpStatusCode.NotFound
                    });
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
            catch (Exception ex)
            {

                return StatusCode(500, new ErrorResponseDto
                {
                    Title = HttpStatusCode.InternalServerError.ToString(),
                    Success = false,
                    Message = $"An error ocurred while updating the message with id {id}" + ex.Message,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMessageAsync(Guid id)
        {
            try
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
            catch (Exception ex)
            {

                return StatusCode(500, new ErrorResponseDto
                {
                    Title = HttpStatusCode.InternalServerError.ToString(),
                    Success = false,
                    Message = $"An error ocurred while deleting the message with id {id}" + ex.Message,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                });
            }
        }

    }
}
