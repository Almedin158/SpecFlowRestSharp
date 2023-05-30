using Newtonsoft.Json;

namespace SpecFlowRestSharp.Utility
{
    public static class JsonProcessor
    {
        /// <summary>
        /// Reads a json file specified by the filepath, returns it as a dynamic object
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static dynamic ReadJson(string filePath)
        {
            // Check if the file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");
            }

            // Read the contents of the file
            string json = File.ReadAllText(filePath);

            // Deserialize the JSON into an object
            dynamic obj = JsonConvert.DeserializeObject(json);
            return obj;
        }

        /// <summary>
        /// Serializes a object and overrides the existing json file specified by the path
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="obj"></param>
        /// <exception cref="FileNotFoundException"></exception>
        public static void UpdateJson(string filePath, object obj)
        {
            //This check throws an error if the file doens't exist, without this error the missing file would be created.
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");
            }

            string updatedJson = JsonConvert.SerializeObject(obj);

            File.WriteAllText(filePath, updatedJson);
        }

        /// <summary>
        /// Serializes a object and creates a new json file specified by the path
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="obj"></param>
        public static void CreateJson(string filePath, object obj)
        {
            string updatedJson = JsonConvert.SerializeObject(obj);

            File.WriteAllText(filePath, updatedJson);
        }
    }
}