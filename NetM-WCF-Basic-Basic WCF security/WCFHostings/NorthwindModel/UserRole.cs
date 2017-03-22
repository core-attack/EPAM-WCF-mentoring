using System.Runtime.Serialization;

namespace NorthwindModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [DataContract]
    public partial class UserRole
    {
        [DataMember]
        public long UserRoleId { get; set; }

        [DataMember]
        public long UserId { get; set; }

        [DataMember]
        public long RoleId { get; set; }

        public override string ToString()
        {
            return string.Format("UserRoleId: {0}, UserId: {1}, RoleId: {2}", UserRoleId, UserId, RoleId);
        }
    }
}
