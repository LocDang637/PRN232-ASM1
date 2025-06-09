using Microsoft.AspNetCore.Mvc;
using SmokeQuit.Repositories.LocDPX.Models;
using SmokeQuit.Services.LocDPX;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmokeQuit.APIServices.BE.LocDPX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachLocDpxController(CoachLocDpxService _service) : ControllerBase
    {
        // GET: api/<CoachLocDpxController>
        [HttpGet]
        public async Task<IEnumerable<CoachesLocDpx>> Get()
        {
            return await _service.GetAll();
        }

        // GET api/<CoachLocDpxController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var coach = await _service.GetById(id);
            if (coach == null)
            {
                return NotFound($"Coach with ID {id} not found.");
            }
            return Ok(coach);
        }

        // POST api/<CoachLocDpxController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string coachEmail)
        {
            if (string.IsNullOrEmpty(coachEmail))
            {
                return BadRequest("Missing coach email.");
            }
            var existingCoach = await _service.GetByEmail(coachEmail.ToLower());
            if (existingCoach != null)
            {
                return Conflict($"Coach with email {coachEmail} already exists.");
            }
            var newCoachId = await _service.Add(coachEmail);
            return CreatedAtAction(nameof(Get), new { id = newCoachId }, new { id = newCoachId, email = coachEmail });

        }



      
    }
}
