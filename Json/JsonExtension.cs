using System;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace CSharpExtension
{
    public static class JsonExtension
    {
        public static string ToJson<T>(this T obj) where T : new()
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T ToJson<T>(this T obj, string filePath,FileMode fm = FileMode.Create) where T : new()
        {
            string jsonStr = obj.ToJson();
            File.WriteAllText(filePath,jsonStr);
            using(FileStream fs = new FileStream(filePath, fm))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(jsonStr);
                fs.Write(info, 0, info.Length);
            }
            return obj;
        }

        public static T GetFromJson<T>(string json) where T : new()
        {
            T item = JsonConvert.DeserializeObject<T>(json);
            return item;
        }

        public static T GetFromJson<T>(FileInfo f) where T : new()
        {
            using(FileStream fs = new FileStream(f.FullName, FileMode.Open))
            {
                using(StreamReader sr = new StreamReader(fs))
                {
                     return GetFromJson<T>(sr.ReadToEnd());
                }
            }
        }



    }


}

