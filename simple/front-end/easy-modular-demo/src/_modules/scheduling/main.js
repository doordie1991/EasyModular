import './api'
import store from './store'
import routes from './routes'
export default {
  module: {
    name: 'em-module-scheduling',
    code: 'scheduling',
    version: '1.0.0',
    description: '任务调度'
  },
  routes,
  store,
  components: []
}
