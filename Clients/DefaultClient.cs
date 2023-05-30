using RestSharp;
using SpecFlowRestSharp.Configuration;

namespace SpecFlowRestSharp.Clients
{
    public class DefaultClient : IClient
    {
        private RestClient _restClient;
        private readonly RestClientOptions _restClientOptions;

        /// <summary>
        /// Constructor, you can use it to add custom options to the RestClient
        /// </summary>
        public DefaultClient()
        {
            _restClientOptions = new RestClientOptions();
        }

        /// <summary>
        /// Disposes RestClient connection
        /// </summary>
        public void Dispose()
        {
            if( _restClient != null ) { 
                _restClient.Dispose();
            }
        }

        /// <summary>
        /// Applies the custom options to the client and returns it
        /// </summary>
        /// <returns></returns>
        public RestClient GetClient()
        {
            _restClientOptions.ThrowOnDeserializationError = true;
            _restClient = new RestClient(_restClientOptions);
            return _restClient;
        }
    }
}