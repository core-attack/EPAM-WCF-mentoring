using System.Runtime.Serialization;

namespace NorthwindModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [DataContract]
    [KnownType(typeof(UserRole))]
    public partial class Role
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataMember]
        public long RoleId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        [DataMember]
        public string Name { get; set; }

        ICollection<UserRole> UserRoles { get; set; }

        public override string ToString()
        {
            return String.Format("RoleId: {0}, Name: {1}, UserRoles: {2}", RoleId, Name, UserRoles);
        }

    }

    public static class Roles
    {
        public const string MANAGER = "M";
        public const string OPERATION_CONTRACT = "O";
        public const string GUEST = "G";
    }
}
