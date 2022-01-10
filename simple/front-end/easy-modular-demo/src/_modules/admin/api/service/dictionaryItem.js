export default name => {
  const root = `api/admin/${name}/`
  const crud = $emHttp.crud(root)

  const urls = {
    importUrl: root + 'import'
  }

  /**
   * @description 导入接口路径
   */
  const importUrl = () => {
    return $emHttp.axios.defaults.baseURL + urls.importUrl
  }

  return {
    ...crud,
    importUrl
  }
}
