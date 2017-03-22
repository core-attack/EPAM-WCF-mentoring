using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NorthwindModel;

namespace OrderServiceLibrary
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