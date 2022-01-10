export default name => {
  const root = `api/admin/${name}/`
  const crud = $emHttp.crud(root)

  const urls = {
    select: root + 'select',
    tree: root + 'tree'
  }
  const select = params => {
    return $emHttp.get(urls.select, params)
  }

  const tree = params => {
    return $emHttp.get(urls.tree, params)
  }

  return {
    ...crud,
    select,
    tree
  }
}
