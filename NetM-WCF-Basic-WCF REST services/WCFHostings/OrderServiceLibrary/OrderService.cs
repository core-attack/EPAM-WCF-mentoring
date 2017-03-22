using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using NorthwindModel;
using OrderServiceLibrary.Helpers;
using OrderServiceLibrary.SubscribeServiceReference;

namespace OrderServiceLibrary
{
    public class OrderService : IOrderService, ISubscribeServiceCallback
    {
        private NorthwindContext db = new NorthwindContext();

        public List<OrderDto> OrdersWithStatuses()
        {
            var query = from x in db.Orders
                select new OrderDto()
                {
                    Status = Statuses.New,
                    Order = x
                };
            foreach (var x in query.ToList())
            {
                x.Status = x.Order.GetStatus();
            }
            return query.ToList();
        }

        public List<Order> Orders()
        {
            return db.Orders.ToList();
        }

        public Order LastNewOrder()
        {
            return db.Orders.LastOrDefault(x => x.OrderDate == null);
        }

        public Order Details(string id)
        {
            Order order = db.Orders.Find(int.Parse(id));
            if (order == null)
            {
                ViewError(new FaultException(ResourseHelper.GetResource("NotExist")));
            }
            return order;
        }

        public void Create(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
        }

        public void Update(Order order)
        {
            if (order != null)
            {
                Order existingOrder = db.Orders.Find(order.OrderID);
                if (existingOrder != null)
                {
                    if (order.OrderDate != existingOrder.OrderDate || order.ShippedDate != existingOrder.ShippedDate)
                        ViewError(new FaultException(string.Format(ResourseHelper.GetResource("NonChangableField"),
                            "OrderDate")));
                    if (order.GetStatus() == Statuses.New)
                    {
                        db.Entry(existingOrder).CurrentValues.SetValues(order);
                        db.SaveChanges();
                    }
                    else
                    {
                        ViewError(new FaultException(
                            string.Format(ResourseHelper.GetResource("OrderWithStatusIsNonChangable"), order.GetStatus())));
                    }
                }
                else
                {
                    throw new FaultException(ResourseHelper.GetResource("NotExist"));
                }
            }
            else
            {
                throw new FaultException(ResourseHelper.GetResource("NotExist"));
            }
        }

        public void SetStatus(OrderWithStatus ows, DateTime? dateTime)
        {
            Order order = Details(ows.OrderId.ToString());
            if (order != null)
            {
                switch (ows.Status)
                {
                    case Statuses.InProgress:
                    {
                        if (order.GetStatus() == Statuses.New)
                        {
                            order.OrderDate = dateTime ?? DateTime.Now;
                            ows.OldStatus = Statuses.New;
                            ows.Status = Statuses.InProgress;
                        }
                        else
                        {
                            Console.WriteLine("3");
                            ViewError(new FaultException(ResourseHelper.GetResource("StatusMismatch")));
                        }
                    }
                        break;
                    case Statuses.Done:
                    {
                        if (order.GetStatus() == Statuses.InProgress)
                        {
                            order.ShippedDate = dateTime ?? DateTime.Now;
                            ows.OldStatus = Statuses.InProgress;
                            ows.Status = Statuses.Done;
                        }
                        else
                        {
                            Console.WriteLine("2");
                            ViewError(new FaultException(ResourseHelper.GetResource("StatusMismatch")));
                        }
                    }
                        break;
                    default:
                    {
                        Console.WriteLine("1");
                        ViewError(new FaultException(ResourseHelper.GetResource("StatusMismatch")));
                    }
                        break;
                }
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                Console.WriteLine("пытаемся уведомить...");
                try
                {
                    using (var client = new SubscribeServiceClient(new InstanceContext(this)))
                    {
                        Console.WriteLine("пытаемся уведомить...");
                        client.NotifyEveryone(ows);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                ViewError(new FaultException(ResourseHelper.GetResource("NotExist")));
            }
            Console.WriteLine("выходим");
        }

        public void IsInProgress(string orderId, DateTime date)
        {
            Order order = Details(orderId);
            if (order != null)
            {
                if (order.RequiredDate < DateTime.Now)
                {
                    ViewError(
                        new FaultException(string.Format(ResourseHelper.GetResource("OrderRequiredDateIsLessThanNow"),
                            order.RequiredDate, DateTime.Now)));
                }
                order.OrderDate = date;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                ViewError(new FaultException(ResourseHelper.GetResource("NotExist")));
            }
        }

        public void IsDone(string orderId, DateTime date)
        {
            Order order = Details(orderId);
            if (order != null)
            {
                if (order.GetStatus() == Statuses.InProgress)
                {
                    order.ShippedDate = date;
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    ViewError(new FaultException(ResourseHelper.GetResource("StatusMismatch")));
                }
            }
            else
            {
                ViewError(new FaultException(ResourseHelper.GetResource("NotExist")));
            }
        }

        public void Delete(string id)
        {
            Order order = db.Orders.Find(int.Parse(id));
            if (order != null)
            {
                if (order.GetStatus() != Statuses.Done)
                {
                    db.Orders.Remove(order);
                    db.SaveChanges();
                }
                else
                {
                    ViewError(new FaultException(string.Format(ResourseHelper.GetResource("RemovingIsForbidden"),
                        order.GetStatus())));
                }
            }
            else
            {
                ViewError(new FaultException(ResourseHelper.GetResource("NotExist")));
            }
        }

        public void StatusWasChanged(OrderWithStatus ows)
        {
            Console.WriteLine("Status by Order #{0} was chenged from {2} to {1}", ows.OrderId, ows.Status, ows.OldStatus);
        }

        public void ViewError(FaultException e)
        {
            Console.WriteLine(e.Message);
            throw e;
        }
    }
}