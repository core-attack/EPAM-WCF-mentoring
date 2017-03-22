using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using NorthwindModel;

namespace Contracts
{
    [ServiceContract(Namespace = "http://epam.com/Mentoring/WCF/RoleService")]
    public interface IRoleService
    {
        [WebGet(UriTemplate = "/roles")]
        [OperationContract]
        List<Role> Roles();

        [WebGet(UriTemplate = "/roles/{id}")]
        [OperationContract]
        List<UserRole> RolesByUser(string id);

        [WebGet(UriTemplate = "/exist/{id}?pswd={password}")]
        [OperationContract]
        bool IsUserExist(string id, string password);
    }
}
