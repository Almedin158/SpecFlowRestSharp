using Newtonsoft.Json;
using RestSharp;
using SpecFlowRestSharp.Clients;
using SpecFlowRestSharp.Configuration;
using SpecFlowRestSharp.Utility;
using System.Xml.Linq;

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
        protected string _curl;
        protected dynamic JObj;
        protected dynamic JArr;
        

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
                JArr = DynamicConverter.ConvertToJArray(_response.Content);
                return JArr;
            }
            catch
            {
                throw new Exception($"Response body is not available\n Status message: {_response.Content}\n{_curl}");
            }
        }
        public dynamic ConvertXMLToJObject()
        {
            try
            {
                XDocument xmlDoc = XDocument.Parse(_response.Content);
                string jsonText = JsonConvert.SerializeXNode(xmlDoc);
                JObj = DynamicConverter.ConvertToJObject(jsonText);
                return JObj;
            }
            catch
            {
                throw new Exception($"Response body is not available\n Status message: {_response.Content}\n{_curl}");
            }
        }
        public dynamic ConvertXMLToJArray()
        {
            try
            {
                XDocument xmlDoc = XDocument.Parse(_response.Content);
                string jsonText = JsonConvert.SerializeXNode(xmlDoc);
                JArr = DynamicConverter.ConvertToJArray(jsonText);
                return JArr;
            }
            catch
            {
                throw new Exception($"Response body is not available\n Status message: {_response.Content}\n{_curl}");
            }
        }
    }
}