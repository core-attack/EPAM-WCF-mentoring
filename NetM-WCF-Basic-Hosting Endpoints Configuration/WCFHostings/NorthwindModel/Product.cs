using System.Runtime.Serialization;

namespace NorthwindModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [DataContract]
    [KnownType(typeof(Category))]
    [KnownType(typeof(Order_Detail))]
    [KnownType(typeof(Supplier))]
    [KnownType(typeof(ICollection<Order_Detail>))]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Order_Details = new HashSet<Order_Detail>();
        }

        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        [DataMember]
        public int? SupplierID { get; set; }

        [DataMember]
        public int? CategoryID { get; set; }

        [DataMember]
        [StringLength(20)]
        public string QuantityPerUnit { get; set; }

        [DataMember]
        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        [DataMember]
        public short? UnitsInStock { get; set; }

        [DataMember]
        public short? UnitsOnOrder { get; set; }

        [DataMember]
        public short? ReorderLevel { get; set; }

        [DataMember]
        public bool Discontinued { get; set; }

        [DataMember]
        public virtual Category Category { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Details { get; set; }

        [DataMember]
        public virtual Supplier Supplier { get; set; }

        public override string ToString()
        {
            return string.Format("ProductID: {0}, ProductName: {1}, SupplierID: {2}, CategoryID: {3}, QuantityPerUnit: {4}, UnitPrice: {5}, UnitsInStock: {6}, UnitsOnOrder: {7}, ReorderLevel: {8}, Discontinued: {9}, Category: {10}, Order_Details: {11}, Supplier: {12}", ProductID, ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued, Category, Order_Details, Supplier);
        }
    }
}
