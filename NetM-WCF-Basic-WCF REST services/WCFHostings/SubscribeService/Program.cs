using System;
using System.ServiceModel;

namespace SubscribeService
{
    class Program 
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(SubscribeService)))
            {
                host.Open();

                Console.WriteLine("SubscribeService was started.");
                Console.ReadKey();

                host.Close();

                Console.WriteLine("SubscribeService was stopped.");
            }
        }
    }
}
