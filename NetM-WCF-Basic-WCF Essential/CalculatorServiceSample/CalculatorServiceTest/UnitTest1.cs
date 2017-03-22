namespace CalculatorServiceTest
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckAdd()
        {
            using (var client = new ServiceReference.CalculatorServiceClient("soap"))
            {
                Console.WriteLine(client.Add(1, 5));
            }

            using (var client = new ServiceReference.CalculatorServiceClient("tcp"))
            {
                Console.WriteLine(client.Add(1, 5));
            }

        }

        [TestMethod]
        public void CheckDiff()
        {
            using (var client = new ServiceReference.CalculatorServiceClient("soap"))
            {
                Console.WriteLine(client.Diff(132, 5));
            }

            using (var client = new ServiceReference.CalculatorServiceClient("tcp"))
            {
                Console.WriteLine(client.Diff(132, 5));
            }
        }

        [TestMethod]
        public void CheckDiv()
        {
            using (var client = new ServiceReference.CalculatorServiceClient("soap"))
            {
                Console.WriteLine(client.Div(153, 5));
            }

            using (var client = new ServiceReference.CalculatorServiceClient("tcp"))
            {
                Console.WriteLine(client.Div(153, 5));
            }
        }

        [TestMethod]
        public void CheckMult()
        {
            using (var client = new ServiceReference.CalculatorServiceClient("soap"))
            {
                Console.WriteLine(client.Mult(17, 5));
            }

            using (var client = new ServiceReference.CalculatorServiceClient("tcp"))
            {
                Console.WriteLine(client.Mult(17, 5));
            }
        }

        [TestMethod]
        public void CheckPi()
        {
            using (var client = new ServiceReference.CalculatorServiceClient("soap"))
            {
                Console.WriteLine(client.PI());
            }

            using (var client = new ServiceReference.CalculatorServiceClient("tcp"))
            {
                Console.WriteLine(client.PI());
            }
        }
    }
}
