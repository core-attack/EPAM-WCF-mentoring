using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using NorthwindModel;

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
