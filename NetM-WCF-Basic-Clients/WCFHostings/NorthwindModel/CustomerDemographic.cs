using System.Runtime.Serialization;

namespace NorthwindModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [DataContract]
    [KnownType(typeof(Customer))]
    [KnownType(typeof(ICollection<Customer>))]
    public partial class CustomerDemographic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerDemographic()
        {
            Customers = new HashSet<Customer>();
        }

        [DataMember]
        [Key]
        [StringLength(10)]
        public string CustomerTypeID { get; set; }

        [DataMember]
        [Column(TypeName = "ntext")]
        public string CustomerDesc { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }

        public override string ToString()
        {
            return string.Format("CustomerTypeID: {0}, CustomerDesc: {1}, Customers: {2}", CustomerTypeID, CustomerDesc, Customers);
        }
    }
}
