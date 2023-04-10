using RestSharp;

namespace SpecFlowRestSharp.Configuration
{
    public interface IClient : IDisposable
    {
        RestClient GetClient();
    }
}
