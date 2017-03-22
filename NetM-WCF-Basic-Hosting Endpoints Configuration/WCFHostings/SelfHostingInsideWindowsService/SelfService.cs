using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostingInsideWindowsService
{
    public partial class SelfService : ServiceBase
    {
        private ServiceHost CategoryServiceHost;
        private ServiceHost OrderServiceHost;
        private ServiceHost SubscribeServiceHost;

        public SelfService()
        {
            InitializeComponent();
            CategoryServiceHost = new ServiceHost(typeof(CategoryService.CategoryService));
            OrderServiceHost = new ServiceHost(typeof(OrderService.OrderService));
            SubscribeServiceHost = new ServiceHost(typeof(SubscribeService.SubscribeService));
        }

        protected override void OnStart(string[] args)
        {
            OpenServiceHost(CategoryServiceHost, "CategoryService");
            OpenServiceHost(OrderServiceHost, "OrderService");
            OpenServiceHost(SubscribeServiceHost, "SubscribeService");
        }

        protected override void OnStop()
        {
            CloseServiceHost(CategoryServiceHost, "CategoryService");
            CloseServiceHost(OrderServiceHost, "OrderService");
            CloseServiceHost(SubscribeServiceHost, "SubscribeService");
        }

        private void OpenServiceHost(ServiceHost serviceHost, string name)
        {
            IAsyncResult result = serviceHost.BeginOpen(AsyncStarted, name + " is opening...");
            serviceHost.EndOpen(result);
        }

        private void CloseServiceHost(ServiceHost serviceHost, string name)
        {
            IAsyncResult result = serviceHost.BeginClose(AsyncClosed, name + " is closing...");
            serviceHost.EndClose(result);
        }

        private void AsyncStarted(IAsyncResult resObj)
        {
            string mes = (string)resObj.AsyncState;
            Console.WriteLine(mes);
            Console.WriteLine("Service was started.");
        }

        private void AsyncClosed(IAsyncResult resObj)
        {
            string mes = (string)resObj.AsyncState;
            Console.WriteLine(mes);
            Console.WriteLine("Service was stopped.");
        }
    }
}
