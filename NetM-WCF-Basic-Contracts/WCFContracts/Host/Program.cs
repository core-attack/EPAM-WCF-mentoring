using System;
using System.ServiceModel;
using Host.OrderServiceReference;
using Host.SubscribeServiceReference;
using NorthwindModel;

namespace Host
{
    class Program : ISubscribeServiceCallback
    {
        private SubscribeServiceClient client;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.SubscribeTest();

            Console.WriteLine("Please wait several seconds. If nothing will show rebuild this project.");
            Console.ReadKey();
        }

        public void SubscribeTest()
        {
            var context = new InstanceContext(this);

            client = new SubscribeServiceClient(context);

            try
            {
                using (var orderClient = new OrderServiceClient())
                {
                    var ows = new OrderWithStatus() { OrderId = 11166, Status = Statuses.InProgress };

                    client.Subscribe(ows);

                    orderClient.SetStatus(ows, DateTime.Now);

                    client.Unsubscribe(ows);

                    ows.Status = Statuses.Done;

                    orderClient.SetStatus(ows, DateTime.Now);
                }

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
