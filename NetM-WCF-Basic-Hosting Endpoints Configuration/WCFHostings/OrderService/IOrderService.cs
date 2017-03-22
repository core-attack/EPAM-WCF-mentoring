using System;
using System.Collections.Generic;
using System.ServiceModel;
using NorthwindModel;
using OrderService.SubscribeServiceReference;

namespace OrderService
{
    [ServiceContract(Namespace = "http://epam.com/Mentoring/WCF/OrderService")]
    public interface IOrderService
    {
        [OperationContract]
        List<OrderDto> OrdersWithStatuses();

        [OperationContract]
        List<Order> Orders();

        [OperationContract]
        Order LastNewOrder();

        [OperationContract]
        Order Details(int id);

        [OperationContract]
        void Create(Order order);

        [OperationContract]
        void Update(Order order);

        [OperationContract]
        void Delete(int id);

        [OperationContract(IsOneWay = true)]
        void SetStatus(OrderWithStatus ows, DateTime? dateTime);

        [OperationContract(IsOneWay = true)]
        void IsInProgress(int orderId, DateTime date);

        [OperationContract(IsOneWay = true)]
        void IsDone(int orderId, DateTime date);
    }
}
