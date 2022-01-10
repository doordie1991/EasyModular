export default name => {
    const root = `api/scheduling/${name}/`
    const crud = $emHttp.crud(root)
  
    return {
      ...crud
    }
  }
  