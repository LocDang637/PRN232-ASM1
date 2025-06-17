using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmokeQuit.Repositories.LocDPX.Models;
using SmokeQuit.Repositories.LocDPX.ModelExtensions;
using SmokeQuit.Services.LocDPX;

namespace SmokeQuit.APIServices.BE.LocDPX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [AllowAnonymous]
    public class CoachLocDpxController : ControllerBase
    {
        private readonly ICoachLocDpxService _service;

        public CoachLocDpxController(ICoachLocDpxService service)
        {
            _service = service;
        }

        // GET: api/CoachLocDpx
        [HttpGet]
       
        public async Task<IActionResult> Get()
        {
            var coaches = await _service.GetAllAsync();
            return Ok(coaches);
        }

        // GET: api/CoachLocDpx/5
        [HttpGet("{id}")]
       
        public async Task<IActionResult> Get(int id)
        {
            var coach = await _service.GetByIdAsync(id);
            if (coach == null)
                return NotFound($"Coach with ID {id} not found");
            return Ok(coach);
        }

        // POST: api/CoachLocDpx
        [HttpPost]
    
        public async Task<IActionResult> Create([FromBody] CoachesLocDpx coach)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCoach = await _service.GetByEmailAsync(coach.Email);
            if (existingCoach != null)
                return Conflict($"Coach with email {coach.Email} already exists");

            var result = await _service.CreateAsync(coach);
            return CreatedAtAction(nameof(Get), new { id = result }, coach);
        }

        // PUT: api/CoachLocDpx/5
        [HttpPut("{id}")]
    
        public async Task<IActionResult> Update(int id, [FromBody] CoachesLocDpx coach)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != coach.CoachesLocDpxid)
                return BadRequest("ID mismatch");

            var existingCoach = await _service.GetByIdAsync(id);
            if (existingCoach == null)
                return NotFound($"Coach with ID {id} not found");

            await _service.UpdateAsync(coach);
            return NoContent();
        }

        // DELETE: api/CoachLocDpx/5
        [HttpDelete("{id}")]
    
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound($"Coach with ID {id} not found");
            return NoContent();
        }

        // POST: api/CoachLocDpx/search
        [HttpPost("Search")]
    
        public async Task<IActionResult> Search([FromBody] SearchCoachRequest request)
        {
            var result = await _service.SearchWithPagingAsync(
                request.FullName ?? "",
                request.Email ?? "",
                request.CurrentPage,
                request.PageSize);

            return Ok(result);
        }

        // GET: api/CoachLocDpx/paging/{currentPage}/{pageSize}
        [HttpGet("{currentPage}/{pageSize}")]
     
        public async Task<IActionResult> GetWithPaging(int currentPage, int pageSize)
        {
            var result = await _service.GetAllWithPagingAsync(currentPage, pageSize);
            return Ok(result);
        }

        // GET: api/CoachLocDpx/email/{email}
        [HttpGet("email/{email}")]
    
        public async Task<IActionResult> GetByEmail(string email)
        {
            var coach = await _service.GetByEmailAsync(email);
            if (coach == null)
                return NotFound($"Coach with email {email} not found");
            return Ok(coach);
        }
    }
}