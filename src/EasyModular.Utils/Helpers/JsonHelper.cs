using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace EasyModular.Utils
{
    /// <summary>
    /// Json辅助类
    /// </summary>
    public static class JsonHelper
    {
        public static object ToJson(this string Json)
        {
            return JsonSerializer.Serialize(Json);
        }

        public static string ToJson(this object obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public static List<T> JonsToList<T>(this string Json)
        {
            return JsonSerializer.Deserialize<List<T>>(Json, new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            });
        }

        public static T JsonToEntity<T>(this string Json)
        {
            return JsonSerializer.Deserialize<T>(Json, new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            });
        }
    }
}
