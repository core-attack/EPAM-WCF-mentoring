namespace ConsoleHost
{
    using System;
    using System.ServiceModel;

    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(CalculatorService.CalculatorService)))
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
