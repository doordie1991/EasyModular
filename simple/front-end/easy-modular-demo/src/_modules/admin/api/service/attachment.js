export default name => {
  const root = `api/admin/${name}/`
  const crud = $emHttp.crud(root)

  const urls = {
    upload: root + 'upload',
    download: root + 'download/',
    queryByIds: root + 'queryByIds',
    getDownloadUrl: root + 'getUrl'
  }

  const getUploadUrl = () => {
    return $emHttp.axios.defaults.baseURL + urls.upload
  }

  // 下载
  const download = id => {
    return $emHttp.download(urls.download + id)
  }

  // 预览
  const preview = id => {
    return $emHttp.preview(urls.download + id)
  }

  // 根据Id查询
  const queryByIds = params => {
    return $emHttp.post(urls.queryByIds, params)
  }

  // 获取下载路径
  const getDownloadUrl = params => {
    return $emHttp.get(urls.getDownloadUrl, params)
  }

  return {
    ...crud,
    getUploadUrl,
    download,
    queryByIds,
    preview,
    getDownloadUrl
  }
}
