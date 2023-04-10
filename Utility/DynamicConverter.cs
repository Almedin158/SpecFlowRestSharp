using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SpecFlowRestSharp.Utility
{
    public static class DynamicConverter
    {
        public static dynamic ConvertToJObject(string content)
        {
            return JsonConvert.DeserializeObject<JObject>(content);
        }
        public static dynamic ConvertToJArray(string content)
        {
            return JsonConvert.DeserializeObject<JArray>(content);
        }
    }
}
