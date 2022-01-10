export default name => {
  const root = `api/admin/${name}/`
  const crud = $emHttp.crud(root)

  const urls = {
    sync: root + 'sync',
    permissions: root + 'permissions'
  }

  /**
   * @description 同步
   * @param {*} id
   */
  const sync = () => {
    return $emHttp.get(urls.sync)
  }

  /**
   * @description 同步
   * @param {*} id
   */
  const permissions = params => {
    return $emHttp.post(urls.permissions, params)
  }

  return {
    ...crud,
    sync,
    permissions
  }
}
