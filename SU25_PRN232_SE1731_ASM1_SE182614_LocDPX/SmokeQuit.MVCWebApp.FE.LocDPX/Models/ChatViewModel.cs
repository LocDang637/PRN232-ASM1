namespace SmokeQuit.MVCWebApp.FE.LocDPX.Models
{
    public class ChatViewModel
    {
        public int ChatsLocDpxid { get; set; }
        public int UserId { get; set; }
        public int CoachId { get; set; }
        public string Message { get; set; }
        public string SentBy { get; set; }
        public string MessageType { get; set; }
        public bool IsRead { get; set; }
        public string AttachmentUrl { get; set; }
        public DateTime? ResponseTime { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CoachName { get; set; }
        public string UserName { get; set; }
    }
}
