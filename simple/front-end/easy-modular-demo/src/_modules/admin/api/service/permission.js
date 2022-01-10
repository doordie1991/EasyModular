export default name => {
    const root = `api/admin/${name}/`
    const crud = $emHttp.crud(root)
  
    const urls = {
        sync: root + 'sync'
    }
  
    /**
     * @description 同步
     * @param {*} id
     */
    const sync = () => {
      return $emHttp.get(urls.sync)
    }
  
    return {
      ...crud,
      sync
    }
  }
  