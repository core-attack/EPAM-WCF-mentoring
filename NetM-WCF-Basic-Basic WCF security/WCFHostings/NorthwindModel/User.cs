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
    public partial class User
    {
        [DataMember]
        public long UserId { get; set; }

        [Required]
        [StringLength(50)]
        [DataMember]
        public string Name { get; set; }

        [StringLength(50)]
        [DataMember]
        public string Password { get; set; }

        ICollection<UserRole> UserRoles { get; set; }

        public override string ToString()
        {
            return string.Format("UserId: {0}, Name: {1}, Password: {2}, UserRoles: {3}", UserId, Name, Password, UserRoles);
        }
    }
}
