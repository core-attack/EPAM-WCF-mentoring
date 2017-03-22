using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NorthwindModel;

namespace CategoryServiceLibrary
{
    [ServiceContract(Namespace = "http://epam.com/Mentoring/WCF/CategoryService")]
    public interface ICategoryServiceLibrary
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
