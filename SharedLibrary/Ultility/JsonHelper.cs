using System.Text.Json;
using System;
using System.IO;

namespace SharedLibrary.Ultility
{
    public static class JsonHelper
    {
        //Read functions
        public static object Deserialize(string json, Type type)
        {
            object? result = JsonSerializer.Deserialize(json, type);
            if (result == null)
                throw new JsonException("Unable to parse json");
            return result;
        }

        /// <summary>
        ///     Read and parse into type
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ReadJsonFile(string path, Type type)
        {
            string json = File.ReadAllText(path);
            return Deserialize(json, type);
        }
        //Write functinos
        public static void SaveJsonFile(string filename, object data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(filename, jsonString);
        }
        
        public static string ToJson(object data)
        {
            return JsonSerializer.Serialize(data);
        }
    }
}
