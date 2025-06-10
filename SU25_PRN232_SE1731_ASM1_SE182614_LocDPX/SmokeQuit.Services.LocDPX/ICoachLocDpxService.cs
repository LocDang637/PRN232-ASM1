using SmokeQuit.Repositories.LocDPX.ModelExtensions;
using SmokeQuit.Repositories.LocDPX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeQuit.Services.LocDPX
{
    public interface ICoachLocDpxService
    {
        Task<List<CoachesLocDpx>> GetAllAsync();
        Task<CoachesLocDpx> GetByIdAsync(int id);
        Task<CoachesLocDpx> GetByEmailAsync(string email);
        Task<List<CoachesLocDpx>> SearchAsync(string fullName, string email);
        Task<PaginationResult<List<CoachesLocDpx>>> SearchWithPagingAsync(string fullName, string email, int currentPage, int pageSize);
        Task<PaginationResult<List<CoachesLocDpx>>> GetAllWithPagingAsync(int currentPage, int pageSize);
        Task<int> CreateAsync(CoachesLocDpx coach);
        Task<int> UpdateAsync(CoachesLocDpx coach);
        Task<bool> DeleteAsync(int id);
    }
}
