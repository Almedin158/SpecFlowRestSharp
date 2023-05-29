using RestSharp;

namespace SpecFlowRestSharp.Utility
{
    public static class CurlConverter
    {
        /// <summary>
        /// This should work with json, url encoded and form-data
        /// To clear things up, this function generates a curl that you can use to replicate the request in case of issues, meaning you can reproduce it and debug the exact reason the request failed.
        /// Important, this should properly function when pasting the curl in Postman for example, but it might not work when pasting inside of the Command Prompt, why?
        /// Well, because Postman adds required headers by default when performing the request, Command Prompt does not.
        /// You can always add the required headers via the "WithHeaders" function, in those cases, should perform well using either Postman, Command Prompt or other tools.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string ConvertToCurl(RestRequest request)
        {
            var client = new RestClient(request.Resource);
            var method = request.Method.ToString();
            var url = client.BuildUri(request).ToString();
            var curl = $"curl -X {method.ToUpper()} \"{url}\"";

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
                    else if (bodyType == "multipart/form-data")
                    {
                        var obj = DynamicConverter.ConvertToJObject(param.Value.ToString());

                        foreach (var value in obj)
                        {
                            curl += $" -F '{value.Name}={value.Value}'";
                        }
                    }

                }
                else if (param.Type == ParameterType.HttpHeader)
                {
                    curl += $" -H '{param.Name}:{param.Value}'";
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
                else if(param.Value == "multipart/form-data")
                {
                    return "multipart/form-data";
                }
            }

            return "";
        }
    }
}