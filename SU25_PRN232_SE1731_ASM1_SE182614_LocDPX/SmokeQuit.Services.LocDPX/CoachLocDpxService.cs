using SmokeQuit.Repositories.LocDPX;
using SmokeQuit.Repositories.LocDPX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeQuit.Services.LocDPX
{
    public class CoachLocDpxService
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

        public async Task<List<CoachesLocDpx>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CoachesLocDpx> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> Add(string coachEmail)
        {
            var coach = new CoachesLocDpx
            {
                FullName = "Mock Name",
                Email = coachEmail.ToLower(),
                PhoneNumber = "1234567890",
                Bio = "Mock Bio",
                CreatedAt = DateTime.Now
            };
            return await _repository.CreateAsync(coach);
        }

        public async Task<CoachesLocDpx> GetByEmail(string email)
        {
            return await _repository.GetByEmail(email.ToLower());
        }
    }
}
