using System.Runtime.Serialization;

namespace NorthwindModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [DataContract]
    [KnownType(typeof(Employee))]
    [KnownType(typeof(Region))]
    [KnownType(typeof(ICollection<Employee>))]
    public partial class Territory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Territory()
        {
            Employees = new HashSet<Employee>();
        }

        [DataMember]
        [StringLength(20)]
        public string TerritoryID { get; set; }

        [DataMember]
        [Required]
        [StringLength(50)]
        public string TerritoryDescription { get; set; }

        [DataMember]
        public int RegionID { get; set; }

        [DataMember]
        public virtual Region Region { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }

        public override string ToString()
        {
            return string.Format("TerritoryID: {0}, TerritoryDescription: {1}, RegionID: {2}, Region: {3}, Employees: {4}", TerritoryID, TerritoryDescription, RegionID, Region, Employees);
        }
    }
}
