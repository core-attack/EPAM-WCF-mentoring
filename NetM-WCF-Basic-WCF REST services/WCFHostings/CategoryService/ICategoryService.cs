using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using NorthwindModel;

namespace CategoryService
{
    [ServiceContract(Namespace = "http://epam.com/Mentoring/WCF/CategoryService")]
    public interface ICategoryService
    {
        [WebGet(UriTemplate = "/categories")]
        [OperationContract]
        List<Category> Categories();

        [WebGet(UriTemplate = "/categories/{id}")]
        [OperationContract]
        MemoryStream GetPicture(string id);

        [WebInvoke(UriTemplate = "/categories/setpicture/", Method = "PUT")]
        [OperationContract]
        void SetPicture(CategoryPicture cp);

        [WebInvoke(UriTemplate = "/categories/download/", Method = "POST")]
        [OperationContract]
        void SaveBytesToFile(CategoryPicture cp);
    }
}
