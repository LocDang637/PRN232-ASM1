using SmokeQuit.Repositories.DuongLNT;
using SmokeQuit.Repositories.DuongLNT.Models;

namespace SmokeQuit.Services.DuongLNT
{
	public class QuitPlansAnhDtnDuongLNTService
	{
		private readonly QuitPlansAnhDtnDuongLNTRepository _repository;

		public QuitPlansAnhDtnDuongLNTService() => _repository ??= new QuitPlansAnhDtnDuongLNTRepository();

		public async Task<List<QuitPlansAnhDtn>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

	}
}
