using Microsoft.AspNetCore.Mvc;
using SmokeQuit.Repositories.LocDPX.Models;
using SmokeQuit.Services.LocDPX;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmokeQuit.APIServices.BE.LocDPX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController(SystemUserAccountService _service) : ControllerBase
    {
        [HttpGet("{Username},{Password}")]
        public async Task<IActionResult> Login(string Username, string Password)
        {
            var result = await _service.GetAccount(Username, Password);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        // GET: api/<UserAccountController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserAccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserAccountController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserAccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserAccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
