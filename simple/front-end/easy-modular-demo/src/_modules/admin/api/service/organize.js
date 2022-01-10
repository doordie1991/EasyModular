export default name => {
  const root = `api/admin/${name}/`
  const crud = $emHttp.crud(root)

  const urls = {
    tree: root + 'tree',
    move: root + 'move'
  }

  /**
   * @description 获取组织架构树
   * @param {*} id
   */
  const getTree = () => {
    return $emHttp.get(urls.tree)
  }

  /**
   * @description 移动
   */
  const move = params => {
    return $emHttp.post(urls.move, params)
  }

  return {
    ...crud,
    getTree,
    move
  }
}
