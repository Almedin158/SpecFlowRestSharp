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

        /// <summary>
        /// Performs the request, cancels the request if the cancellation token timer expires
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public RestResponse Execute(IClient client, RestRequest request, int cancellationTokenMiliseconds=0)
        {
            _curl = CurlConverter.ConvertToCurl(request);

            //Cancellation configured globally in the json file, can also set to a custom value in the request execution
            var jsonCancellationToken = JsonProcessor.ReadJson("../../../JsonConfiguration/CancellationToken.json");
            if (cancellationTokenMiliseconds == 0 && (bool)jsonCancellationToken.CancellationToken)
            {
                cancellationTokenMiliseconds = (int)jsonCancellationToken.CancellationTokenTimerInMiliseconds;
            }
            if (cancellationTokenMiliseconds != 0)
            {
                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(cancellationTokenMiliseconds);
                var cancellationToken = cancellationTokenSource.Token;

                try
                {
                    _response = client.GetClient().Execute(request, cancellationToken);
                }
                catch (Exception ex)
                {

                    throw new Exception($"Error executing the request: {ex.Message}\n{_curl}");

                }
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new Exception($"Request was cancelled because the request took longer than {cancellationTokenMiliseconds} miliseconds to complete.\n{_curl}");
                }
                return _response;
            }

            try
            {
                _response = client.GetClient().Execute(request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing the request: {ex.Message}\n{_curl}");
            }

            return _response;
        }

        /// <summary>
        /// Converts the RestResponse to a dynamic object
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Converts the RestResponse to a dynamic array
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Converts XMl RestResponse to a dynamic object
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Converts XML RestResponse to a dynamic array
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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