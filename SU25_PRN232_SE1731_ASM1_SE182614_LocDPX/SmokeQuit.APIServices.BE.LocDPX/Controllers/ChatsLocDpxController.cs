using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using SmokeQuit.APIServices.BE.LocDPX.Dto;
using SmokeQuit.Repositories.LocDPX.Models;
using SmokeQuit.Services.LocDPX;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmokeQuit.APIServices.BE.LocDPX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsLocDpxController(IChatsLocDpxService _service) : ControllerBase
    {
        //private readonly IChatsLocDpxService _chatsLocDpxService;
        //public ChatsLocDpxController(IChatsLocDpxService service)
        //{
        //    _chatsLocDpxService = service;
        //}
        // GET: api/<ChatsLocDpxController>
        [HttpGet]
        public async Task<IEnumerable<ChatsLocDpx>> Get()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<ChatsLocDpxController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var chat = await _service.GetByIdAsync(id);
            return Ok(chat);
        }

        // POST api/<ChatsLocDpxController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChatDto value)
        {
            if (value == null)
                return BadRequest("Missing value");
            var createChat = new ChatsLocDpx
            {
                Message = value.Message,
                MessageType = value.MessageType,
                UserId = value.UserId,
                CoachId = value.CoachId,
                SentBy = value.SentBy,
                AttachmentUrl = value.AttachmentUrl,
                CreatedAt = DateTime.UtcNow
            };
            var result = await _service.CreateAsync(createChat);
            return CreatedAtAction(nameof(GetById), new { id = result }, createChat);
        }

        // PUT api/<ChatsLocDpxController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult>  Update(int id, [FromBody] string message)
        {
            if (string.IsNullOrEmpty(message))
                return BadRequest("Missing message");
            var chat = await _service.GetByIdAsync(id);
            if (chat == null)
                return NotFound();
            chat.Message = message;
            await _service.UpdateAsync(chat);
            return NoContent();
        }

        // DELETE api/<ChatsLocDpxController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var chat = await _service.GetByIdAsync(id);
            if (chat == null)
                return NotFound();
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
