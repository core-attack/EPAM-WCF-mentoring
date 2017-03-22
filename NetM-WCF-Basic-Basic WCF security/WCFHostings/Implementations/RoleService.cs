using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using NorthwindModel;

namespace Implementations
{
    public class RoleService : IRoleService
    {
        private NorthwindContext db = new NorthwindContext();

        public List<Role> Roles()
        {
            return db.Roles.ToList();
        }

        public List<UserRole> RolesByUser(string id)
        {
            var Id = long.Parse(id);
            var v = db.UserRoles.Where(r => r.UserId == Id).ToList();
            return db.UserRoles.Where(r => r.UserId == Id).ToList();
        }

        public bool IsUserExist(string id, string password)
        {
            return Helpers.UserPassword.IsUserExist(id, password);
        }
    }
}
