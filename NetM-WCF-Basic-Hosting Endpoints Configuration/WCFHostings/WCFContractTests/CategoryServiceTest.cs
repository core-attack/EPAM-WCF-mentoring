using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCFContractTests.CategoryServiceReference;

namespace WCFContractTests
{
    [TestClass]
    public class CategoryServiceTest
    {
        private const string directoryName = @"\Pictures";
        private const string testPicture = @"\Pictures\test.bmp";

        [TestMethod]
        public void CategoriesTest()
        {
            using (var client = new CategoryServiceClient("httpCategory"))
            {
                foreach (var category in client.Categories())
                {
                    Console.WriteLine(ToString(category));
                }
            }
        }

        [TestMethod]
        public void GetCategoryPictureTest()
        {
            using (var client = new CategoryServiceClient("httpCategory"))
            {
                Category[] categories = client.Categories();

                foreach (var category in categories)
                {
                    MemoryStream stream = client.GetPicture(category.CategoryID);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory() + directoryName, category.CategoryName) + ".bmp";
                    client.SaveBytesToFile(new CategoryPicture() {FileName = filePath, PictureStream = stream });
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
            using (var client = new CategoryServiceClient("httpCategory"))
            {
                Category[] categories = client.Categories();
                Random r = new Random();
                int index = r.Next(categories.Length);

                byte[] oldPicture = categories[index].Picture;
                byte[] newPicture = File.ReadAllBytes(Directory.GetCurrentDirectory() + testPicture);
                client.SetPicture(new CategoryPicture()
                {
                    CategoryId = categories[index].CategoryID,
                    PictureByteArray = newPicture
                });

                Category[] categoriesNew = client.Categories();
                
                if (!categoriesNew[index].Picture.SequenceEqual(newPicture))
                {
                    //возвращаем, что испортили
                    client.SetPicture(new CategoryPicture()
                    {
                        CategoryId = categories[index].CategoryID,
                        PictureByteArray = oldPicture
                    });

                    Assert.Fail("test failed");
                }

                //возвращаем, что испортили
                client.SetPicture(new CategoryPicture()
                {
                    CategoryId = categories[index].CategoryID,
                    PictureByteArray = oldPicture
                });

            }
        }

        public string ToString(Category c)
        {
            return string.Format("CategoryID: {0}, CategoryName: {1}, Description: {2}, Picture: {3}, Products: {4}", c.CategoryID, c.CategoryName, c.Description, c.Picture, c.Products);
        }
    }
}
 