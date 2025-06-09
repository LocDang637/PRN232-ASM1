using SmokeQuit.Repositories.LocDPX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeQuit.Services.LocDPX
{
    public interface IChatsLocDpxService
    {
        Task<List<ChatsLocDpx>> GetAllAsync();
        Task<ChatsLocDpx> GetByIdAsync(int id);
        Task<List<ChatsLocDpx>> SearchAsync(string messageType, string message);
        Task<int> CreateAsync(ChatsLocDpx input);
        Task<int> UpdateAsync(ChatsLocDpx input);
        Task<bool> DeleteAsync(int id);
    }
}
