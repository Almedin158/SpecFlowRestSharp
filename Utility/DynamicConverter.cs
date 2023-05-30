using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SpecFlowRestSharp.Utility
{
    public static class DynamicConverter
    {
        /// <summary>
        /// Convert the response content to a dynamic object
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static dynamic ConvertToJObject(string content)
        {
            return JsonConvert.DeserializeObject<JObject>(content);
        }
        /// <summary>
        /// Converts the response content to a dynamic array
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static dynamic ConvertToJArray(string content)
        {
            return JsonConvert.DeserializeObject<JArray>(content);
        }
    }
}