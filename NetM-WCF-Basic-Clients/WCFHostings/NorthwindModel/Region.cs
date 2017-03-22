using System.Runtime.Serialization;

namespace NorthwindModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Region")]
    [DataContract]
    [KnownType(typeof(Territory))]
    [KnownType(typeof(ICollection<Territory>))]
    public partial class Region
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Region()
        {
            Territories = new HashSet<Territory>();
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RegionID { get; set; }

        [DataMember]
        [Required]
        [StringLength(50)]
        public string RegionDescription { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Territory> Territories { get; set; }

        public override string ToString()
        {
            return string.Format("RegionID: {0}, RegionDescription: {1}, Territories: {2}", RegionID, RegionDescription, Territories);
        }
    }
}
