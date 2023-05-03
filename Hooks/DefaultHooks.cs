using SpecFlowRestSharp.Clients;
using SpecFlowRestSharp.Configuration;
using System.Security.Policy;

namespace SpecFlowRestSharp.Hooks
{
    //Can not use binding, else BeforeStep and AfterStep would be performed twice each.
    //[Binding]
    public class DefaultHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        protected IClient _client;

        [BeforeStep]
        public void BeforeStep()
        {
            _client = new DefaultClient();
        }

        [AfterStep]
        public void AfterStep()
        {
            _client.Dispose();
        }
    }
}