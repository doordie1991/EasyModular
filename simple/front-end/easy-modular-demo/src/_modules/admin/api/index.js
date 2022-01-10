
window.$api = {} // 注册接口列表为window全局属性
let apis = {}
const requireComponent = require.context('./service', true, /\.*\.js$/)
requireComponent.keys().map(fileName => {
  const name = fileName.replace('./', '').replace('.js', '')
  const func = requireComponent(fileName).default
  apis[name] = func(name)
})

$api['admin'] = apis
