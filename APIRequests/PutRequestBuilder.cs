﻿using Newtonsoft.Json;
using RestSharp;
using SpecFlowRestSharp.Configuration;

namespace SpecFlowRestSharp.APIRequests
{
    //Check the AbstractRequest class for method explainations
    public class PutRequestBuilder : AbstractRequest
    {
        RestRequest _restRequest;

        public PutRequestBuilder()
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Put
            };
        }

        public override RestRequest Build()
        {
            return _restRequest;
        }
        public PutRequestBuilder WithUrl(string url)
        {
            WithUrl(url, _restRequest);
            return this;
        }

        public PutRequestBuilder WithEndpoint(string endpoint)
        {
            WithEndpoint(endpoint, _restRequest);
            return this;
        }

        public PutRequestBuilder WithHeaders(Dictionary<string, string> headers)
        {
            WithHeaders(headers, _restRequest);
            return this;
        }

        public PutRequestBuilder WithBody(string body)
        {
            WithJsonBody(body, _restRequest);
            return this;
        }

        public PutRequestBuilder WithBody(object body)
        {
            var stringBody = JsonConvert.SerializeObject(body);
            WithJsonBody(stringBody, _restRequest);
            return this;
        }

        public PutRequestBuilder WithParameters(Dictionary<string, string> wwwForm)
        {
            WithParameters(wwwForm, _restRequest);
            return this;
        }

        public PutRequestBuilder WithUrlSegments(Dictionary<string, string> urlSegments)
        {
            WithUrlSegments(urlSegments, _restRequest);
            return this;
        }
    }
}