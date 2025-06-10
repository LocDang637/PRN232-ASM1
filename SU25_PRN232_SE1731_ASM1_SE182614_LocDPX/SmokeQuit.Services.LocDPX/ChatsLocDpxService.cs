using SmokeQuit.Repositories.LocDPX;
using SmokeQuit.Repositories.LocDPX.Models;
using SmokeQuit.Repositories.LocDPX.ModelExtensions;

namespace SmokeQuit.Services.LocDPX
{
    public class ChatsLocDpxService : IChatsLocDpxService
    {
        private readonly ChatsLocDpxRepository _repository;

        public ChatsLocDpxService()
        {
            _repository ??= new ChatsLocDpxRepository();
        }

        public ChatsLocDpxService(ChatsLocDpxRepository repo)
        {
            _repository = repo;
        }

        public async Task<List<ChatsLocDpx>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ChatsLocDpx> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<ChatsLocDpx>> SearchAsync(string message, string messageType, string sentBy, DateTime? startDate, DateTime? endDate)
        {
            return await _repository.SearchAsync(message, messageType, sentBy, startDate, endDate);
        }

        public async Task<PaginationResult<List<ChatsLocDpx>>> SearchWithPagingAsync(string message, string messageType, string sentBy, DateTime? startDate, DateTime? endDate, int currentPage, int pageSize)
        {
            return await _repository.SearchWithPagingAsync(message, messageType, sentBy, startDate, endDate, currentPage, pageSize);
        }

        public async Task<PaginationResult<List<ChatsLocDpx>>> GetAllWithPagingAsync(int currentPage, int pageSize)
        {
            return await _repository.GetAllWithPagingAsync(currentPage, pageSize);
        }

        public async Task<List<ChatsLocDpx>> GetChatsByUserAsync(int userId)
        {
            return await _repository.GetChatsByUserAsync(userId);
        }

        public async Task<List<ChatsLocDpx>> GetChatsByCoachAsync(int coachId)
        {
            return await _repository.GetChatsByCoachAsync(coachId);
        }

        public async Task<int> CreateAsync(ChatsLocDpx input)
        {
            input.CreatedAt = DateTime.Now;
            return await _repository.CreateAsync(input);
        }

        public async Task<int> UpdateAsync(ChatsLocDpx input)
        {
            return await _repository.UpdateAsync(input);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var chat = await _repository.GetByIdAsync(id);
            if (chat == null) return false;
            return await _repository.RemoveAsync(chat);
        }
    }
}