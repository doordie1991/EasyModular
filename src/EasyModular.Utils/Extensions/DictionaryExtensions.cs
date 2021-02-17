using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.Utils
{
    /// <summary>
    /// 字段扩展
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 扩展Dictionary类，直接将参数转换为get请求的参数
        /// </summary>
        /// <param name="obj">字典数据</param>
        /// <returns></returns>
        public static string ToUrlParamString(this Dictionary<string, string> obj)
        {
            if (null == obj)
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in obj)
            {
                string key = item.Key;
                var value = item.Value;
                if (string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }
                stringBuilder.AppendFormat($"{key.Trim()}={value}&");
            }
            return stringBuilder.ToString().TrimEnd('&');
        }

        /// <summary>
        /// 扩展Dictionary类，直接将参数转换为get请求的参数
        /// </summary>
        /// <param name="obj">字典数据</param>
        /// <returns></returns>
        public static string ToUrlParamString(this Dictionary<string, object> obj)
        {
            if (null == obj)
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in obj)
            {
                string key = item.Key;
                string value = string.Empty;
                if (null != item.Value && !string.IsNullOrWhiteSpace(item.Value.ToString()))
                {
                    value = item.Value.ToString().Trim();
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }
                stringBuilder.AppendFormat($"{key.Trim()}={value}&");
            }
            return stringBuilder.ToString().TrimEnd('&');
        }

        /// <summary>
        /// 扩展Dictionary类，直接将参数转换为post请求的参数
        /// </summary>
        /// <param name="obj">字典数据</param>
        /// <returns></returns>
        public static string ToJsonString(this Dictionary<string, object> obj)
        {
            if (null != obj && obj.Count > 0)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
            return string.Empty;
        }
    }
}
