using Newtonsoft.Json;
using RestSharp;
using SpecFlowRestSharp.Configuration;

namespace SpecFlowRestSharp.APIRequests
{
    //Check the AbstractRequest class for method explainations
    public class PostRequestBuilder : AbstractRequest
    {
        RestRequest _restRequest;

        public PostRequestBuilder()
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Post
            };
        }

        public override RestRequest Build()
        {
            return _restRequest;
        }
        public PostRequestBuilder WithUrl(string url)
        {
            WithUrl(url, _restRequest);
            return this;
        }

        public PostRequestBuilder WithEndpoint(string endpoint)
        {
            WithEndpoint(endpoint, _restRequest);
            return this;
        }

        public PostRequestBuilder WithHeaders(Dictionary<string, string> headers)
        {
            WithHeaders(headers, _restRequest);
            return this;
        }

        public PostRequestBuilder WithBody(string body)
        {
            WithJsonBody(body, _restRequest);
            return this;
        }

        public PostRequestBuilder WithBody(object body)
        {
            var stringBody = JsonConvert.SerializeObject(body);
            WithJsonBody(stringBody, _restRequest);
            return this;
        }


        public PostRequestBuilder WithParameters(Dictionary<string, string> wwwForm)
        {
            WithParameters(wwwForm, _restRequest);
            return this;
        }

        public PostRequestBuilder WithUrlSegments(Dictionary<string, string> urlSegments)
        {
            WithUrlSegments(urlSegments, _restRequest);
            return this;
        }
    }
}