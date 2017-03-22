using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using Implementations.Helpers;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //rest работать не будет, потому что убрал WebServiceHost
                var categoryServiceHost = new ServiceHost(typeof(Implementations.CategoryService));
                var categoryServiceFaultedHost = new ServiceHost(typeof(Implementations.CategoryServiceFaulted));
                var orderServiceHost = new ServiceHost(typeof(Implementations.OrderService));
                var subscribeServiceHost = new ServiceHost(typeof(SubscribeService.SubscribeService));
                var roleServiceHost = new ServiceHost(typeof(Implementations.RoleService));

                //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3;

                OpenServiceHost(categoryServiceHost, "CategoryService");
                OpenServiceHost(categoryServiceFaultedHost, "CategoryServiceFaulted");
                OpenServiceHost(orderServiceHost, "OrderService");
                OpenServiceHost(subscribeServiceHost, "SubscribeService");
                OpenServiceHost(roleServiceHost, "RoleService");

                Console.WriteLine("");
                Console.WriteLine("Enter any key to stop this host.");
                Console.ReadKey();

                CloseServiceHost(categoryServiceHost, "CategoryService");
                CloseServiceHost(categoryServiceFaultedHost, "CategoryServiceFaulted");
                CloseServiceHost(orderServiceHost, "OrderService");
                CloseServiceHost(subscribeServiceHost, "SubscribeService");
                CloseServiceHost(roleServiceHost, "RoleService");

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
