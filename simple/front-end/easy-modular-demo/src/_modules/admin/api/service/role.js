export default name => {
  const root = `api/admin/${name}/`
  const crud = $emHttp.crud(root)

  const urls = {
    tree: root + 'tree',
    menuTree: root + 'menuTree',
    bindMenuPermission: root + 'bindMenuPermission'
  }

  /**
   * @description 获取角色树形单列表
   */
  const getTree = () => {
    return $emHttp.get(urls.tree)
  }

  /**
   * @description 获取角色的关联菜单树形列表
   */
  const menuTree = params => {
    return $emHttp.get(urls.menuTree, params)
  }

  /**
   * @description 绑定菜单权限
   */
  const bindMenuPermission = params => {
    return $emHttp.post(urls.bindMenuPermission, params)
  }


  return {
    ...crud,
    getTree,
    menuTree,
    bindMenuPermission
  }
}
