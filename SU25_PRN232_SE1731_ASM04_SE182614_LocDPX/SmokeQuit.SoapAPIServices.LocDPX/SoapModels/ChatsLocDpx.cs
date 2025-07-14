using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmokeQuit.SoapAPIServices.LocDPX.SoapModels;

[DataContract]
public partial class ChatsLocDpx
{
    [DataMember]
    public int ChatsLocDpxid { get; set; }

    [DataMember]
    public int UserId { get; set; }

    [DataMember]
    public int CoachId { get; set; }

    [DataMember]
    public string Message { get; set; } = null!;

    [DataMember]
    public string SentBy { get; set; } = null!;

    [DataMember]
    public string MessageType { get; set; } = null!;

    [DataMember]
    public bool IsRead { get; set; }

    [DataMember]
    public string? AttachmentUrl { get; set; }

    [DataMember]
    public DateTime? ResponseTime { get; set; }

    [DataMember]
    public DateTime? CreatedAt { get; set; }


}
