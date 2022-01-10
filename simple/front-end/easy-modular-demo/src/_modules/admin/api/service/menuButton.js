export default name => {
  const root = `api/admin/${name}/`
  const crud = $emHttp.crud(root)

  const urls = {
    queryByMenuId: root + 'queryByMenuId'
  }

  /**
   * @description 根据菜单Id查询
   * @param {*} id
   */
  const queryByMenuId = menuId => {
    const url = `${urls.queryByMenuId}?menuId=${menuId}`
    return $emHttp.get(url)
  }

  return {
    ...crud,
    queryByMenuId
  }
}
