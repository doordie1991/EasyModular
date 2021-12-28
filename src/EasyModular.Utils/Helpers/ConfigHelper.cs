using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace EasyModular.Utils
{
    /// <summary>
    /// 配置帮助类
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 根据路径获取配置文件内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T GetModel<T>(string url)
        {
            using var jsonReader = new StreamReader(url);

            var model = JsonHelper.JsonToEntity<T>(jsonReader.ReadToEnd());

            return model;
        }

        /// <summary>
        /// 根据路径获取配置文件内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static List<T> GetModels<T>(string url)
        {
            using var jsonReader = new StreamReader(url);

            var models = JsonHelper.JonsToList<T>(jsonReader.ReadToEnd());

            return models;
        }
    }
}
