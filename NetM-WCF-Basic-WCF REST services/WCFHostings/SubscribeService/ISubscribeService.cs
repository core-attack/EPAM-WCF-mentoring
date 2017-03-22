using System.ServiceModel;
using NorthwindModel;

namespace SubscribeService
{
    [ServiceContract(Namespace = "http://epam.com/Mentoring/WCF/SubscribeService", SessionMode = SessionMode.Required, CallbackContract = typeof(IOrderStatusChangedCallback))]
    public interface ISubscribeService
    {
        [OperationContract(IsOneWay = true)]
        void Subscribe(OrderWithStatus ows);

        [OperationContract(IsOneWay = true)]
        void NotifyEveryone(OrderWithStatus ows);

        [OperationContract(IsOneWay = true)]
        void Unsubscribe(OrderWithStatus ows);
    }

    public interface IOrderStatusChangedCallback
    {
        [OperationContract(IsOneWay = true)]
        void StatusWasChanged(OrderWithStatus order);
    }
}
