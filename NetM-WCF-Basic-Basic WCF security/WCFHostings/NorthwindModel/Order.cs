using System.Runtime.Serialization;

namespace NorthwindModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [DataContract]
    [KnownType(typeof(Customer))]
    [KnownType(typeof(Employee))]
    [KnownType(typeof(Shipper))]
    [KnownType(typeof(Order_Detail))]
    [KnownType(typeof(Statuses))]
    [KnownType(typeof(Category))]
    [KnownType(typeof(CustomerDemographic))]
    [KnownType(typeof(Order))]
    [KnownType(typeof(Product))]
    [KnownType(typeof(Region))]
    [KnownType(typeof(Supplier))]
    [KnownType(typeof(Territory))]
    [KnownType(typeof(ICollection<Order_Detail>))]
    public class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Order_Details = new HashSet<Order_Detail>();
        }

        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        [StringLength(5)]
        public string CustomerID { get; set; }

        [DataMember]
        public int? EmployeeID { get; set; }

        [DataMember]
        public DateTime? OrderDate { get; set; }

        [DataMember]
        public DateTime? RequiredDate { get; set; }

        [DataMember]
        public DateTime? ShippedDate { get; set; }

        [DataMember]
        public int? ShipVia { get; set; }

        [DataMember]
        [Column(TypeName = "money")]
        public decimal? Freight { get; set; }

        [DataMember]
        [StringLength(40)]
        public string ShipName { get; set; }

        [DataMember]
        [StringLength(60)]
        public string ShipAddress { get; set; }

        [DataMember]
        [StringLength(15)]
        public string ShipCity { get; set; }

        [DataMember]
        [StringLength(15)]
        public string ShipRegion { get; set; }

        [DataMember]
        [StringLength(10)]
        public string ShipPostalCode { get; set; }

        [DataMember]
        [StringLength(15)]
        public string ShipCountry { get; set; }

        [DataMember]
        public virtual Customer Customer { get; set; }

        [DataMember]
        public virtual Employee Employee { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Details { get; set; }

        [DataMember]
        public virtual Shipper Shipper { get; set; }

        //[OperationContract] придумать что-нибудь

        public override string ToString()
        {
            return string.Format("OrderID: {0}, CustomerID: {1}, EmployeeID: {2}, OrderDate: {3}, RequiredDate: {4}, ShippedDate: {5}, ShipVia: {6}, Freight: {7}, ShipName: {8}, ShipAddress: {9}, ShipCity: {10}, ShipRegion: {11}, ShipPostalCode: {12}, ShipCountry: {13}, Customer: {14}, Employee: {15}, Order_Details: {16}, Shipper: {17}", OrderID, CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry, Customer, Employee, Order_Details, Shipper);
        }

        public Statuses GetStatus()
        {
            if (!OrderDate.HasValue)
                return Statuses.New;
            if (!ShippedDate.HasValue)
                return Statuses.InProgress;
            return Statuses.Done;
        }
    }
}
