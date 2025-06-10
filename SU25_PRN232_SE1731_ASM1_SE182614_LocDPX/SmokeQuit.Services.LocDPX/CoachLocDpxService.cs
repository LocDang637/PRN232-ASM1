using SmokeQuit.Repositories.LocDPX;
using SmokeQuit.Repositories.LocDPX.Models;
using SmokeQuit.Repositories.LocDPX.ModelExtensions;

namespace SmokeQuit.Services.LocDPX
{
    public class CoachLocDpxService : ICoachLocDpxService
    {
        private readonly CoachesLocDpxRepository _repository;

        public CoachLocDpxService()
        {
            _repository ??= new CoachesLocDpxRepository();
        }

        public CoachLocDpxService(CoachesLocDpxRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CoachesLocDpx>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CoachesLocDpx> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<CoachesLocDpx> GetByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }

        public async Task<List<CoachesLocDpx>> SearchAsync(string fullName, string email)
        {
            return await _repository.SearchAsync(fullName, email);
        }

        public async Task<PaginationResult<List<CoachesLocDpx>>> SearchWithPagingAsync(string fullName, string email, int currentPage, int pageSize)
        {
            return await _repository.SearchWithPagingAsync(fullName, email, currentPage, pageSize);
        }

        public async Task<PaginationResult<List<CoachesLocDpx>>> GetAllWithPagingAsync(int currentPage, int pageSize)
        {
            return await _repository.GetAllWithPagingAsync(currentPage, pageSize);
        }

        public async Task<int> CreateAsync(CoachesLocDpx coach)
        {
            coach.CreatedAt = DateTime.Now;
            return await _repository.CreateAsync(coach);
        }

        public async Task<int> UpdateAsync(CoachesLocDpx coach)
        {
            return await _repository.UpdateAsync(coach);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var coach = await _repository.GetByIdAsync(id);
            if (coach == null) return false;
            return await _repository.RemoveAsync(coach);
        }
    }
}