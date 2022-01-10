
let apis = {}
const requireComponent = require.context('./service', true, /\.*\.js$/)
requireComponent.keys().map(fileName => {
  const name = fileName.replace('./', '').replace('.js', '')
  const func = requireComponent(fileName).default
  apis[name] = func(name)
})

$api['scheduling'] = apis
