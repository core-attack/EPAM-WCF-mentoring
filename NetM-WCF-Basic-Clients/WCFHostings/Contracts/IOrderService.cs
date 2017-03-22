using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using NorthwindModel;

namespace OrderService
{
    [ServiceContract(Namespace = "http://epam.com/Mentoring/WCF/OrderService")]
    public interface IOrderService
    {
        [WebGet(UriTemplate = "/orders/withstatuses")]
        [OperationContract]
        List<OrderDto> OrdersWithStatuses();

        [WebGet(UriTemplate = "/orders/")]
        [OperationContract]
        List<Order> Orders();

        [WebGet(UriTemplate = "/orders/last")]
        [OperationContract]
        Order LastNewOrder();

        [WebGet(UriTemplate = "/orders/details/{id}")]
        [OperationContract]
        Order Details(string id);

        [WebInvoke(UriTemplate = "/orders/create", Method = "PUT")]
        [OperationContract]
        void Create(Order order);

        [WebInvoke(UriTemplate = "/orders/update", Method = "PUT")]
        [OperationContract]
        void Update(Order order);

        [WebInvoke(UriTemplate = "/orders/delete/{id}", Method = "DELETE")]
        [OperationContract]
        void Delete(string id);

        [WebInvoke(UriTemplate = "/orders/setstatus", Method = "PUT", BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract(IsOneWay = true)]
        void SetStatus(OrderWithStatus ows, DateTime? dateTime);

        [WebInvoke(UriTemplate = "/orders/setinprogress?id={orderId}", Method = "PUT", BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract(IsOneWay = true)]
        void IsInProgress(string orderId, DateTime date);

        [WebInvoke(UriTemplate = "/orders/setisdone?id={orderId}", Method = "PUT", BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract(IsOneWay = true)]
        void IsDone(string orderId, DateTime date);
    }
}
