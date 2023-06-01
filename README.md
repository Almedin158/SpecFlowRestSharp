RestSharp with SpecFlow

Dependencies: 
- Microsoft.Net.Test.Sdk
- FluentAssertions
- NUnit
- NUnit3TestAdapter
- RestSharp
- SpecFlowNunit

RestSharp developed using the builder design pattern. 
AbstractRequest class contains a list of function that allow request modifications.
Delete/Get/Post/Put and PatchRequestBuilder based on the AbstractRequest class.

IClient interface contains the GetClient() method which returns the client, also inherits the IDisposable interface.
In the current framework, there is a DefaultClient which inherits the IClient interface and implements its methods, DefaultClient has no custom options set.
You can also add a new client with custom options if required by inheriting the IClient interface and implementing the Dispose() and GetClient() method, modify the client options inside of the class constructor.

Since SpecFlow is implemented, tests are written in a behavior driver development format (Given/When/Then).
To view an example of this click on the "DeleteBooking.feature" file inside of the "Features" folder.
Step definitions associated with the feature file are located in the "StepDefinitions" folder.
SpecFlow allows for parallel executions of tests on the feature level, meaning you can NOT parallelly execute multiple tests inside of the same feature file.
Parallel text execution limit is set via the "[assembly: LevelOfParallelism(8)]" found inside of the "DefaultHooks.cs" file.

As required by the RestSharp documentation, connection disposal is performed at the end of each test step in the [AfterStep] hook, this also requires creating a new instance of the RestSharp client before each step using the [BeforeStep] hook in the "DefaultHook.cs" file.
The "DefaultHook.cs" file inherits the HookSetup class, HookSetup contains the RestResponse parameter in which the API response is saved in after request executions, this class also contains functions for response conversion into dynamic objects.

Methods for reading, updating and creating json files can be found in the "JsonProcessor.cs" file.

An example of the RestSharp usage can be found by clicking on the "DeleteBookingStepDefinitions.cs" file inside of the "StepDefinitions" folder.
The test steps follow a general structure: 
1. Declare or read values from external files in to parameters (headers, body, url, endpoints etc.)
2. Following the builder pattern create a new RestRequest (DeleteRequestBuilder, GetRequestBuilder etc.), using the .With methods add the required parameters to the request and using .Build() return the RestRequest.
3. Perform the DefaultHook.Execute() method by passing the request.

It seems as code reusability could have been implemented better in the "DeleteBookingStepDefinitions.cs" file, but since the headers, body etc.. parameters differ in each request, I found this to be the best solution.

To access the response in the form of a dynamic object, perform the DefaultHook.ConvertToJObject or DefaultHook.ConvertToJArray function depending if the response is an object or array. (Same goes for xml responses via the DefaultHook.ConvertXMLToJObject and DefaultHook.ConvertXMLToJArray functions)

In the "CurlConverter.cs" file, the ConvertToCurl() method generates a cURL request based on the RestRequest during the Execute() method, this request works with raw JSON, form-data and xxx-form-urlencoded request bodies. In case an error happens during request execution and response conversion an error message is thrown containing the cUrl request (which can then be pasted inside of Command Prompt/Postman or similar tool to easily reproduce the issue).

Reporter not implemented, in a pipeline if the test fails an error message will be thrown which contains the cURL request, so the issue is easily reproducable.


DeleteBooking.json file should be .gitignored or hidden, I left it here so you can run the test.


31.05.2023.
Implemented cancellation token using the CancellationToken.json configuration file for the global level in the "HookSetup.cs" Execute() method, can be overriden by passing an int value (miliseconds) into the Execute method inside of test steps.