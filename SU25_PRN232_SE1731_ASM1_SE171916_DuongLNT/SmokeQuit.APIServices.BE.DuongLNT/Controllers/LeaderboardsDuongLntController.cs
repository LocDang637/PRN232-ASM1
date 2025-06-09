using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmokeQuit.Repositories.DuongLNT.ModelExtensions;
using SmokeQuit.Repositories.DuongLNT.Models;
using SmokeQuit.Services.DuongLNT;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmokeQuit.APIServices.BE.DuongLNT.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class LeaderboardsDuongLntController : ControllerBase
	{
		private readonly ILeaderboardsDuongLntService _leaderboardsDuongLntService;

		//Dùng cái này phải cấp phát vùng nhớ cho nó => đk trên program
		public LeaderboardsDuongLntController(ILeaderboardsDuongLntService leaderboardsDuongLntService) => _leaderboardsDuongLntService = leaderboardsDuongLntService;

		//public LeaderboardsDuongLntController(ILeaderboardsDuongLntService leaderboardsDuongLntService)
		//{
		//	_leaderboardsDuongLntService = leaderboardsDuongLntService;
		//}

		// GET: api/<LeaderboardsDuongLntController>
		[Authorize(Roles ="1,2")]
		[HttpGet]
		public async Task<IEnumerable<LeaderboardsDuongLnt>> Get()
		{
			return await _leaderboardsDuongLntService.GetAllAsync();
		}

		// GET api/<LeaderboardsDuongLntController>/5
		[Authorize(Roles = "1,2")]
		[HttpGet("{id}")]
		public async Task<LeaderboardsDuongLnt> Get(int id)
		{
			return await _leaderboardsDuongLntService.GetByIdAsync(id);
		}

		// POST api/<LeaderboardsDuongLntController>
		[Authorize(Roles = "1,2")]
		[HttpPost]
		public async Task<int> Post(LeaderboardsDuongLnt leaderboards)
		{
			if (ModelState.IsValid)
			{
				return await _leaderboardsDuongLntService.CreateAsync(leaderboards);
			}

			return 0;
		}

		// PUT api/<LeaderboardsDuongLntController>/5
		//[HttpPut("{id}")]
		[Authorize(Roles = "1,2")]
		[HttpPut]
		public async Task<int> Put(LeaderboardsDuongLnt leaderboards)
		{
			if (ModelState.IsValid)
			{
				return await _leaderboardsDuongLntService.UpdateAsync(leaderboards);
			}

			return 0;

		}

		// DELETE api/<LeaderboardsDuongLntController>/5
		[Authorize(Roles = "1")]
		[HttpDelete("{id}")]
		public async Task<bool> Delete(int id)
		{
			return await _leaderboardsDuongLntService.DeleteAsync(id);
		}

		#region Comment tạm
		//[Authorize(Roles ="1,2")]
		//[HttpGet("{page}/{pageSize}")]
		//public async Task<IActionResult> Get(int page, int pageSize)
		//{
		//	var leaderboardsQueryable =  (await _leaderboardsDuongLntService.GetAllAsync()).AsQueryable();

		//	var totalCount = leaderboardsQueryable.Count();
		//	var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

		//	leaderboardsQueryable = leaderboardsQueryable.Skip((page - 1) * pageSize).Take(pageSize);

		//	var result = new
		//	{
		//		TotalItems = totalCount,
		//		TotalPages = totalPages,
		//		CurrentPage = page,
		//		PageSize = pageSize,
		//		LeaderBoards = leaderboardsQueryable.ToList()
		//	};

		//	return Ok(result);
		//}
		#endregion

		[HttpGet("{note}/{money}/{reason}/{currentPage}/{pageSize}")]
		[Authorize(Roles = "1,2")]
		public async Task<PaginationResult<List<LeaderboardsDuongLnt>>> Get(string note, double money, string reason, int currentPage, int pageSize)
		{
			return await _leaderboardsDuongLntService.SearchWithPagingAsync(note, money, reason, currentPage, pageSize);
		}

		[HttpGet("{note}/{money}/{reason}")]
		//[HttpGet("Search")]
		[Authorize(Roles = "1,2")]
		public async Task<List<LeaderboardsDuongLnt>> Get(string? note, double money, string? reason)
		{
			return await _leaderboardsDuongLntService.SearchAsync(note, money, reason);
		}

		[HttpGet("{currentPage}/{pageSize}")]
		[Authorize(Roles = "1,2")]
		public async Task<PaginationResult<List<LeaderboardsDuongLnt>>> Get(int currentPage, int pageSize)
		{
			return await _leaderboardsDuongLntService.GetAllWithPagingAsync(currentPage, pageSize);
		}

		[HttpPost("Search")]
		//[Authorize(Roles = "1,2")]
		public async Task<PaginationResult<List<LeaderboardsDuongLnt>>> Get(SearchLeaderboardsRequest request)
		{
			return await _leaderboardsDuongLntService.SearchWithPagingAsync(request.Note ?? "", request.Money ?? 0, request.Reason ?? "", request.CurrentPage, request.PageSize);
		}

	}
}
