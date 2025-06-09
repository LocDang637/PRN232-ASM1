using Microsoft.EntityFrameworkCore;
using SmokeQuit.Repositories.LocDPX.Basic;
using SmokeQuit.Repositories.LocDPX.DBContext;
using SmokeQuit.Repositories.LocDPX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeQuit.Repositories.LocDPX
{
    public class ChatsLocDpxRepository : GenericRepository<ChatsLocDpx>
    {
        public ChatsLocDpxRepository()
        {
            _context ??= new SU25_PRN232_SE1731_G6_SmokeQuitContext();
        }
        public ChatsLocDpxRepository(SU25_PRN232_SE1731_G6_SmokeQuitContext context)
        {
            _context = context;
        }

        //Override method
        public async Task<List<ChatsLocDpx>> GetAllAsync()
        {
            var chat = await _context.ChatsLocDpxes.Include(x => x.Coach).Include(x => x.User).ToListAsync();
            return chat;
        }
        
        //Override method
        public async Task<ChatsLocDpx> GetById(int id)
        {
            var chat = await _context.ChatsLocDpxes.Include(x => x.Coach).Include(x => x.User).FirstOrDefaultAsync(x => x.ChatsLocDpxid.Equals(id));
            return chat;
        }

        public async Task<List<ChatsLocDpx>> SearchAsync ( string messageType, string message)
        {
            var tempChat = await _context.ChatsLocDpxes.Include(x => x.Coach)
                .Where(x => x.MessageType.Contains(messageType) || string.IsNullOrEmpty(messageType)
                                &&(x.Message.Contains(message)) || string.IsNullOrEmpty(message))
                .ToListAsync();

            return tempChat ?? new List<ChatsLocDpx>();
        }
    }
    
    

    }
