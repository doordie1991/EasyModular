using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;


namespace EasyModular.Utils
{
    /// <summary>
    /// Excel处理接口
    /// </summary>
    public interface IExcelHandler
    {
        /// <summary>
        /// 导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">数据</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        byte[] Export<T>(IList<T> entities, string title = null) where T : class, new();

        /// <summary>
        /// 导入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="formFile">文件</param>
        /// <param name="hasTitle">是否包含标题</param>
        /// <returns></returns>
        List<T> Import<T>(IFormFile formFile, bool hasTitle = false) where T : class, new();
    }
}
