export default name => {
  const root = `api/admin/${name}/`
  const crud = $emHttp.crud(root)

  const get = () => {
    const config = {
      title: '企业双控系统',
      logo: require('../../assets/images/logo.png'),
      home: '/home',
      login: '',
      version:'1.0.3',
      copyright: '版权所有：陈曦·LR | 用代码改变世界 Powered by EasyModular' //版权声明}
    }
    return { config }
  }

  return {
    ...crud,
    get
  }
}
