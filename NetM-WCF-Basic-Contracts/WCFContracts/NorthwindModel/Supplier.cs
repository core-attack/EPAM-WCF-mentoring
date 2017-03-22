using System.Runtime.Serialization;

namespace NorthwindModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [DataContract]
    [KnownType(typeof(Product))]
    [KnownType(typeof(ICollection<Product>))]
    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        [DataMember]
        public int SupplierID { get; set; }

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
        [Column(TypeName = "ntext")]
        public string HomePage { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }

        public override string ToString()
        {
            return string.Format("SupplierID: {0}, CompanyName: {1}, ContactName: {2}, ContactTitle: {3}, Address: {4}, City: {5}, Region: {6}, PostalCode: {7}, Country: {8}, Phone: {9}, Fax: {10}, HomePage: {11}, Products: {12}", SupplierID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax, HomePage, Products);
        }
    }
}
