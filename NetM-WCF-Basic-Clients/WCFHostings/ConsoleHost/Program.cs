using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceModel.Web;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
                var categoryServiceHost = new WebServiceHost(typeof(Implementations.CategoryService));
                var categoryServiceFaultedHost = new WebServiceHost(typeof(Implementations.CategoryServiceFaulted));
                var orderServiceHost = new WebServiceHost(typeof(Implementations.OrderService));
                var subscribeServiceHost = new WebServiceHost(typeof(SubscribeService.SubscribeService));

                OpenServiceHost(categoryServiceHost, "CategoryService");
                OpenServiceHost(categoryServiceFaultedHost, "CategoryServiceFaulted");
                OpenServiceHost(orderServiceHost, "OrderService");
                OpenServiceHost(subscribeServiceHost, "SubscribeService");

                Console.WriteLine("");
                Console.WriteLine("Enter any key to stop this host.");
                Console.ReadKey();

                CloseServiceHost(categoryServiceHost, "CategoryService");
                CloseServiceHost(categoryServiceHost, "CategoryServiceFaulted");
                CloseServiceHost(orderServiceHost, "OrderService");
                CloseServiceHost(subscribeServiceHost, "SubscribeService");

                Console.WriteLine("");
                Console.WriteLine("Enter any key to close.");
                Console.ReadKey();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.ReadKey();
            }
        }

        static void OpenServiceHost(ServiceHost serviceHost, string name)
        {
            IAsyncResult result = serviceHost.BeginOpen(AsyncStarted, name + " is opening...");
            serviceHost.EndOpen(result);
        }

        static void CloseServiceHost(ServiceHost serviceHost, string name)
        {
            IAsyncResult result = serviceHost.BeginClose(AsyncClosed, name + " is closing...");
            serviceHost.EndClose(result);
        }

        static void AsyncStarted(IAsyncResult resObj)
        {
            string mes = (string)resObj.AsyncState;
            Console.WriteLine(mes);
            Console.WriteLine("Service was started.");
        }

        static void AsyncClosed(IAsyncResult resObj)
        {
            string mes = (string)resObj.AsyncState;
            Console.WriteLine(mes);
            Console.WriteLine("Service was stopped.");
        }
    }
}
