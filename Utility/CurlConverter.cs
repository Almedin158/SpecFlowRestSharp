using RestSharp;

namespace SpecFlowRestSharp.Utility
{
    public static class CurlConverter
    {
        //Should work with json, url encoded and form-data
        public static string ConvertToCurl(RestRequest request)
        {
            var client = new RestClient(request.Resource);
            var method = request.Method.ToString();
            var url = client.BuildUri(request).ToString();
            var curl = $"curl -X {method} \"{url}\"";

            var bodyType = GetBodyType(request);

            foreach (var param in request.Parameters)
            {
                if (param.Type == ParameterType.RequestBody)
                {
                    if (bodyType == "application/json")
                    {
                        curl += $" -d '{param.Value}'";
                    }
                    else if(bodyType== "application/x-www-form-urlencoded")
                    {
                        var obj = DynamicConverter.ConvertToJObject(param.Value.ToString());

                        foreach (var value in obj)
                        {
                            curl += $" -d '{value.Name}={value.Value}'";
                        }
                    }

                }
                else if (param.Type == ParameterType.HttpHeader)
                {
                    curl += $" -H '{param.Name}: {param.Value}'";
                }
            }

            return curl;
        }

        public static string GetBodyType(RestRequest request)
        {
            foreach (var param in request.Parameters)
            {
                if (param.Value == "application/x-www-form-urlencoded")
                {
                    return "application/x-www-form-urlencoded";
                }
                else if (param.Value == "application/json")
                {
                    return "application/json";
                }
            }

            return "";
        }
    }
}