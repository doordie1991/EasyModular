export default name => {
  const root = `api/admin/${name}/`
  const crud = $emHttp.crud(root)
  const urls = {
    queryAll: root + 'queryAll'
  }

  const queryAll = () => {
    return $emHttp.get(urls.queryAll)
  }

  return {
    ...crud,
    queryAll
  }
}
