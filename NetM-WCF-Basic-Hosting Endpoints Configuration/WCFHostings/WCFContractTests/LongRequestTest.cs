using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCFContractTests.CategoryServiceReference;
using WCFContractTests.OrderServiceReference;
using IISCatRef = WCFContractTests.IISCategoryServiceReference;
using IISOrderRef = WCFContractTests.IISOrderServiceReference;

namespace WCFContractTests
{
    [TestClass]
    public class LongRequestTest
    {
        private const string directoryName = @"\Logs";

        [TestMethod]
        public void CategoryServiceLongRequestTest()
        {
            using (var client = new CategoryServiceClient("httpCategory"))
            {
                System.Threading.Thread.Sleep(10000);

                if (!File.Exists(Directory.GetCurrentDirectory() + directoryName))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + directoryName);
                }

                var fileName = DateTime.Now.ToLocalTime().ToString("u").Replace(":", " ");
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory() + directoryName, fileName) + ".txt", "CategoryServiceLongRequestTest");
            }
            
        }

        [TestMethod]
        public void OrderServiceLongRequestTest()
        {
            using (var client = new OrderServiceClient("httpOrder"))
            {
                System.Threading.Thread.Sleep(10000);

                if (!File.Exists(Directory.GetCurrentDirectory() + directoryName))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + directoryName);
                }

                var fileName = DateTime.Now.ToLocalTime().ToString("u").Replace(":", "-");
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory() + directoryName, fileName) + ".txt",
                    "OrderServiceLongRequestTest");
            }
        }

        [TestMethod]
        public void IisCategoryServiceLongRequestTest()
        {
            using (var client = new IISCatRef.CategoryServiceLibraryClient())
            {
                System.Threading.Thread.Sleep(10000);

                if (!File.Exists(Directory.GetCurrentDirectory() + directoryName))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + directoryName);
                }

                var fileName = DateTime.Now.ToLocalTime().ToString("u").Replace(":", " ");
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory() + directoryName, fileName) + ".txt", "CategoryServiceLongRequestTest");
            }

        }

        [TestMethod]
        public void IisOrderServiceLongRequestTest()
        {
            using (var client = new IISOrderRef.OrderServiceClient())
            {
                System.Threading.Thread.Sleep(10000);

                if (!File.Exists(Directory.GetCurrentDirectory() + directoryName))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + directoryName);
                }

                var fileName = DateTime.Now.ToLocalTime().ToString("u").Replace(":", "-");
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory() + directoryName, fileName) + ".txt",
                    "OrderServiceLongRequestTest");
            }
        }
    }
}
