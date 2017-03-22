using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFactoryHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var customServiceHostFactory = new CustomServiceHostFactory();

                var categoryServiceHost = customServiceHostFactory.CreateMyServiceHost(typeof(CategoryService.CategoryService), 
                    new [] { new Uri(@"http://localhost/CategoryService/"), new Uri(@"net.tcp://localhost/CategoryService/")  },
                    typeof(CategoryService.ICategoryService),
                    "Category",
                    CustomServiceHostFactory.HttpBindingTypes.Category, 
                    "http",
                    "tcp");
                var orderServiceHost = customServiceHostFactory.CreateMyServiceHost(typeof(OrderService.OrderService),
                    new[] { new Uri(@"http://localhost/OrderService/"), new Uri(@"net.tcp://localhost/OrderService/") },
                    typeof(OrderService.IOrderService),
                    "Order",
                    CustomServiceHostFactory.HttpBindingTypes.Order, 
                    "http",
                    "tcp");
                var subscribeServiceHost = customServiceHostFactory.CreateMyServiceHost(typeof(SubscribeService.SubscribeService),
                    new[] { new Uri(@"http://localhost/SubscribeService/"), new Uri(@"net.tcp://localhost/SubscribeService/") },
                    typeof(SubscribeService.ISubscribeService),
                    "Subscribe",
                    CustomServiceHostFactory.HttpBindingTypes.Subscribe, 
                    "http",
                    "tcp");

                OpenServiceHost(categoryServiceHost, "CategoryService");
                OpenServiceHost(orderServiceHost, "OrderService");
                OpenServiceHost(subscribeServiceHost, "SubscribeService");

                Console.WriteLine("");
                Console.WriteLine("Enter any key to stop this host.");
                Console.ReadKey();

                CloseServiceHost(categoryServiceHost, "CategoryService");
                CloseServiceHost(orderServiceHost, "OrderService");
                CloseServiceHost(subscribeServiceHost, "SubscribeService");

                Console.WriteLine("");
                Console.WriteLine("Enter any key to close.");
                Console.ReadKey();
            }
            catch (Exception e)
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
