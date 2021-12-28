
using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.AttachmentService;
using Demo.Admin.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using EasyModular.Auth;
using Microsoft.AspNetCore.Http;
using System.IO;
using Demo.Admin.Infrastructure;
using EasyModular;

namespace Demo.Admin.Web
{
    [Description("附件")]
    public class AttachmentController : ModuleController
    {
        private readonly IAttachmentService _service;
        private readonly WebConfigModel _webConfig;

        public AttachmentController(ILoginInfo loginInfo, IAttachmentService AttachmentService, WebConfigModel webConfig)
        {
            _service = AttachmentService;
            _webConfig = webConfig;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery] AttachmentQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("查询")]
        public Task<IResultModel> QueryByIds([FromBody] List<string> ids)
        {
            return _service.QueryByIds(ids);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(AttachmentAddModel model)
        {
            return _service.Add(model);
        }

        [HttpGet]
        [Description("编辑")]
        public Task<IResultModel> Edit([BindRequired] string id)
        {
            return _service.Edit(id);
        }

        [HttpPost]
        [Description("更新")]
        public Task<IResultModel> Update(AttachmentUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired] string id)
        {
            return _service.Delete(id);
        }


        [HttpPost]
        [Description("上传")]
        [Common]
        public async Task<IResultModel> Upload([FromForm] AttachmentUploadModel model, IFormFile formFile)
        {
            var uploadModel = new FileUploadModel
            {
                Request = Request,
                FormFile = formFile,
                RootPath = Path.Combine(AppContext.BaseDirectory, _webConfig.UploadPath),
                Module = model.Module,
                Group = model.Group
            };

            var result = await FileUploadHelper.Upload(uploadModel);

            if (result.Successful)
            {
                var resultModel = await _service.Upload(model, result.Data);
                if (resultModel.Successful)
                {
                    var url = Request.GetHost($"/admin/attachment/download/{resultModel.Data.Id}");
                    resultModel.Data.Url = new Uri(url).ToString();
                    return ResultModel.Success(resultModel);
                }
            }

            return ResultModel.Failed("上传失败");
        }

        [HttpGet("{id}")]
        [Description("下载")]
        [Common]
        public async Task<IActionResult> Download([BindRequired] string id)
        {
            var result = await _service.Download(id);
            if (result.Successful)
            {
                var file = result.Data;
                return PhysicalFile(file.FilePath, file.MediaType, file.Name, true);
            }

            return new JsonResult(result);
        }

        [HttpGet]
        [Description("获取路径")]
        [Common]
        public async Task<IResultModel> GetUrl([BindRequired] string id)
        {
            var result = await _service.GetUrl(id);
            if (!result.Successful)
                return ResultModel.Failed(result.Msg);

            var url = $"upload/{result.Data}";

            return ResultModel.Success(url);
        }
    }
}

