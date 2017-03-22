using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using CategoryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindModel;
using WCFContractTests.Channels;

namespace WCFContractTests
{
    [TestClass]
    public class CategoryServiceTest
    {
        private const string directoryName = @"\Pictures";
        private const string testPicture = @"\Pictures\test.bmp";

        //private const string Channel = "ConsoleHostCategoryServiceConfiguration";
        private const string Channel = "IISCategoryServiceConfiguration";

        //private const string FaultedChannel = "WSConsoleHostCategoryServiceFaultedConfiguration";
        private const string FaultedChannel = "BasicConsoleHostCategoryServiceFaultedConfiguration";


        [TestMethod]
        public void CategoriesTest()
        {
            var channelFactory = new ChannelFactory<ICategoryServiceChannel>(Channel);

            using (var categoryServiceChannel = channelFactory.CreateChannel())
            {
                foreach (var category in categoryServiceChannel.Categories())
                {
                    Console.WriteLine(ToString(category));
                }
            }
        }

        [TestMethod]
        public void GetCategoryPictureTest()
        {
            var channelFactory = new ChannelFactory<ICategoryServiceChannel>(Channel);

            using (var categoryServiceChannel = channelFactory.CreateChannel())
            {
                var categories = categoryServiceChannel.Categories();

                foreach (var category in categories)
                {
                    MemoryStream stream = categoryServiceChannel.GetPicture(category.CategoryID.ToString());
                    string filePath = Path.Combine(Directory.GetCurrentDirectory() + directoryName, category.CategoryName) + ".bmp";
                    categoryServiceChannel.SaveBytesToFile(new CategoryPicture() {FileName = filePath, PictureStream = stream });
                    System.Diagnostics.Process.Start(filePath);
                    if (!File.Exists(filePath))
                    {
                        Assert.Fail("test failed");
                    }
                    return;
                }
                
            }
        }

        [TestMethod]
        public void SetCategoryPictureTest()
        {
            var channelFactory = new ChannelFactory<ICategoryServiceChannel>(Channel);

            using (var categoryServiceChannel = channelFactory.CreateChannel())
            {
                var categories = categoryServiceChannel.Categories();
                Random r = new Random();
                int index = r.Next(categories.Count);

                byte[] oldPicture = categories[index].Picture;
                byte[] newPicture = File.ReadAllBytes(Directory.GetCurrentDirectory() + testPicture);
                categoryServiceChannel.SetPicture(new CategoryPicture()
                {
                    CategoryId = categories[index].CategoryID,
                    PictureByteArray = newPicture
                });

                var categoriesNew = categoryServiceChannel.Categories();
                
                if (!categoriesNew[index].Picture.SequenceEqual(newPicture))
                {
                    //возвращаем, что испортили
                    categoryServiceChannel.SetPicture(new CategoryPicture()
                    {
                        CategoryId = categories[index].CategoryID,
                        PictureByteArray = oldPicture
                    });

                    Assert.Fail("test failed");
                }

                //возвращаем, что испортили
                categoryServiceChannel.SetPicture(new CategoryPicture()
                {
                    CategoryId = categories[index].CategoryID,
                    PictureByteArray = oldPicture
                });

            }
        }

        [TestMethod]
        public void CallFaultedMethod()
        {
            var channelFactory = new ChannelFactory<ICategoryServiceChannel>(FaultedChannel);

            using (var categoryServiceChannel = channelFactory.CreateChannel())
            {
                categoryServiceChannel.Categories();
                categoryServiceChannel.GetPicture("1");
            }
        }

        [TestMethod]
        public void CallFaultedMethodWithChannelState()
        {
            var channelFactory = new ChannelFactory<ICategoryServiceChannel>(FaultedChannel);

            using (var categoryServiceChannel = channelFactory.CreateChannel())
            {
                WriteState(categoryServiceChannel);
                categoryServiceChannel.Categories();
                WriteState(categoryServiceChannel);

                WriteState(categoryServiceChannel);
                categoryServiceChannel.GetPicture("1");
                WriteState(categoryServiceChannel);
            }
        }

        [TestMethod]
        public void CallFaultedMethodWithChannelStateWithRecreateChannel()
        {
            var channelFactory = new ChannelFactory<ICategoryServiceChannel>(FaultedChannel);
            var categoryServiceChannel = channelFactory.CreateChannel();

            try
            {
                WriteState(categoryServiceChannel);
                categoryServiceChannel.GetPicture("1");
                WriteState(categoryServiceChannel);
            }
            catch (CommunicationException)
            {
                WriteState(categoryServiceChannel);
                categoryServiceChannel.Abort();

                categoryServiceChannel = channelFactory.CreateChannel();
            }

            WriteState(categoryServiceChannel);
            categoryServiceChannel.Categories();
            WriteState(categoryServiceChannel);
        }

        public string ToString(Category c)
        {
            return string.Format("CategoryID: {0}, CategoryName: {1}, Description: {2}, Picture: {3}, Products: {4}", c.CategoryID, c.CategoryName, c.Description, c.Picture, c.Products);
        }

        protected void WriteState(IClientChannel channel)
        {
            Console.WriteLine("Channel state: {0}", channel.State);
        }
    }
}
 