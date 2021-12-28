
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileInfo = EasyModular.Utils.FileInfo;
using Demo.Admin.Infrastructure;
using EasyModular;

namespace Demo.Admin.Application.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IMapper _mapper;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMediaTypeRepository _mediaTypeRepository;
        private readonly WebConfigModel _webConfig;
        public AttachmentService(IMapper mapper, IAttachmentRepository attachmentRepository, IMediaTypeRepository mediaTypeRepository, WebConfigModel webConfig)
        {
            _mapper = mapper;
            _attachmentRepository = attachmentRepository;
            _mediaTypeRepository = mediaTypeRepository;
            _webConfig = webConfig;
        }

        public async Task<IResultModel> Query(AttachmentQueryModel model)
        {
            var result = new QueryResultModel<AttachmentEntity>
            {
                Rows = await _attachmentRepository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> QueryByIds(List<string> ids)
        {
            var result = await _attachmentRepository.GetListAsync(m => ids.Contains(m.Id) && m.IsDel == false);
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(AttachmentAddModel model)
        {
            var entity = _mapper.Map<AttachmentEntity>(model);
            var result = await _attachmentRepository.InsertAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(string id)
        {
            var entity = await _attachmentRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var result = await _attachmentRepository.SoftDeleteAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(string id)
        {
            var entity = await _attachmentRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<AttachmentUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(AttachmentUpdateModel model)
        {
            var entity = await _attachmentRepository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            var result = await _attachmentRepository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel<AttachmentUploadResultModel>> Upload(AttachmentUploadModel model, FileInfo fileInfo)
        {
            var result = new ResultModel<AttachmentUploadResultModel>();

            var entity = new AttachmentEntity
            {
                FileName = model.Name.NotNull() ? model.Name : fileInfo.FileName,
                SaveName = fileInfo.SaveName,
                Ext = fileInfo.Ext,
                Md5 = fileInfo.Md5,
                Path = fileInfo.Path,
                FullPath = Path.Combine(fileInfo.Path, fileInfo.SaveName),
                Size = fileInfo.Size.Size,
                SizeCn = fileInfo.Size.ToString(),

                Module = model.Module,
                Group = model.Group
            };

            var media = await _mediaTypeRepository.FirstAsync(m => m.Ext == fileInfo.Ext);

            entity.MediaType = media?.Value;

            if (await _attachmentRepository.InsertAsync(entity))
            {
                var resultModel = _mapper.Map<AttachmentUploadResultModel>(entity);
                return result.Success(resultModel);
            }

            return result.Failed("上传失败");
        }


        public async Task<IResultModel<FileDownloadModel>> Download(string id)
        {
            var result = new ResultModel<FileDownloadModel>();

            var attachment = await _attachmentRepository.FirstAsync(id);
            if (attachment == null)
                return result.Failed("附件不存在");

            var filePath = Path.Combine(_webConfig.UploadPath, attachment.FullPath);
            if (!File.Exists(filePath))
                return result.Failed("附件不存在");

            return result.Success(new FileDownloadModel(filePath, attachment.FileName, attachment.MediaType));
        }

        public async Task<IResultModel<string>> GetUrl(string id)
        {
            var result = new ResultModel<string>();

            var attachment = await _attachmentRepository.FirstAsync(id);
            if (attachment == null)
                return result.Failed("附件不存在");

            return result.Success(attachment.FullPath);
        }
    }
}
