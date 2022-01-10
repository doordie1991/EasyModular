
export default name => {
  const root = `api/admin/${name}/`

  const urls = {
    login: root + 'login',
    refreshToken: root + 'refreshToken',
    getAuthInfo: root + 'authInfo',
    verifyCode: root + 'verifyCode'
  }

  /**
   * @description 登录
   * @param {*} params
   */
  const login = async params => {
    return $emHttp.post(urls.login, params)
  }

  /**
   * @description 刷新令牌
   */
  const refreshToken = refreshToken => {
    return $emHttp.get(urls.refreshToken, { refreshToken })
  }

  /**
   * @description: 获取认证信息
   * @param {*}
   */

  const getAuthInfo = () => {
    return $emHttp.get(urls.getAuthInfo)
  }

  /**
   * @description 获取验证码
   */
  const getVerifyCode = () => {
    return $emHttp.get(urls.verifyCode)
  }


  return {
    login,
    refreshToken,
    getAuthInfo,
    getVerifyCode
  }
}
