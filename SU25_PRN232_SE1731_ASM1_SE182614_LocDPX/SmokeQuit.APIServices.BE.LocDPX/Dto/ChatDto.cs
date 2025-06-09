namespace SmokeQuit.APIServices.BE.LocDPX.Dto
{
    public class ChatDto
    {
        public int UserId { get; set; }

        public int CoachId { get; set; }

        public string Message { get; set; }

        public string SentBy { get; set; }

        public string MessageType { get; set; }

        public bool IsRead { get; set; }

        public string AttachmentUrl { get; set; }

        public DateTime? ResponseTime { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
