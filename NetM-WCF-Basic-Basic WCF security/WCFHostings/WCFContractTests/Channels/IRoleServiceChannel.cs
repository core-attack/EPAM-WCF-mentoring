using System.ServiceModel;
using CategoryService;
using Contracts;

namespace WCFContractTests.Channels
{
    public interface IRoleServiceChannel : IClientChannel, IRoleService
    {
    }
}
