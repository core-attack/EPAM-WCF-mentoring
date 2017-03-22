using System;
namespace OrderService.Helpers
{
    class ResourseHelper
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
