using RestSharp;

namespace SpecFlowRestSharp.Configuration
{
    public abstract class AbstractRequest
    {
        public abstract RestRequest Build();

        /// <summary>
        /// Adds an url to the RestRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="restRequest"></param>
        protected virtual void WithUrl(string url, RestRequest restRequest)
        {
            restRequest.Resource = url;
        }

        /// <summary>
        /// Adds an endpoint to the RestRequest
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="restRequest"></param>
        protected virtual void WithEndpoint(string endpoint, RestRequest restRequest)
        {
            restRequest.Resource += endpoint;
        }

        /// <summary>
        /// Adds or updates the request header. RestSharp will try to separate request and content headers when calling the resource.
        /// Existing header with the same name will be replaced.
        /// </summary>
        /// <param name="header"></param>
        /// <param name="restRequest"></param>
        protected virtual void WithHeaders(Dictionary<string, string> header, RestRequest restRequest)
        {
            foreach (var key in header.Keys)
            {
                restRequest.AddOrUpdateHeader(key, header[key]);
            }
        }

        /// <summary>
        /// Accepts a string json and applies it to the RestRequest body
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <param name="restRequest"></param>
        protected virtual void WithJsonBody(string jsonBody, RestRequest restRequest)
        {
            restRequest.AddJsonBody(jsonBody);
        }

        /// <summary>
        /// Adds a query string parameter to the request. The request resource should not contain any placeholders for this parameter.
        /// The parameter will be added to the request URL as a query string using name=value format.
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="restRequest"></param>
        protected virtual void WithQueryParameters(Dictionary<string, string> queryParameters, RestRequest restRequest)
        {
            foreach (var key in queryParameters.Keys)
            {
                restRequest.AddQueryParameter(key, queryParameters[key]);
            }
        }

        /// <summary>
        /// Adds a URL segment parameter to the request. The resource URL must have a placeholder for the parameter for it to work.
        /// For example, if you add a URL segment parameter with the name "id", the resource URL should contain {id} in its path.
        /// </summary>
        /// <param name="urlSegments"></param>
        /// <param name="restRequest"></param>
        protected virtual void WithUrlSegments(Dictionary<string, string> urlSegments, RestRequest restRequest)
        {
            foreach (var key in urlSegments.Keys)
            {
                restRequest.AddUrlSegment(key, urlSegments[key]);
            }
        }

        /// <summary>
        /// Adds a HTTP parameter to the request (QueryString for GET, DELETE, OPTIONS and HEAD; Encoded form for POST and PUT)
        /// </summary>
        /// <param name="urlSegments"></param>
        /// <param name="restRequest"></param>
        protected virtual void WithParameters(Dictionary<string, string> urlSegments, RestRequest restRequest)
        {
            foreach (var key in urlSegments.Keys)
            {
                restRequest.AddParameter(key, urlSegments[key]);
            }
        }
    }
}