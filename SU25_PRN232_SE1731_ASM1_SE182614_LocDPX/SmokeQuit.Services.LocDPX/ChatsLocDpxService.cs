using SmokeQuit.Repositories.LocDPX;
using SmokeQuit.Repositories.LocDPX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<int> CreateAsync(ChatsLocDpx input)
        {
            return await _repository.CreateAsync(input);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var chat = await _repository.GetById(id);
            return await _repository.RemoveAsync(chat);
        }

        public async Task<List<ChatsLocDpx>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ChatsLocDpx> GetByIdAsync(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<List<ChatsLocDpx>> SearchAsync(string messageType, string message)
        {
            return await _repository.SearchAsync(messageType, message);
        }

        public async Task<int> UpdateAsync(ChatsLocDpx input)
        {
            return await _repository.UpdateAsync(input);
        }
    }
}
