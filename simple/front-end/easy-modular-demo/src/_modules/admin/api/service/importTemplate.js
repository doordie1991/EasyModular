export default name => {
  const root = `api/admin/${name}/`
  const crud = $emHttp.crud(root)
  const urls = {
    queryByCode: root + 'queryByCode'
  }

  /**
   * @description 根据编码查询
   */
  const queryByCode = params => {
    return $emHttp.get(urls.queryByCode, params)
  }


  return {
    ...crud,
    queryByCode
  }
}
