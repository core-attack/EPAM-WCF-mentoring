using System.Runtime.Serialization;

namespace NorthwindModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [DataContract]
    [KnownType(typeof(Order))]
    [KnownType(typeof(ICollection<Order>))]
    public partial class Shipper
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        [DataMember]
        public int ShipperID { get; set; }

        [DataMember]
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [DataMember]
        [StringLength(24)]
        public string Phone { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        public override string ToString()
        {
            return string.Format("ShipperID: {0}, CompanyName: {1}, Phone: {2}, Orders: {3}", ShipperID, CompanyName, Phone, Orders);
        }
    }
}
