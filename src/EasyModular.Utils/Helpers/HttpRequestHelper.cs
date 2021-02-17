using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EasyModular.Utils
{
    /// <summary>
    /// HttpRequest辅助类
    /// </summary>
    public static class HttpRequestHelper
    {
        /// <summary>
        /// 请求方式
        /// </summary>
        private enum HTTP_TYPE
        {
            GET,
            POST
        };

        /// <summary>
        /// 连接超时时间
        /// </summary>
        private const int TIME_OUT = 60*1000;

        /// <summary>
        /// 正文类型
        /// </summary>
        private static readonly string CONTENT_TYPE = "application/json";
        private static readonly string ACCEPT = "application/json";

        /// <summary>
        /// 最大接收2M报文 公式：1024 * 1024 * 2 
        /// </summary>
        private const long BUFFER_SIZE = 2097152;

        #region ==同步方法==

        #region Get同步

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static string Get(string url)
        {
            return Execute(url, string.Empty, HTTP_TYPE.GET, null);
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, object> data)
        {
            return Execute(url, data.ToUrlParamString(), HTTP_TYPE.GET, null);
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="headerParams">头部参数</param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, object> data, Dictionary<string, string> headerParams)
        {
            return Execute(url, data.ToUrlParamString(), HTTP_TYPE.GET, headerParams);
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="headerParams">头部参数</param>
        /// <param name="contentType">请求方式</param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, object> data, Dictionary<string, string> headerParams, string contentType)
        {
            return Execute(url, data.ToUrlParamString(), HTTP_TYPE.GET, headerParams, contentType);
        }

        #endregion

        #region Post同步
        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static string Post(string url)
        {
            return Execute(url, string.Empty, HTTP_TYPE.POST, null);
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static string Post(string url, Dictionary<string, object> data)
        {
            return Execute(url, data.ToJsonString(), HTTP_TYPE.POST, null);
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static string Post(string url, Dictionary<string, object> data, Dictionary<string, string> headerParams)
        {
            return Execute(url, data.ToJsonString(), HTTP_TYPE.POST, headerParams);
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static string Post(string url, Dictionary<string, object> data, Dictionary<string, string> headerParams, string contentType)
        {
            string strData = string.Empty;
            strData = data.ToJsonString();
            return Execute(url, strData, HTTP_TYPE.POST, headerParams, contentType);
        }
        #endregion

        #endregion

        #region ==异步方法==

        #region Get异步

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url)
        {
            return await ExecuteAsync(url, string.Empty, HTTP_TYPE.GET, null);
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, Dictionary<string, object> data)
        {
            return await ExecuteAsync(url, data.ToUrlParamString(), HTTP_TYPE.GET, null);
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, Dictionary<string, object> data, Dictionary<string, string> webHeaderCollection)
        {
            return await ExecuteAsync(url, data.ToUrlParamString(), HTTP_TYPE.GET, webHeaderCollection);
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, Dictionary<string, object> data, Dictionary<string, string> webHeaderCollection, string contentType)
        {
            return await ExecuteAsync(url, data.ToUrlParamString(), HTTP_TYPE.GET, webHeaderCollection, contentType);
        }

        #endregion

        #region Post异步
        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url)
        {
            return await ExecuteAsync(url, string.Empty, HTTP_TYPE.POST, null);
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, Dictionary<string, object> data)
        {
            return await ExecuteAsync(url, data.ToJsonString(),  HTTP_TYPE.POST, null);
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, Dictionary<string, object> data, string contentType)
        {
            string strData = string.Empty;
            strData = data.ToJsonString();

            return await ExecuteAsync(url, strData,  HTTP_TYPE.POST, null, contentType);
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, Dictionary<string, object> data, Dictionary<string, string> headerParams)
        {
            return await ExecuteAsync(url, data.ToJsonString(),  HTTP_TYPE.POST, headerParams);
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, Dictionary<string, object> data, Dictionary<string, string> headerParams, string contentType)
        {
            string strData = string.Empty;
            strData = data.ToJsonString();

            return await ExecuteAsync(url, strData,  HTTP_TYPE.POST, headerParams, contentType);
        }

        #endregion

        #endregion

        #region ==Execute==
        /// <summary>
        /// 执行请求（同步）
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="httpType">请求类型：POST、GET</param>
        /// <param name="headerParams">请求header</param>
        /// <returns></returns>
        private static string Execute(string url, string data, HTTP_TYPE httpType, Dictionary<string, string> headerParams, string contentType = null)
        {
            string strResult = string.Empty;

            url = httpType== HTTP_TYPE.POST ? url : string.Concat(url, "?", data);

            if (url.StartsWith("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            }

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest; 
            try
            {
                StringBuilder strHeaders = new StringBuilder();
                if (null != headerParams)
                {
                    //加入头信息
                    foreach (var item in headerParams)
                    {
                        if (null == item.Key || string.IsNullOrWhiteSpace(item.Key))
                        {
                            continue;
                        }
                        strHeaders.Append($"{item.Key}={item.Value}");
                        request.Headers.Add(item.Key, item.Value);
                    }
                }

                request.Method = httpType.ToString();
                request.ContentType = contentType ?? CONTENT_TYPE;
                request.Accept = ACCEPT;
                request.Timeout = TIME_OUT;
                request.Headers.Set("Cache-Control", "no-cache");

                if (httpType == HTTP_TYPE.POST)
                {
                    if (data.Length > 0)
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(data);
                        request.ContentLength = bytes.Length;

                        using var reqstream = request.GetRequestStream();
                        reqstream.Write(bytes, 0, bytes.Length);
                    }
                }

                using var response = (HttpWebResponse)request.GetResponse();
                using var streamReceive = response.GetResponseStream();
                using var streamReader = new StreamReader(streamReceive, Encoding.UTF8);

                strResult = $"{streamReader.ReadToEnd()}";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != request)
                {
                    request.Abort();
                }
            }

            return strResult;
        }

        /// <summary>
        /// 执行请求（异步）
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="httpType">请求类型：POST、GET</param>
        /// <param name="headerParams">请求header</param>
        /// <returns></returns>
        private static async Task<string> ExecuteAsync(string url, string data, HTTP_TYPE httpType, Dictionary<string, string> headerParams, string contentType = null)
        {
            string strResult = string.Empty;

            url = httpType == HTTP_TYPE.POST ? url : string.Concat(url, "?", data);

            if (url.StartsWith("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            }

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest; ;
            try
            {
                StringBuilder strHeaders = new StringBuilder();
                if (null != headerParams)
                {
                    //加入头信息
                    foreach (var item in headerParams)
                    {
                        if (null == item.Key || string.IsNullOrWhiteSpace(item.Key))
                        {
                            continue;
                        }
                        strHeaders.Append($"{item.Key}={item.Value}");
                        request.Headers.Add(item.Key, item.Value);
                    }
                }


                //写数据
                request.Method = httpType.ToString();
                request.ContentType = contentType ?? CONTENT_TYPE;
                request.Accept = ACCEPT;
                request.Timeout = TIME_OUT;
                request.Headers.Set("Cache-Control", "no-cache");

                if (httpType == HTTP_TYPE.POST)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(data);
                    request.ContentLength = bytes.Length;

                    using var reqstream = request.GetRequestStream();
                    reqstream.Write(bytes, 0, bytes.Length);
                }

                using var response = (HttpWebResponse)await request.GetResponseAsync();
                using var streamReceive = response.GetResponseStream();
                using var streamReader = new StreamReader(streamReceive, Encoding.UTF8);

                strResult = await streamReader.ReadToEndAsync();

                return strResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != request)
                {
                    request.Abort();
                }
            }
        }
        #endregion
    }
}
