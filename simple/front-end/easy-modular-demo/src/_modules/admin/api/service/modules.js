export default name => {
  const root = `api/admin/${name}/`
  const crud = $emHttp.crud(root)

  const urls = {
    select: root + 'select'
  }

  /**
   * @description 下拉
   * @param {*} id
   */
  const select = () => {
    return $emHttp.get(urls.select)
  }


  return {
    ...crud,
    select
  }
}
