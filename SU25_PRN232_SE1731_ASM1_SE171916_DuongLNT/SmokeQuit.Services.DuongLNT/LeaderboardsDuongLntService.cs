using SmokeQuit.Repositories.DuongLNT;
using SmokeQuit.Repositories.DuongLNT.ModelExtensions;
using SmokeQuit.Repositories.DuongLNT.Models;

namespace SmokeQuit.Services.DuongLNT
{
	//public interface ILeaderboardsDuongLntService
	//{

	//}

	public class LeaderboardsDuongLntService : ILeaderboardsDuongLntService
	{
		private readonly LeaderboardsDuongLNTRepository _repository;
		
		//Nếu null, không tham số thì new nó
		public LeaderboardsDuongLntService() => _repository ??= new LeaderboardsDuongLNTRepository();

		public LeaderboardsDuongLntService(LeaderboardsDuongLNTRepository repository) => _repository = repository;

		public async Task<int> CreateAsync(LeaderboardsDuongLnt leaderboards)
		{
			return await _repository.CreateAsync(leaderboards);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var leaderboards = await _repository.GetByIdAsync(id);
			return await _repository.RemoveAsync(leaderboards);
		}

		public async Task<List<LeaderboardsDuongLnt>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<LeaderboardsDuongLnt> GetByIdAsync(int id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public async Task<List<LeaderboardsDuongLnt>> SearchAsync(string note, double money, string reason)
		{
			return await _repository.SearchAsync(note, money, reason);
		}

		public async Task<int> UpdateAsync(LeaderboardsDuongLnt leaderboards)
		{
			return await _repository.UpdateAsync(leaderboards);
		}

		public async Task<PaginationResult<List<LeaderboardsDuongLnt>>> SearchWithPagingAsync(string note, double money, string reason, int currentPage, int pageSize)
		{
			var paginationResult = await _repository.SearchWithPagingAsync(note, money, reason, currentPage, pageSize);

			return paginationResult ?? new PaginationResult<List<LeaderboardsDuongLnt>>();
		}

		public async Task<PaginationResult<List<LeaderboardsDuongLnt>>> GetAllWithPagingAsync(int currentPage, int pageSize)
		{
			var paginationResult = await _repository.GetAllWithPagingAsync(currentPage, pageSize);

			return paginationResult ?? new PaginationResult<List<LeaderboardsDuongLnt>>();
		}

		//public async Task<PaginationResult<List<LeaderboardsDuongLnt>>> SearchNew(SearchLeaderboardsRequest search)
		//{
		//	return await _repository.SearchWithPagingAsync(search.Reason, search.Money, search.Reason, search.CurrentPage, search.PageSize);
		//}
	}
}
