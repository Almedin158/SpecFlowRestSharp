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
    public class DefaultHooks : HookSetup
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        // I can make this class binding again, and remove the inheritance in stepdefinitions, but in that case I need to set IClient _client inside of the stepdefinition and perform initialization of the _client at the begining of each step and perofmr disposing at the end of each step, so I believe this is a better alternative.
        protected IClient _client;

        /// <summary>
        /// Initializes the client before each request
        /// </summary>
        [BeforeStep]
        public void BeforeStep()
        {
            _client = new DefaultClient();
        }

        /// <summary>
        /// Disposes connection after each request
        /// </summary>
        [AfterStep]
        public void AfterStep()
        {
            _client.Dispose();
        }

        /// <summary>
        /// Performs the request, optional parameter is cancellation token timer in miliseconds after the timer expires the request gets cancelled
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationTokenMiliseconds"></param>
        public void Execute(RestRequest request, int cancellationTokenMiliseconds = 0)
        {
            Execute(_client, request, cancellationTokenMiliseconds);
        }
    }
}