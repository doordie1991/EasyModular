export default name => {
  const root = `api/scheduling/${name}/`
  const crud = $emHttp.crud(root)

  const urls = {
    select: root + 'select'
  }

  /**
   * @description: 下拉
   */
  const select = () => {
    return $emHttp.get(urls.select)
  }

  return {
    ...crud,
    select
  }
}
