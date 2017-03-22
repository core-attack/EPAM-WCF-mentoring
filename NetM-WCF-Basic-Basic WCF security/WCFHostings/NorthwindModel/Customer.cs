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
    [KnownType(typeof(CustomerDemographic))]
    [KnownType(typeof(ICollection<Order>))]
    [KnownType(typeof(ICollection<CustomerDemographic>))]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Orders = new HashSet<Order>();
            CustomerDemographics = new HashSet<CustomerDemographic>();
        }

        [DataMember]
        [StringLength(5)]
        public string CustomerID { get; set; }

        [DataMember]
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [DataMember]
        [StringLength(30)]
        public string ContactName { get; set; }

        [DataMember]
        [StringLength(30)]
        public string ContactTitle { get; set; }

        [DataMember]
        [StringLength(60)]
        public string Address { get; set; }

        [DataMember]
        [StringLength(15)]
        public string City { get; set; }

        [DataMember]
        [StringLength(15)]
        public string Region { get; set; }

        [DataMember]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [DataMember]
        [StringLength(15)]
        public string Country { get; set; }

        [DataMember]
        [StringLength(24)]
        public string Phone { get; set; }


        [DataMember]
        [StringLength(24)]
        public string Fax { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerDemographic> CustomerDemographics { get; set; }

        public override string ToString()
        {
            return string.Format("CustomerID: {0}, CompanyName: {1}, ContactName: {2}, ContactTitle: {3}, Address: {4}, City: {5}, Region: {6}, PostalCode: {7}, Country: {8}, Phone: {9}, Fax: {10}, Orders: {11}, CustomerDemographics: {12}", CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax, Orders, CustomerDemographics);
        }
    }
}
