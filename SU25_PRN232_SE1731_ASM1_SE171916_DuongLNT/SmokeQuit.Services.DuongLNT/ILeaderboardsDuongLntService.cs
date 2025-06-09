using SmokeQuit.Repositories.DuongLNT.ModelExtensions;
using SmokeQuit.Repositories.DuongLNT.Models;

namespace SmokeQuit.Services.DuongLNT
{
	public interface ILeaderboardsDuongLntService
	{
		Task<List<LeaderboardsDuongLnt>> GetAllAsync();

		Task<LeaderboardsDuongLnt> GetByIdAsync(int id);

		Task<List<LeaderboardsDuongLnt>> SearchAsync(string? note, double money, string? reason);

		Task<PaginationResult<List<LeaderboardsDuongLnt>>> SearchWithPagingAsync(string note, double money, string reason, int currentPage, int pageSize);

		Task<PaginationResult<List<LeaderboardsDuongLnt>>> GetAllWithPagingAsync(int currentPage, int pageSize);

		//Task<PaginationResult<List<LeaderboardsDuongLnt>>> SearchNew(SearchLeaderboardsRequest search);

		Task<int> CreateAsync(LeaderboardsDuongLnt leaderboards);
		Task<int> UpdateAsync(LeaderboardsDuongLnt leaderboards);
		Task<bool> DeleteAsync(int id);

	}
}
