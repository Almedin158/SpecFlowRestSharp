using Newtonsoft.Json;
using RestSharp;
using SpecFlowRestSharp.Configuration;
using SpecFlowRestSharp.Utility;
using System.Xml.Linq;

namespace SpecFlowRestSharp.Hooks
{
    public class HookSetup
    {
        public RestResponse _response;
        protected string _curl;
        public dynamic JObj;
        public dynamic JArr;

        public RestResponse Execute(IClient client, RestRequest request)
        {
            _curl = CurlConverter.ConvertToCurl(request);

            try
            {
                _response = client.GetClient().Execute(request);
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