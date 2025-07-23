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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageByIdAsync(Guid id)
        {
            try
            {
                var message = await _messageService.GetByIdAsync(id);
                if (message == null)
                {
                    return Ok(new
                    {
                        message = "Message not found"
                    });
                }

                return Ok(new
                {
                    message = "Successfully retrieve message",
                    data = message
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

    }
}
