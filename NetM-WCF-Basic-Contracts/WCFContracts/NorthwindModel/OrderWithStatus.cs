using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using NorthwindModel;

namespace NorthwindModel
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Statuses))]
    public class OrderWithStatus
    {
        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public Statuses Status { get; set; }

        [DataMember]
        public Statuses OldStatus { get; set; }
    }
}
