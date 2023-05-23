using SpecFlowRestSharp.Clients;
using SpecFlowRestSharp.Configuration;

namespace SpecFlowRestSharp.Hooks
{
    //Can not use binding, else BeforeStep and AfterStep would be performed twice each.
    //[Binding]
    public class DefaultHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        //I can make this class binding again, and remove the inheritance in stepdefinitions, but in that case I need to set IClient _client inside of the stepdefinition and perform initialization of the _client at the begining of each step and perofmr disposing at the end of each step, so I believe this is a better alternative.
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