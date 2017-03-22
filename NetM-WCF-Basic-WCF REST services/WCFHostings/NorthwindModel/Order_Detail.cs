using System.Runtime.Serialization;

namespace NorthwindModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order Details")]
    [DataContract]
    [KnownType(typeof(Order))]
    [KnownType(typeof(Product))]
    public partial class Order_Detail
    {
        [DataMember]
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [DataMember]
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [DataMember]
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public short Quantity { get; set; }

        [DataMember]
        public float Discount { get; set; }

        [DataMember]
        public virtual Order Order { get; set; }

        [DataMember]
        public virtual Product Product { get; set; }

        public override string ToString()
        {
            return string.Format("OrderID: {0}, ProductID: {1}, UnitPrice: {2}, Quantity: {3}, Discount: {4}, Order: {5}, Product: {6}", OrderID, ProductID, UnitPrice, Quantity, Discount, Order, Product);
        }
    }
}
