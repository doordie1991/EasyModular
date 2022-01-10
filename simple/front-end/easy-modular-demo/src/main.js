import Vue from 'vue'
import EasyModularUI from 'easy-modular-ui'

import app from './app'
import global from './global'

//注入模块
const context = require.context('./_modules', true, /.main.js$/)
context.keys().forEach((m) => {
  app.modules.push(context(m).default)
})

//获取系统配置
const config = $api.admin.system.get()
Object.assign(app.config, config)

//注入应用基本接口
app.actions = {
  //身份认证相关方法
  auth: $api.admin.auth,
  //皮肤保存
  saveSkin: $api.admin.skin.save,
  //获取发布记录
  getreleaseLog: $api.admin.releaseLog.queryAll
}

 EasyModularUI.use(app)


