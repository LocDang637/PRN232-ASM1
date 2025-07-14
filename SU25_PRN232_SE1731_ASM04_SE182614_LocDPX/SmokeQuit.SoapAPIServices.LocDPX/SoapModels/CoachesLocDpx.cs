using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmokeQuit.SoapAPIServices.LocDPX.SoapModels;

[DataContract]
public partial class CoachesLocDpx
{
    [DataMember]
    public int CoachesLocDpxid { get; set; }

    [DataMember]
    public string FullName { get; set; } = null!;

    [DataMember]
    public string Email { get; set; } = null!;

    [DataMember]
    public string? PhoneNumber { get; set; }

    [DataMember]
    public string? Bio { get; set; }

    [DataMember]
    public DateTime? CreatedAt { get; set; }


}
