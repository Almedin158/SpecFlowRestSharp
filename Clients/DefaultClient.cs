using RestSharp;
using SpecFlowRestSharp.Configuration;

namespace SpecFlowRestSharp.Clients
{
    public class DefaultClient : IClient
    {
        private RestClient _restClient;
        private readonly RestClientOptions _restClientOptions;

        public DefaultClient()
        {
            _restClientOptions = new RestClientOptions();
        }

        public void Dispose()
        {
            if( _restClient != null ) { 
                _restClient.Dispose();
            }
        }

        public RestClient GetClient()
        {
            _restClientOptions.ThrowOnDeserializationError = true;
            _restClient = new RestClient(_restClientOptions);
            return _restClient;
        }
    }
}