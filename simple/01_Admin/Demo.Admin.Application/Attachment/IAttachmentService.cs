
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application.AttachmentService
{
    /// <summary>
    /// 附件服务
    /// </summary>
    public interface IAttachmentService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(AttachmentQueryModel model);

        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IResultModel> QueryByIds(List<string> ids);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(AttachmentAddModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Delete(string id);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Edit(string id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Update(AttachmentUpdateModel model);

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        Task<IResultModel<AttachmentUploadResultModel>> Upload(AttachmentUploadModel model, FileInfo fileInfo);

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel<FileDownloadModel>> Download(string id);

        /// <summary>
        /// 获取路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel<string>> GetUrl(string id);

    }
}
