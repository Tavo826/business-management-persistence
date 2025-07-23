using Application.Interfaces;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id:string}")]
        public async Task<IActionResult> GetMessageByIdAsync(String id)
        {
            try
            {
                var messageList = await _messageService.GetAllByMessageIdAsync(id);
                if (messageList == null || !messageList.Any())
                {
                    return Ok(new
                    {
                        message = "Messages not found"
                    });
                }

                return Ok(new
                {
                    message = "Successfully retrieve message",
                    data = messageList
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error ocurred while retrieving the message",
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessageAsync(MessageDto messageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _messageService.CreateMessageAsync(messageDto);
                return Ok(new
                {
                    message = "Message succesfully saved"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error ocurred while saving the message",
                    error = ex.Message
                });
            }            
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateMessageAsync(Guid id, MessageDto messageDto)
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
                    return NotFound(new
                    {
                        message = $"Message with id {id} not found"
                    });
                }

                await _messageService.UpdateMessageAsync(id, messageDto);

                return Ok(new
                {
                    message = $"Message with id {id} successfully updated"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"An error ocurred while updating the message with id {id}",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMessageAsync(Guid id)
        {
            try
            {
                await _messageService.DeleteMessageAsync(id);

                return Ok(new
                {
                    message = $"Message with id {id} successfully deleted"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"An error ocurred while deleting the message with id {id}",
                    error = ex.Message
                });
            }
        }

    }
}
