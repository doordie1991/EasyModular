using Demo.Scheduling.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Scheduling.Application
{
    /// <summary>
    /// 任务日志新增模型
    /// </summary>
    public class JobLogAddModel
    {
        
        /// <summary>
        /// 任务编号
        /// </summary>
        public string JobId  { get; set; }
    
        /// <summary>
        /// 任务日志类型（0.信息、1.调试、2.异常）
        /// </summary>
        public JobLogType Type  { get; set; }
    
        /// <summary>
        /// 内容信息
        /// </summary>
        public string Msg  { get; set; }
    }
}
