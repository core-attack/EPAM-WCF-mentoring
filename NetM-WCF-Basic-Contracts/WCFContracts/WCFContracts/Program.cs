using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using NorthwindModel;
using OrderService.SubscribeServiceReference;

namespace OrderService
{
    class Program : SubscribeServiceReference.ISubscribeServiceCallback
    {
        private SubscribeServiceClient client;
        static void Main(string[] args)
        {
            Program p = new Program();
            //p.SubscribeTest();

            using (var host = new ServiceHost(typeof(OrderService)))
            {
                host.Open();

                Console.WriteLine("OrderService was started.");
                Console.ReadKey();

                host.Close();

                Console.WriteLine("OrderService was stopped.");
            }
        }

        public void SubscribeTest()
        {
            var context = new InstanceContext(this);

            client = new SubscribeServiceClient(context);

            try
            {

                var orderClient = new OrderService();
                var ows = new OrderWithStatus() { OrderId = 11166, Status = Statuses.InProgress };

                client.Subscribe(ows);

                orderClient.SetStatus(ows, DateTime.Now);

                client.Unsubscribe(ows);

                ows.Status = Statuses.Done;

                orderClient.SetStatus(ows, DateTime.Now);

            }
            catch (FaultException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void StatusWasChanged(OrderWithStatus ows)
        {
            Console.WriteLine("Status by Order #{0} was chenged from {2} to {1}", ows.OrderId, ows.Status, ows.OldStatus);
        }
    }
}
