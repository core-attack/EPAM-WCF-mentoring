using System;
using System.Linq;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCFContractTests.OrderServiceReference;

namespace WCFContractTests
{
    [TestClass]
    public class OrderServiceTest
    {
        [TestMethod]
        public void OrdersTest()
        {
            using (var client = new OrderServiceClient())
            {
                foreach (var order in client.Orders())
                {
                    Console.WriteLine(ToString(order));
                    Console.WriteLine(order.ToString());
                }
            }
        }

        [TestMethod]
        public void OrdersWithStatusesTest()
        {
            using (var client = new OrderServiceClient())
            {
                foreach (var orderDto in client.OrdersWithStatuses())
                {
                    Console.WriteLine("{0} - status: {1}", ToString(orderDto.Order), orderDto.Status);
                }
            }
        }

        [TestMethod]
        public void CreateTest()
        {
            Order firstOrder = new Order()
            {
                ShipName = "testtest",
                ShipAddress = "test",
                ShipCity = "testtesttest",
                OrderDate = DateTime.Now
            };
            using (var client = new OrderServiceClient())
            {
                client.Create(firstOrder);
                Console.WriteLine(ToString(client.Orders().Last()));
            }
        }

        [TestMethod]
        public void DetailsTest()
        {
            Order firstOrder = new Order()
            {
                OrderID = 10248,
                CustomerID = "VINET",
                EmployeeID = 5,
                ShipVia = 3,
                ShipName = "Vins et alcools Chevalier",
                ShipAddress = "59 rue de l'Abbaye",
                ShipCity = "Reims"

            };
            using (var client = new OrderServiceClient())
            {
                Order order = client.Details(10248);
                if (order.CustomerID == firstOrder.CustomerID &&
                    order.OrderID == firstOrder.OrderID &&
                    order.EmployeeID == firstOrder.EmployeeID &&
                    order.ShipVia == firstOrder.ShipVia &&
                    order.ShipName == firstOrder.ShipName &&
                    order.ShipAddress == firstOrder.ShipAddress &&
                    order.ShipCity == firstOrder.ShipCity)
                {
                    Console.WriteLine("test done successfully");
                    Console.WriteLine(ToString(order));

                }
                else
                {
                    Assert.Fail("test failed");
                }
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            using (var client = new OrderServiceClient())
            {
                var orderId = 11177;//для проверки failt exeption использовать -> 10248;
                Order order = client.Details(orderId);
                var shipname = order.ShipName;
                var testString = "test" + DateTime.Now.ToLongTimeString();
                order.ShipName = testString;

                client.Update(order);
                Order updatedOrder = client.Details(orderId);

                if (updatedOrder.ShipName == order.ShipName)
                {
                    Console.WriteLine(ToString(updatedOrder));
                }
                else
                {
                    Assert.Fail("test failed");
                }

                order.ShipName = shipname;
                client.Update(order);
            }
        }

        [TestMethod]
        public void SetInProgressStatusTest()
        {
            using (var client = new OrderServiceClient())
            {
                client.Create(new Order()
                {
                    ShipName = "testtest",
                    ShipAddress = "test",
                    ShipCity = "testtesttest"
                });
                Order order = client.Orders().Last(x => x.OrderDate == null);
                int id = order.OrderID;
                Console.WriteLine("Was:" + ToString(order));
                client.IsInProgress(id, DateTime.Now);
                Console.WriteLine("Is:" + ToString(client.Details(id)));
            }
        }

        [TestMethod]
        public void SetIsDoneStatusTest()
        {
            using (var client = new OrderServiceClient())
            {
                client.Create(new Order()
                {
                    ShipName = "testtest",
                    ShipAddress = "test",
                    ShipCity = "testtesttest"
                });
                Order order = client.Orders().Last(x => x.OrderDate == null);
                int id = order.OrderID;

                Console.WriteLine("Was:" + ToString(order));
                client.IsDone(id, DateTime.Now);
                Console.WriteLine("Is:" + ToString(client.Details(id)));
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (var client = new OrderServiceClient())
            {
                var count = client.Orders().Count();
                client.Delete(client.Orders().Last().OrderID);
                var newCount = client.Orders().Count();
                if (count == newCount)
                {
                    Assert.Fail("test failed");
                }
                Console.WriteLine("test done successfully");
            }
        }

        public string ToString(Order order)
        {
            return string.Format("OrderID: {0}, CustomerID: {1}, EmployeeID: {2}, OrderDate: {3}, RequiredDate: {4}, ShippedDate: {5}, ShipVia: {6}, Freight: {7}, ShipName: {8}, ShipAddress: {9}, ShipCity: {10}, ShipRegion: {11}, ShipPostalCode: {12}, ShipCountry: {13}, Customer: {14}, Employee: {15}, Order_Details: {16}, Shipper: {17}", order.OrderID, order.CustomerID, order.EmployeeID, order.OrderDate, order.RequiredDate, order.ShippedDate, order.ShipVia, order.Freight, order.ShipName, order.ShipAddress, order.ShipCity, order.ShipRegion, order.ShipPostalCode, order.ShipCountry, order.Customer, order.Employee, order.Order_Details, order.Shipper);
        }

        //1.	Запрошенной по ID сущности нет (там, где обращение идет по конкретному ID – например, при редактировании)
        [TestMethod]
        public void DetailsNoExistRecordTest()
        {
            using (var client = new OrderServiceClient())
            {
                Order order = client.Details(1);
            }
        }

        //Fault: 2.	Попытка изменения заказа в статусе «В работе» или «Выполнен».
        [TestMethod]
        public void UpdateDoneOrderTest()
        {
            using (var client = new OrderServiceClient())
            {
                Order order = client.Details(10248);
                var temp = order.ShipName;
                order.ShipName = "test";
                client.Update(order);
                Order updatedOrder = client.Details(10248);
                if (updatedOrder.ShipName == order.ShipName)
                {
                    Console.WriteLine(ToString(order));
                }
                else
                {
                    throw new Exception("test failed");
                }
                order.ShipName = temp;
                client.Update(order);
            }
        }

        //3.	Попытка удаления выполненного заказа
        [TestMethod]
        public void DeleteNoExistingRecordTest()
        {
            using (var client = new OrderServiceClient())
            {
                var count = client.Orders().Count();
                client.Delete(1);
                var newCount = client.Orders().Count();
                if (count == newCount)
                {
                    Assert.Fail("test failed");
                }
                Console.WriteLine("test done successfully");
            }
        }

        //4.	Попытка запустить в работу заказа у которого требуемая дата выполнения меньше текущей (т.е. срок выполнения уже прошел)
        [TestMethod]
        public void FaultSetInProgressStatusTest()
        {
            using (var client = new OrderServiceClient())
            {
                try
                {
                    int id = 10248;
                    client.IsInProgress(id, DateTime.Now);
                }
                catch (FaultException e)
                {
                    Assert.Fail(e.Message);
                }
            }
        }

        //5.	Попытка поменять статус заказу, кроме следующих 2-х разрешенных случаев (напомню, что статус меняется специальными методами):
        //a.  «Новый» на «В работе»
        //b.  «В работе» на «Выполненный»
        [TestMethod]
        public void FaultSetDoneStatusTest()
        {
            using (var client = new OrderServiceClient())
            {
                try
                {
                    int id = 11166;
                    client.IsDone(id, DateTime.Now);
                }
                catch (FaultException e)
                {
                    Assert.Fail(e.Message);
                }
            }
        }

    }
}
