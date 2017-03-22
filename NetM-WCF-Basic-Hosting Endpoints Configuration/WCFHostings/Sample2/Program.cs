using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Sample2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(SimpleCalculatorService.CalculatorService)))
            {               
                host.Open();

                Console.WriteLine("Service started");
                Console.ReadLine();

                host.Close();

                Console.WriteLine("Service stopped");
            }
        }
    }
}
