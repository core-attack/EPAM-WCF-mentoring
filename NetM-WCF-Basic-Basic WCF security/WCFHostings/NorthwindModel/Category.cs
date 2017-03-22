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
    [KnownType(typeof(byte[]))]
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [DataMember]
        public int CategoryID { get; set; }

        [DataMember]
        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }

        [DataMember]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [DataMember]
        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }

        public override string ToString()
        {
            return string.Format("CategoryID: {0}, CategoryName: {1}, Description: {2}, Picture: {3}, Products: {4}", CategoryID, CategoryName, Description, Picture, Products);
        }
    }
}
