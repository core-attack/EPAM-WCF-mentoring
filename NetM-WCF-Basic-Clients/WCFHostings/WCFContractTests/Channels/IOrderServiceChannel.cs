using System.ServiceModel;
using OrderService;

namespace WCFContractTests.Channels
{
    interface IOrderServiceChannel : IClientChannel, IOrderService
    {
    }
}
