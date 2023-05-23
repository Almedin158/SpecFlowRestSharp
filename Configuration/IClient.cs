using RestSharp;
using System.Security.Policy;

namespace SpecFlowRestSharp.Configuration
{
    public interface IClient : IDisposable
    {
        RestClient GetClient();
    }
}