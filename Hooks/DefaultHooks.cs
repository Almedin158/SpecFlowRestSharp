using SpecFlowRestSharp.Clients;
using SpecFlowRestSharp.Configuration;

namespace SpecFlowRestSharp.Hooks
{
    //Can not use binding, else BeforeStep and AfterStep would be performed twice each.
    //[Binding]
    public class DefaultHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        public IClient client;

        [BeforeStep]
        public void BeforeStep()
        {
            client = new DefaultClient();
        }

        [AfterStep]
        public void AfterStep()
        {
            client.Dispose();
        }
    }
}