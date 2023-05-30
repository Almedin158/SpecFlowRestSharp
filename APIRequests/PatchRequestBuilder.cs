using Newtonsoft.Json;
using RestSharp;
using SpecFlowRestSharp.Configuration;

namespace SpecFlowRestSharp.APIRequests
{
    //Check the AbstractRequest class for method explainations
    public class PatchRequestBuilder : AbstractRequest
    {
        RestRequest _restRequest;

        public PatchRequestBuilder()
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Patch
            };
        }

        public override RestRequest Build()
        {
            return _restRequest;
        }
        public PatchRequestBuilder WithUrl(string url)
        {
            WithUrl(url, _restRequest);
            return this;
        }

        public PatchRequestBuilder WithHeaders(Dictionary<string, string> headers)
        {
            WithHeaders(headers, _restRequest);
            return this;
        }

        public PatchRequestBuilder WithBody(string body)
        {
            WithJsonBody(body, _restRequest);
            return this;
        }

        public PatchRequestBuilder WithBody(object body)
        {
            var stringBody = JsonConvert.SerializeObject(body);
            WithJsonBody(stringBody, _restRequest);
            return this;
        }

        public PatchRequestBuilder WithParameters(Dictionary<string, string> wwwForm)
        {
            WithParameters(wwwForm, _restRequest);
            return this;
        }

        public PatchRequestBuilder WithUrlSegments(Dictionary<string, string> urlSegments)
        {
            WithUrlSegments(urlSegments, _restRequest);
            return this;
        }
    }
}