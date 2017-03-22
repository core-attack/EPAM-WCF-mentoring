using System.ServiceModel;
using CategoryService;

namespace WCFContractTests.Channels
{
    public interface ICategoryServiceChannel : IClientChannel, ICategoryService
    {
    }
}
