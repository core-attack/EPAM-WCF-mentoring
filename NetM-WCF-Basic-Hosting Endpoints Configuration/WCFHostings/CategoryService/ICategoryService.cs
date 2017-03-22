using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using NorthwindModel;

namespace CategoryService
{
    [ServiceContract(Namespace = "http://epam.com/Mentoring/WCF/CategoryService")]
    public interface ICategoryService
    {
        [OperationContract]
        List<Category> Categories();

        [OperationContract]
        MemoryStream GetPicture(int id);

        [OperationContract]
        void SetPicture(CategoryPicture cp);

        [OperationContract]
        void SaveBytesToFile(CategoryPicture cp);
    }
}
