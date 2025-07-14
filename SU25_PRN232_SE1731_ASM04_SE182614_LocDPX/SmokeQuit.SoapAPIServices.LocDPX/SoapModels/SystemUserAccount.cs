using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmokeQuit.SoapAPIServices.LocDPX.SoapModels;

[DataContract]
public partial class SystemUserAccount
{
    [DataMember]
    public int UserAccountId { get; set; }

    [DataMember]
    public string UserName { get; set; } = null!;

    [DataMember]
    public string Password { get; set; } = null!;

    [DataMember]
    public string FullName { get; set; } = null!;

    [DataMember]
    public string Email { get; set; } = null!;

    [DataMember]
    public string Phone { get; set; } = null!;

    [DataMember]
    public string EmployeeCode { get; set; } = null!;

    [DataMember]
    public int RoleId { get; set; }

    [DataMember]
    public string? RequestCode { get; set; }

    [DataMember]
    public DateTime? CreatedDate { get; set; }

    [DataMember]
    public string? ApplicationCode { get; set; }

    [DataMember]
    public string? CreatedBy { get; set; }

    [DataMember]
    public DateTime? ModifiedDate { get; set; }

    [DataMember]
    public string? ModifiedBy { get; set; }

    [DataMember]
    public bool IsActive { get; set; }

}
