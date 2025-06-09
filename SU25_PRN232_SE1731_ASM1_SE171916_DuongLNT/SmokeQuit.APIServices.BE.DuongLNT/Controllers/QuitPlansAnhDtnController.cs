using Microsoft.AspNetCore.Mvc;
using SmokeQuit.Repositories.DuongLNT.Models;
using SmokeQuit.Services.DuongLNT;

namespace SmokeQuit.APIServices.BE.DuongLNT.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuitPlansAnhDtnController : ControllerBase
	{
		private readonly QuitPlansAnhDtnDuongLNTService _quitPlansAnhDtnService;
		public QuitPlansAnhDtnController(QuitPlansAnhDtnDuongLNTService quitPlansAnhDtnService)
		{
			_quitPlansAnhDtnService = quitPlansAnhDtnService;
		}

		// GET: api/<QuitPlansAnhDtnController>
		[HttpGet]
		public async Task<IEnumerable<QuitPlansAnhDtn>> Get()
		{
			return await _quitPlansAnhDtnService.GetAllAsync();
		}

		//// GET api/<QuitPlansAnhDtnController>/5
		//[HttpGet("{id}")]
		//public string Get(int id)
		//{
		//    return "value";
		//}

		//// POST api/<QuitPlansAnhDtnController>
		//[HttpPost]
		//public void Post([FromBody] string value)
		//{
		//}

		//// PUT api/<QuitPlansAnhDtnController>/5
		//[HttpPut("{id}")]
		//public void Put(int id, [FromBody] string value)
		//{
		//}

		//// DELETE api/<QuitPlansAnhDtnController>/5
		//[HttpDelete("{id}")]
		//public void Delete(int id)
		//{
		//}
	}
}
