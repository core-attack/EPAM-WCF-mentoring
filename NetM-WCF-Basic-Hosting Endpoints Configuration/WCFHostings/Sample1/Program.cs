using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(SimpleCalculatorService.CalculatorService)))
            {
                var contract = ContractDescription.GetContract(typeof(SimpleCalculatorService.ISimpleCalculator));
                var binding = new BasicHttpBinding();
                var address = new EndpointAddress("http://localhost:8733/Design_Time_Addresses/SimpleCalculatorService/Sample1/");

                var endpoint = new ServiceEndpoint(contract, binding, address);

                host.AddServiceEndpoint(endpoint);

                host.Open();

                Console.WriteLine("Service started");
                Console.ReadLine();

                host.Close();

                Console.WriteLine("Service stopped");
            }
        }
    }
}
