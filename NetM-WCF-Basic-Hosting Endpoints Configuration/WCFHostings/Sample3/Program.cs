using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Sample3
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = new Uri("http://localhost:8733/Design_Time_Addresses/SimpleCalculatorService/Sample3/");

            using (var host = new ServiceHost(typeof(SimpleCalculatorService.CalculatorService), baseAddress))
            {
                host.AddServiceEndpoint(typeof(SimpleCalculatorService.ISimpleCalculator), new BasicHttpBinding(), "");
                host.Description.Behaviors.Add(new ServiceMetadataBehavior() { HttpGetEnabled = true });

                host.Open();

                Console.WriteLine("Service started");
                Console.ReadLine();

                host.Close();

                Console.WriteLine("Service stopped");
            }
        }
    }
}
