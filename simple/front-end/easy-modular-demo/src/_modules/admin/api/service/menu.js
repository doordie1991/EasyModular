export default name => {
  const root = `api/admin/${name}/`
  const crud = $emHttp.crud(root)

  const urls = {
    move: root + 'move',
    tree: root + 'tree',
    bindBtn: root + 'bindBtn'
  }

  /**
   * @description 移动
   */
  const move = params => {
    return $emHttp.post(urls.move, params)
  }

  /**
   * @description 获取菜单树
   */
  const getTree = () => {
    return $emHttp.get(urls.tree)
  }

  /**
   * @description 绑定按钮
   */
  const bindBtn = params => {
    return $emHttp.post(urls.bindBtn, params)
  }

  return {
    ...crud,
    move,
    getTree,
    bindBtn
  }
}
