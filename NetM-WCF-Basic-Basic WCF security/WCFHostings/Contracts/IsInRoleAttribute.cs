using System;
using System.Linq;
using System.ServiceModel;
using NorthwindModel;

namespace Contracts
{
    public class IsInRoleAttribute :  Attribute
    {
        static NorthwindContext Context = new NorthwindContext();

        public IsInRoleAttribute()
        {
            var user = GetUserByName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
            bool hasAccess = false;

            if (Context != null && user != null)
            {
                var userRoles = Context.UserRoles.Where(r => r.UserId == user.UserId).ToList();
                foreach (var userRole in userRoles)
                {
                    var role = Context.Roles.FirstOrDefault(r => r.RoleId == userRole.RoleId);

                    if (role.Name.Equals(Role))
                    {
                        hasAccess = true;
                    }
                }
            }

            if (!hasAccess)
            {
                throw new Exception(string.Format("У пользователя {0} нет прав на совершения этого действия!", user.Name));
            }
        }

        public string Role { get; set; }

        private User GetUserByName(string name)
        {
            var users = Context.Users.Where(u => u.Name.Equals(name));

            if (users.Count() > 1)
            {
                throw new Exception(
                    "Ошибка структуры базы данных. Поле Name для таблицы Users должно содержать уникальные значения.");
            }

            if (users.Any())
            {
                return users.ToList()[0];
            }

            return null;
        }
    }
}
