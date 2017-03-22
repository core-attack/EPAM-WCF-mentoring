using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using NorthwindModel;

namespace CategoryService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(CategoryService)))
            {
                host.Open();

                Console.WriteLine("CategoryService was started.");
                Console.ReadKey();

                host.Close();

                Console.WriteLine("CategoryService was stopped.");
            }
        }
    }
}
