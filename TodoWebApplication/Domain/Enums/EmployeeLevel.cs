using System.Runtime.Serialization;

namespace TodoWebApplication.Domain.Enums
{
    public enum EmployeeLevel
    {
        [EnumMember(Value = "Junior")]
        Junior = 1,

        [EnumMember(Value = "Mid")]
        Mid = 2,

        [EnumMember(Value = "Senior")]
        Senior = 3
    }
}
