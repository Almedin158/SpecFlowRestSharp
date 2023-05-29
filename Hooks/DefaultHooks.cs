using NUnit.Framework;
using RestSharp;
using SpecFlowRestSharp.Clients;
using SpecFlowRestSharp.Configuration;

//Parallel exeuction works on fixture (feature) level, you can NOT parallelly execute multiple tests inside of the same feature file.
[assembly: Parallelizable(ParallelScope.Fixtures)]
//Limits the number of possible parallel executions
[assembly: LevelOfParallelism(8)]

namespace SpecFlowRestSharp.Hooks
{
    [Binding]
    public class DefaultHooks:HookSetup
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        // I can make this class binding again, and remove the inheritance in stepdefinitions, but in that case I need to set IClient _client inside of the stepdefinition and perform initialization of the _client at the begining of each step and perofmr disposing at the end of each step, so I believe this is a better alternative.
        protected IClient _client;

        [BeforeStep]
        public void BeforeStep()
        {
            //Set which client to use
            _client = new DefaultClient();
        }

        [AfterStep]
        public void AfterStep()
        {
            _client.Dispose();
        }

        public void Execute(RestRequest request)
        {
            Execute(_client, request);
        }
    }
}