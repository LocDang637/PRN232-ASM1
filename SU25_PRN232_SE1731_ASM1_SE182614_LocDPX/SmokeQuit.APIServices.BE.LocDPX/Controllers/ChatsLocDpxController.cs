using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmokeQuit.APIServices.BE.LocDPX.Dto;
using SmokeQuit.Repositories.LocDPX.Models;
using SmokeQuit.Repositories.LocDPX.ModelExtensions;
using SmokeQuit.Services.LocDPX;

namespace SmokeQuit.APIServices.BE.LocDPX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatsLocDpxController : ControllerBase
    {
        private readonly IChatsLocDpxService _service;

        public ChatsLocDpxController(IChatsLocDpxService service)
        {
            _service = service;
        }

        // GET: api/ChatsLocDpx
        [HttpGet]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> Get()
        {
            var chats = await _service.GetAllAsync();
            return Ok(chats);
        }

        // GET: api/ChatsLocDpx/5
        [HttpGet("{id}")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> GetById(int id)
        {
            var chat = await _service.GetByIdAsync(id);
            if (chat == null)
                return NotFound($"Chat with ID {id} not found");
            return Ok(chat);
        }

        // POST: api/ChatsLocDpx
        [HttpPost]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> Create([FromBody] ChatDto chatDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var chat = new ChatsLocDpx
            {
                Message = chatDto.Message,
                MessageType = chatDto.MessageType,
                UserId = chatDto.UserId,
                CoachId = chatDto.CoachId,
                SentBy = chatDto.SentBy,
                AttachmentUrl = chatDto.AttachmentUrl,
                IsRead = chatDto.IsRead,
                ResponseTime = chatDto.ResponseTime,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _service.CreateAsync(chat);
            return CreatedAtAction(nameof(GetById), new { id = result }, chat);
        }

        // PUT: api/ChatsLocDpx/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> Update(int id, [FromBody] ChatDto chatDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingChat = await _service.GetByIdAsync(id);
            if (existingChat == null)
                return NotFound($"Chat with ID {id} not found");

            existingChat.Message = chatDto.Message;
            existingChat.MessageType = chatDto.MessageType;
            existingChat.SentBy = chatDto.SentBy;
            existingChat.AttachmentUrl = chatDto.AttachmentUrl;
            existingChat.IsRead = chatDto.IsRead;
            existingChat.ResponseTime = chatDto.ResponseTime;

            await _service.UpdateAsync(existingChat);
            return NoContent();
        }

        // DELETE: api/ChatsLocDpx/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound($"Chat with ID {id} not found");
            return NoContent();
        }

        // POST: api/ChatsLocDpx/search
        [HttpPost("Search")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> Search([FromBody] SearchChatRequest request)
        {
            var result = await _service.SearchWithPagingAsync(
                request.Message ?? "",
                request.MessageType ?? "",
                request.SentBy ?? "",
                request.StartDate,
                request.EndDate,
                request.CurrentPage,
                request.PageSize);

            return Ok(result);
        }

        // GET: api/ChatsLocDpx/paging/{currentPage}/{pageSize}
        [HttpGet("{currentPage}/{pageSize}")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> GetWithPaging(int currentPage, int pageSize)
        {
            var result = await _service.GetAllWithPagingAsync(currentPage, pageSize);
            return Ok(result);
        }

        // GET: api/ChatsLocDpx/user/{userId}
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> GetChatsByUser(int userId)
        {
            var chats = await _service.GetChatsByUserAsync(userId);
            return Ok(chats);
        }

        // GET: api/ChatsLocDpx/coach/{coachId}
        [HttpGet("coach/{coachId}")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> GetChatsByCoach(int coachId)
        {
            var chats = await _service.GetChatsByCoachAsync(coachId);
            return Ok(chats);
        }
    }
}