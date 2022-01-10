export default name => {
  const root = `api/admin/${name}/`
  const crud = $emHttp.crud(root)

  const urls = {
    select: root + 'select'
  }
  
  const select = params => {
    return $emHttp.get(urls.select, params)
  }

  return {
    ...crud,
    select
  }
}
