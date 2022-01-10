import Vue from 'vue'
import app from '../app'

//服务地址
Vue.prototype.$baseUrl = app.serviceUrl
//空的GUID
Vue.prototype.$emptyGuid = '00000000000000000000000000000000'