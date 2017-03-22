using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using CategoryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderService;
using Web = System.Web.Services;

namespace WCFContractTests
{
    [TestClass]
    public class MetadataTest
    {
        [TestMethod]
        public void ExportMetadataFromAssembly()
        {
            const string assemblyFile = "Contracts.dll";
            string[] serviceContractTypeNames = new string[] { "CategoryService.ICategoryService", "OrderService.IOrderService" };

            foreach (var serviceContractTypeName in serviceContractTypeNames)
            {
                Assembly assembly = Assembly.LoadFile(System.IO.Path.GetFullPath(assemblyFile));
                Type serviceContractType = assembly.GetType(serviceContractTypeName);

                ContractDescription cd = ContractDescription.GetContract(serviceContractType);

                WsdlExporter exporter = new WsdlExporter();
                exporter.ExportContract(cd);

                foreach (Web.Description.ServiceDescription sd in exporter.GeneratedWsdlDocuments)
                {
                    var fileName = new Uri(sd.TargetNamespace).GetComponents(UriComponents.Host, UriFormat.Unescaped) + ".wsdl";
                    sd.Write(fileName);
                }

                foreach (XmlSchema schema in exporter.GeneratedXmlSchemas.Schemas())
                {
                    var fileName = new Uri(schema.TargetNamespace).GetComponents(UriComponents.Path, UriFormat.SafeUnescaped).Replace('/', '.') + ".xsd";
                    schema.Write(new System.IO.FileStream(fileName, System.IO.FileMode.Create));
                }
            }
        }

        [TestMethod]
        public void ConfigureServiceClientFromMetadata()
        {
            var metadataAddress = "http://localhost:59218/order.svc?wsdl";

            var metadataClient = new MetadataExchangeClient(new Uri(metadataAddress), MetadataExchangeClientMode.HttpGet);
            var wsdlImporter = new WsdlImporter(metadataClient.GetMetadata());
            var endpoints = wsdlImporter.ImportAllEndpoints();

            foreach (var endpoint in endpoints)
            {
                Console.WriteLine("{0}, {1}, {2}", endpoint.Binding.GetType().Name, endpoint.Address, endpoint.Contract.Namespace);

                BasicHttpBinding httpBinding = new BasicHttpBinding();
                httpBinding.MaxBufferPoolSize = 2147483647;
                httpBinding.MaxBufferSize = 2147483647;
                httpBinding.MaxReceivedMessageSize = 2147483647;

                var channelFactory = new ChannelFactory<IOrderService>(httpBinding, endpoint.Address);
                var orderService = channelFactory.CreateChannel();
                try
                {
                    var cats = orderService.Orders();

                    foreach (var c in cats)
                    {
                        Console.WriteLine(c.ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        [TestMethod]
        public void ConfigureServiceClientFromMetadataConsoleHost()
        {
            string mexAddress = "http://localhost/OrderService/mex";

            var metadataClient = new MetadataExchangeClient(new Uri(mexAddress), MetadataExchangeClientMode.MetadataExchange);
            var wsdlImporter = new WsdlImporter(metadataClient.GetMetadata());
            var endpoints = wsdlImporter.ImportAllEndpoints();

            foreach (var endpoint in endpoints)
            {
                Console.WriteLine("{0}, {1}, {2}", endpoint.Binding.GetType().Name, endpoint.Address, endpoint.Contract.Namespace);

                BasicHttpBinding httpBinding = new BasicHttpBinding();
                httpBinding.MaxBufferPoolSize = 2147483647;
                httpBinding.MaxBufferSize = 2147483647;
                httpBinding.MaxReceivedMessageSize = 2147483647;

                var channelFactory = new ChannelFactory<IOrderService>(httpBinding, endpoint.Address);
                var orderService = channelFactory.CreateChannel();
                try
                {
                    var cats = orderService.Orders();

                    foreach (var c in cats)
                    {
                        Console.WriteLine(c.ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
