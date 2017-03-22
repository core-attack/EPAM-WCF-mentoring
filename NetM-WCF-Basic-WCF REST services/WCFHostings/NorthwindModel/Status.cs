using System.Runtime.Serialization;

namespace NorthwindModel
{
    [DataContract]
    public enum Statuses
    {
        [EnumMember]
        New,

        [EnumMember]
        InProgress,

        [EnumMember]
        Done
    }
}
