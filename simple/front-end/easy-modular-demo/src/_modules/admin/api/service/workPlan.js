export default name => {
    const root = `api/admin/${name}/`
    const crud = $emHttp.crud(root)
  
    return {
      ...crud
    }
  }
  