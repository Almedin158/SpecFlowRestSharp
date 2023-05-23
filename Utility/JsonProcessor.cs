using Newtonsoft.Json;

namespace SpecFlowRestSharp.Utility
{
    public static class JsonProcessor
    {
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
    }
}