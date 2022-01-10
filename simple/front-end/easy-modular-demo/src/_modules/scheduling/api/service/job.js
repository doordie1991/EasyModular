export default name => {
  const root = `api/scheduling/${name}/`
  const crud = $emHttp.crud(root)

  const urls = {
    pause: root + 'pause',
    resume: root + 'resume',
    stop: root + 'stop'
  }

  /**
   * @description 暂停
   * @param {*} id
   */
  const pause = id => {
    const url = `${urls.pause}?id=${id}`
    return $emHttp.post(url)
  }

  /**
   * @description 恢复
   * @param {*} params
   */
  const resume = id => {
    const url = `${urls.resume}?id=${id}`
    return $emHttp.post(url)
  }

  
  /**
   * @description 停止
   * @param {*} params
   */
   const stop = id => {
    const url = `${urls.stop}?id=${id}`
    return $emHttp.post(url)
  }

  return {
    ...crud,
    pause,
    resume,
    stop
  }
}
