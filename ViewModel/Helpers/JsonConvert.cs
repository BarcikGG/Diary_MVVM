using System;
using Newtonsoft.Json;
using System.IO;

namespace Diary_MVVM.ViewModel.Helpers
{
    public static class Json
    {
        

        public static void Serialize<T>(T data, string filename)
        {
            var json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filename, json);
        }

        public static T Deserialize<T>(string filename)
        {
            var json = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
