using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeQuit.Repositories.LocDPX.ModelExtensions
{
    public class SearchRequest
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class SearchChatRequest : SearchRequest
    {
        public string? Message { get; set; } = "";
        public string? MessageType { get; set; } = "";
        public string? SentBy { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class SearchCoachRequest : SearchRequest
    {
        public string? FullName { get; set; } = "";
        public string? Email { get; set; } = "";
    }
}
