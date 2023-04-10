using RestSharp;

namespace SpecFlowRestSharp.Utility
{
    public static class CurlConverter
    {
        public static string ConvertToCurl(RestRequest request)
        {
            var client = new RestClient(request.Resource);
            var method = request.Method.ToString();
            var url = client.BuildUri(request).ToString();
            var curl = $"curl -X {method} \"{url}\"";

            foreach (var param in request.Parameters)
            {
                if (param.Type == ParameterType.RequestBody)
                {
                    curl += $" -d '{param.Value}'";
                }
                else if (param.Type == ParameterType.HttpHeader)
                {
                    curl += $" -H '{param.Name}: {param.Value}'";
                }
            }

            return curl;
        }
    }
}
