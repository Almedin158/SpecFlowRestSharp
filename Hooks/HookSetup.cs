using Newtonsoft.Json;
using RestSharp;
using SpecFlowRestSharp.Configuration;
using SpecFlowRestSharp.Utility;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SpecFlowRestSharp.Hooks
{
    public class HookSetup
    {
        public RestResponse _response;
        protected string _curl;

        /// <summary>
        /// Performs the request, cancels the request if the cancellation token timer expires
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public RestResponse Execute(IClient client, RestRequest request, int cancellationTokenMiliseconds = 0)
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
        public dynamic ConvertToDynamicObject()
        {
            var a = _response.ContentType.ToLower();
            if (_response.ContentType.ToLower().Contains("xml"))
            {
                try
                {
                    XDocument xmlDoc = XDocument.Parse(_response.Content);
                    string jsonText = JsonConvert.SerializeXNode(xmlDoc);
                    var JObj = DynamicConverter.ConvertToJObject(jsonText);
                    return JObj;
                }
                catch
                {
                    throw new Exception($"Response body is not available\n Status message: {_response.Content}\n{_curl}");
                }
            }
            else
            {
                try
                {
                    var JObj = DynamicConverter.ConvertToJObject(_response.Content);
                    return JObj;
                }
                catch
                {
                    throw new Exception($"Response body is not available\n Status message: {_response.Content}\n{_curl}");
                }
            }
        }

        /// <summary>
        /// Converts the RestResponse to a dynamic list
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public dynamic ConvertToDynamicList()
        {
            if (_response.ContentType.ToLower().Contains("xml"))
            {
                try
                {
                    XDocument xmlDoc = XDocument.Parse(_response.Content);
                    string jsonText = JsonConvert.SerializeXNode(xmlDoc);
                    var JArr = DynamicConverter.ConvertToJArray(jsonText);
                    return JArr;
                }
                catch
                {
                    throw new Exception($"Response body is not available\n Status message: {_response.Content}\n{_curl}");
                }
            }
            else
            {
                try
                {
                    var JArr = DynamicConverter.ConvertToJArray(_response.Content);
                    return JArr;
                }
                catch
                {
                    throw new Exception($"Response body is not available\n Status message: {_response.Content}\n{_curl}");
                }
            }
        }

        /// <summary>
        /// Converts the RestResponse to strongly typed object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T ConvertToObject<T>()
        {
            //If it is an XML response, converts it into a strongly typed object
            if (_response.ContentType.ToLower().Contains("xml"))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    using (TextReader reader = new StringReader(_response.Content))
                    {
                        T obj = (T)serializer.Deserialize(reader);
                        return obj;
                    }
                }
                catch
                {
                    throw new Exception($"Response body is not available\n Status message: {_response.Content}\n{_curl}");
                }
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(_response.Content);
            }
            catch
            {
                throw new Exception($"Failed to convert response to {typeof(T).Name}\nStatus message: {_response.Content}\n{_curl}");
            }
        }

        /// <summary>
        /// Converts the RestResponse to strongly typed list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<T> ConvertToList<T>()
        {
            //If it is an XML response, converts it into a strongly typed list
            if (_response.ContentType.ToLower().Contains("xml"))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                    using (TextReader reader = new StringReader(_response.Content))
                    {
                        List<T> objList = (List<T>)serializer.Deserialize(reader);
                        return objList;
                    }
                }
                catch
                {
                    throw new Exception($"Response body is not available\n Status message: {_response.Content}\n{_curl}");
                }
            }

            try
            {
                return JsonConvert.DeserializeObject<List<T>>(_response.Content);
            }
            catch
            {
                throw new Exception($"Failed to convert response to List<{typeof(T).Name}>\nStatus message: {_response.Content}\n{_curl}");
            }
        }
    }
}