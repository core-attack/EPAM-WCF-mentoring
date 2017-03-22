using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using CategoryService.Helpers;
using NorthwindModel;

namespace CategoryService
{
    public class CategoryService : ICategoryService
    {
        private NorthwindContext db = new NorthwindContext();

        public List<Category> Categories()
        {
            return db.Categories.ToList();
        }

        public MemoryStream GetPicture(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                throw new Exception(ResourceHelper.GetResource("NotExist"));
            }

            return new MemoryStream(category.Picture);
        }

        public void SetPicture(CategoryPicture cp)
        {
            Category category = db.Categories.Find(cp.CategoryId);
            if (category == null)
            {
                throw new Exception(ResourceHelper.GetResource("NotExist"));
            }
            category.Picture = ReadFully(new MemoryStream(cp.PictureByteArray));
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void SaveBytesToFile(CategoryPicture cp)
        {
            if (!string.IsNullOrEmpty(cp.FileName) && cp.PictureStream != null)
            {
                const int offset = 78;
                var dir = Path.GetDirectoryName(cp.FileName);
                if (dir != null)
                {
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    FileStream file = File.Create(cp.FileName);

                    file.Write(cp.PictureStream.ToArray(), offset, cp.PictureStream.ToArray().Length - offset);

                    file.Close();
                }
                else
                {
                    throw new Exception("Директория не существует!");
                }
            }
        }

        public byte[] ReadFully(MemoryStream stream)
        {
            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }
    }
}
