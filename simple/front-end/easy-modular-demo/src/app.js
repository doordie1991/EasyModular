module.exports = {
  //应用配置
  config: {
    //应用名称
    title: "权限管理系统",
    //应用logo
    logo: "./images/logo.png",
    //首页
    home: "/home",
    //版本号
    version: "1.0.6",
    //版权
    copyright: "版权所有：陈曦·LR | 大龄落魄中年大叔 Powered by EasyModular"
  },
  //接口
  actions: {},
  //模块
  modules: [],
  //服务地址
  serviceUrl: process.env.NODE_ENV !== "production" ? "http://localhost:8025/" : "http://47.119.173.122/"
}
