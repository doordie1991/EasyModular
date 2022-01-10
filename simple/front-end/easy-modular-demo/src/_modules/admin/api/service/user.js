export default name => {
  const root = `api/admin/${name}/`
  const crud = $emHttp.crud(root)

  const urls = {
    updatePassword: root + 'updatePassword',
    select: root + 'select',
    queryByUserIds: root + 'queryByUserIds',
    queryByUserCodes: root + 'queryByUserCodes',
    queryLatestSelect: root + 'queryLatestSelect',
    queryBySameOrg: root + 'queryBySameOrg',
    enable: root + 'enable',
    disable: root + 'disable',
    changeEnterprise: root + 'changeEnterprise',
    recoverEnterprise: root + 'recoverEnterprise',
    saveLatestSelect: root + 'saveLatestSelect',
    tree: root + 'tree',
    move: root + 'move'
  }

  /**
   * @description: 更新密码
   * @param {*} params
   */
  const updatePassword = params => {
    return $emHttp.post(urls.updatePassword, params)
  }

  /**
   * @description: 下拉
   * @param {*} params
   */
  const select = params => {
    return $emHttp.get(urls.select, params)
  }

  /**
   * @description: 根据多个用户Id查询
   * @param {*} params
   */
   const queryByUserIds = params => {
    return $emHttp.post(urls.queryByUserIds, params)
  }

  /**
   * @description: 根据多个账号查询
   * @param {*} params
   */
  const queryByUserCodes = params => {
    return $emHttp.post(urls.queryByUserCodes, params)
  }

  /**
   * @description: 查询最近选择
   * @param {*} params
   */
  const queryLatestSelect = params => {
    return $emHttp.get(urls.queryLatestSelect, params)
  }

  /**
   * @description: 查询同组织
   * @param {*} params
   */
  const queryBySameOrg = params => {
    return $emHttp.get(urls.queryBySameOrg, params)
  }

  /**
   * @description 启用
   * @param {*} id
   */
  const enable = id => {
    const url = `${urls.enable}?id=${id}`
    return $emHttp.post(url)
  }

  /**
   * @description 禁用
   * @param {*} params
   */
  const disable = id => {
    const url = `${urls.disable}?id=${id}`
    return $emHttp.post(url)
  }

  /**
   * @description 企业换企业
   * @param {*} params
   */
  const changeEnterprise = id => {
    const url = `${urls.changeEnterprise}?proxyEnterpriseId=${id}`
    return $emHttp.post(url)
  }

  /**
   * @description 恢复用户原企业
   * @param {*} params
   */
  const recoverEnterprise = () => {
    return $emHttp.post(urls.recoverEnterprise)
  }

  /**
   * @description: 保存最近选择
   * @param {*} params
   */
  const saveLatestSelect = params => {
    return $emHttp.post(urls.saveLatestSelect, params)
  }

  /**
   * @description: 获取用户树形列表
   * @param {*} params
   */
  const getTree = params => {
    return $emHttp.get(urls.tree, params)
  }

  /**
   * @description 移动
   */
  const move = params => {
    return $emHttp.post(urls.move, params)
  }

  return {
    ...crud,
    updatePassword,
    select,
    queryByUserIds,
    queryByUserCodes,
    queryLatestSelect,
    queryBySameOrg,
    enable,
    disable,
    changeEnterprise,
    recoverEnterprise,
    saveLatestSelect,
    getTree,
    move
  }
}
