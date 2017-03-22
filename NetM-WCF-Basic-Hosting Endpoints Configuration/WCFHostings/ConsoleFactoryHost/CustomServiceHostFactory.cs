using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace ConsoleFactoryHost
{
    public class CustomServiceHostFactory : ServiceHostFactory
    {
        public enum HttpBindingTypes
        {
            Category,
            Order,
            Subscribe
        };

        public ServiceHost CreateMyServiceHost(Type serviceType, Uri[] baseAddresses, Type serviceIType, string name, HttpBindingTypes bindingServiceTypes, 
            string httpAddress, string tcpAddress)
        {
            var host = CreateServiceHost(serviceType, baseAddresses);

            //behaviors
            var serviceMetadataBehavior = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (serviceMetadataBehavior == null)
            {
                serviceMetadataBehavior = new ServiceMetadataBehavior();
                host.Description.Behaviors.Add(serviceMetadataBehavior);
            }

            serviceMetadataBehavior.HttpGetEnabled = true;

            var serviceDebugBehavior = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            if (serviceDebugBehavior == null)
            {
                serviceDebugBehavior = new ServiceDebugBehavior();
                host.Description.Behaviors.Add(serviceDebugBehavior);
            }

            serviceDebugBehavior.IncludeExceptionDetailInFaults = true;

            //endpoints
            host.AddServiceEndpoint(serviceIType, GetBinding(bindingServiceTypes), httpAddress);
            host.AddServiceEndpoint(serviceIType, new NetTcpBinding(), tcpAddress);
            host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

            return host;
        }

        protected Binding GetBinding(HttpBindingTypes bindingServiceTypes)
        {
            //bindings 
            BasicHttpBinding httpBinding = new BasicHttpBinding();
            httpBinding.MaxBufferPoolSize = 2147483647;
            httpBinding.MaxBufferSize = 2147483647;
            httpBinding.MaxReceivedMessageSize = 2147483647;

            BasicHttpBinding streamBinding = new BasicHttpBinding();
            httpBinding.MaxBufferPoolSize = 2147483647;
            httpBinding.MaxBufferSize = 2147483647;
            httpBinding.MaxReceivedMessageSize = 2147483647;
            httpBinding.TransferMode = TransferMode.Streamed;

            switch (bindingServiceTypes)
            {
                case HttpBindingTypes.Category:
                    return streamBinding;
                case HttpBindingTypes.Order:
                    return httpBinding;
                case HttpBindingTypes.Subscribe:
                    return new WSDualHttpBinding();
            }
            return null;
        }
    }
}
