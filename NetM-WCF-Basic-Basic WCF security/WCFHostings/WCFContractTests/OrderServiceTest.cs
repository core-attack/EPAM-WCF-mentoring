using System;
using System.Linq;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindModel;
using WCFContractTests.Channels;

namespace WCFContractTests
{
    [TestClass]
    public class OrderServiceTest
    {
        //private const string Channel = "ConsoleHostOrderServiceConfigurationWS";//wsHttpBinding
        //private const string Channel = "ConsoleHostOrderServiceConfiguration";//basicHttpBinding
        private const string Channel = "IISOrderServiceConfiguration";

        private const string UserName = "Anton";
        private const string Password = "pass3";

        [TestMethod]
        public void OrdersTest()
        {
            var channelFactory = new ChannelFactory<IOrderServiceChannel>(Channel);

            channelFactory.Credentials.UserName.UserName = UserName;
            channelFactory.Credentials.UserName.Password = Password;

            using (var orderServiceChannel = channelFactory.CreateChannel())
            {
                foreach (var order in orderServiceChannel.Orders())
                {
                    Console.WriteLine(ToString(order));
                    Console.WriteLine(order.ToString());
                }
            }
        }

        [TestMethod]
        public void OrdersWithStatusesTest()
        {
            var channelFactory = new ChannelFactory<IOrderServiceChannel>(Channel);

            channelFactory.Credentials.UserName.UserName = UserName;
            channelFactory.Credentials.UserName.Password = Password;

            using (var channel = channelFactory.CreateChannel())
            {
                foreach (var orderDto in channel.OrdersWithStatuses())
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

            var channelFactory = new ChannelFactory<IOrderServiceChannel>(Channel);

            channelFactory.Credentials.UserName.UserName = UserName;
            channelFactory.Credentials.UserName.Password = Password;

            using (var channel = channelFactory.CreateChannel())
            {
                channel.Create(firstOrder);
                Console.WriteLine(ToString(channel.Orders().Last()));
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

            var channelFactory = new ChannelFactory<IOrderServiceChannel>(Channel);

            channelFactory.Credentials.UserName.UserName = UserName;
            channelFactory.Credentials.UserName.Password = Password;

            using (var channel = channelFactory.CreateChannel())
            {
                Order order = channel.Details(10248.ToString());
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
            var channelFactory = new ChannelFactory<IOrderServiceChannel>(Channel);

            channelFactory.Credentials.UserName.UserName = UserName;
            channelFactory.Credentials.UserName.Password = Password;

            using (var channel = channelFactory.CreateChannel())
            {
                var orderId = 11177;//для проверки failt exeption использовать -> 10248;
                Order order = channel.Details(orderId.ToString());
                var shipname = order.ShipName;
                var testString = "test" + DateTime.Now.ToLongTimeString();
                order.ShipName = testString;

                channel.Update(order);
                Order updatedOrder = channel.Details(orderId.ToString());

                if (updatedOrder.ShipName == order.ShipName)
                {
                    Console.WriteLine(ToString(updatedOrder));
                }
                else
                {
                    Assert.Fail("test failed");
                }

                order.ShipName = shipname;
                channel.Update(order);
            }
        }

        [TestMethod]
        public void SetInProgressStatusTest()
        {
            try
            {
                var channelFactory = new ChannelFactory<IOrderServiceChannel>(Channel);

                channelFactory.Credentials.UserName.UserName = UserName;
                channelFactory.Credentials.UserName.Password = Password;

                using (var channel = channelFactory.CreateChannel())
                {
                    channel.Create(new Order()
                    {
                        ShipName = "testtest",
                        ShipAddress = "test",
                        ShipCity = "testtesttest"
                    });
                    Order order = channel.Orders().Last(x => x.OrderDate == null);
                    int id = order.OrderID;
                    Console.WriteLine("Was:" + ToString(order));
                    channel.IsInProgress(id.ToString(), DateTime.Now);
                    Console.WriteLine("Is:" + ToString(channel.Details(id.ToString())));
                }
            }
            catch (FaultException e)
            {
                Console.WriteLine("Error {0}", e.Message);
            }
        }

        [TestMethod]
        public void SetIsDoneStatusTest()
        {
            var channelFactory = new ChannelFactory<IOrderServiceChannel>(Channel);

            channelFactory.Credentials.UserName.UserName = UserName;
            channelFactory.Credentials.UserName.Password = Password;

            using (var channel = channelFactory.CreateChannel())
            {
                channel.Create(new Order()
                {
                    ShipName = "testtest",
                    ShipAddress = "test",
                    ShipCity = "testtesttest"
                });
                Order order = channel.Orders().Last(x => x.OrderDate == null);
                int id = order.OrderID;

                Console.WriteLine("Was:" + ToString(order));
                channel.IsDone(id.ToString(), DateTime.Now);
                Console.WriteLine("Is:" + ToString(channel.Details(id.ToString())));
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            var channelFactory = new ChannelFactory<IOrderServiceChannel>(Channel);

            channelFactory.Credentials.UserName.UserName = UserName;
            channelFactory.Credentials.UserName.Password = Password;

            using (var channel = channelFactory.CreateChannel())
            {
                var count = channel.Orders().Count();
                channel.Delete(channel.Orders().Last().OrderID.ToString());
                var newCount = channel.Orders().Count();
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
            var channelFactory = new ChannelFactory<IOrderServiceChannel>(Channel);

            channelFactory.Credentials.UserName.UserName = UserName;
            channelFactory.Credentials.UserName.Password = Password;

            using (var channel = channelFactory.CreateChannel())
            {
                Order order = channel.Details(1.ToString());
            }
        }

        //Fault: 2.	Попытка изменения заказа в статусе «В работе» или «Выполнен».
        [TestMethod]
        public void UpdateDoneOrderTest()
        {
            var channelFactory = new ChannelFactory<IOrderServiceChannel>(Channel);

            channelFactory.Credentials.UserName.UserName = UserName;
            channelFactory.Credentials.UserName.Password = Password;

            using (var channel = channelFactory.CreateChannel())
            {
                Order order = channel.Details(10248.ToString());
                var temp = order.ShipName;
                order.ShipName = "test";
                channel.Update(order);
                Order updatedOrder = channel.Details(10248.ToString());
                if (updatedOrder.ShipName == order.ShipName)
                {
                    Console.WriteLine(ToString(order));
                }
                else
                {
                    throw new Exception("test failed");
                }
                order.ShipName = temp;
                channel.Update(order);
            }
        }

        //3.	Попытка удаления выполненного заказа
        [TestMethod]
        public void DeleteNoExistingRecordTest()
        {
            var channelFactory = new ChannelFactory<IOrderServiceChannel>(Channel);

            channelFactory.Credentials.UserName.UserName = UserName;
            channelFactory.Credentials.UserName.Password = Password;

            using (var channel = channelFactory.CreateChannel())
            {
                var count = channel.Orders().Count();
                channel.Delete(1.ToString());
                var newCount = channel.Orders().Count();
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
            var channelFactory = new ChannelFactory<IOrderServiceChannel>(Channel);

            channelFactory.Credentials.UserName.UserName = UserName;
            channelFactory.Credentials.UserName.Password = Password;

            using (var channel = channelFactory.CreateChannel())
            {
                try
                {
                    int id = 10248;
                    channel.IsInProgress(id.ToString(), DateTime.Now);
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
            var channelFactory = new ChannelFactory<IOrderServiceChannel>(Channel);

            channelFactory.Credentials.UserName.UserName = UserName;
            channelFactory.Credentials.UserName.Password = Password;

            using (var channel = channelFactory.CreateChannel())
            {
                try
                {
                    int id = 11166;
                    channel.IsDone(id.ToString(), DateTime.Now);
                }
                catch (FaultException e)
                {
                    Assert.Fail(e.Message);
                }
            }
        }

    }
}
