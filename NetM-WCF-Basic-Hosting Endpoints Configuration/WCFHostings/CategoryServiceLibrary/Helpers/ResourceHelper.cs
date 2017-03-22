using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryServiceLibrary.Helpers
{
    class ResourceHelper
    {
        public static string GetResource(string key)
        {
            var value = Properties.Resources.ResourceManager.GetString(key);
            if (value != null)
            {
                return value;
            }
            return "";
        }
    }
}
