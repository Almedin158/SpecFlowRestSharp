using RestSharp;
using SpecFlowRestSharp.Clients;
using SpecFlowRestSharp.Configuration;
using SpecFlowRestSharp.Utility;

namespace SpecFlowRestSharp.Hooks
{
    //Can not use binding, else BeforeStep and AfterStep would be performed twice each.
    //[Binding]
    public class DefaultHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        //I can make this class binding again, and remove the inheritance in stepdefinitions, but in that case I need to set IClient _client inside of the stepdefinition and perform initialization of the _client at the begining of each step and perofmr disposing at the end of each step, so I believe this is a better alternative.
        protected IClient _client;
        protected RestResponse _response;
        protected dynamic JObj;
        protected dynamic JArr;
        protected string _curl;

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

        public RestResponse Execute(RestRequest request)
        {
            _curl = CurlConverter.ConvertToCurl(request);

            try
            {
                _response = _client.GetClient().Execute(request);
                return _response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing the request: {ex.Message}\n{_curl}");
            }
        }
        public dynamic ConvertToJObject()
        {
            try
            {
                JObj = DynamicConverter.ConvertToJObject(_response.Content);
                return JObj;
            }
            catch
            {
                throw new Exception($"Response body is not available\n Status message: {_response.Content}\n{_curl}");
            }
        }

        public dynamic ConvertToJArray()
        {
            try
            {
                JArr = DynamicConverter.ConvertToJObject(_response.Content);
                return JArr;
            }
            catch
            {
                throw new Exception($"Response body is not available\n Status message: {_response.Content}\n{_curl}");
            }
        }
    }
}