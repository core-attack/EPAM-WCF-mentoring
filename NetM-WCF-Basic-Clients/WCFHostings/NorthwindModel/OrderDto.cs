using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindModel
{
    [DataContract]
    [KnownType(typeof(Order))]
    [KnownType(typeof(Statuses))]
    public class OrderDto
    {
        [DataMember]
        public Order Order { get; set; }

        [DataMember]
        public Statuses Status { get; set; }

        public override string ToString()
        {
            return string.Format("Order: {0}, Status: {1}", Order, Status);
        }
    }
}
