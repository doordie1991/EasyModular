export default name => {
    const root = `api/admin/${name}/`
    const crud = $emHttp.crud(root)
  
    const urls = {
        save: root + 'save'
    }
  
    /**
     * @description 保存
     * @param {*} id
     */
    const save = params => {
      return $emHttp.post(urls.save,params)
    }
  
    return {
      ...crud,
      save
    }
  }
  