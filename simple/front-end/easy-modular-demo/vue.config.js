const path = require('path')
const CopyWebpackPlugin = require('copy-webpack-plugin')
const HtmlWebpackPlugin = require('html-webpack-plugin')
// 开发环境
const isDev = process.env.NODE_ENV === 'development'
// 打包输出路径
const outputDir = './dist'
const app = require('./src/app')
const title = app.config.title

// 增加环境变量
process.env.VUE_APP_COPYRIGHT = app.config.copyright
process.env.VUE_APP_BUILD_TIME = require('dayjs')().format('YYYYMDHHmmss')

module.exports = {
  lintOnSave: false, //关闭eslint 验证
  outputDir: outputDir,
  publicPath: '/',
  devServer: {
    port: 5220
  },
  transpileDependencies: ['easy-modular-*', 'element-ui'],
  configureWebpack() {
    let config = {
      plugins: [
        //复制easy-modular-ui/public目录下的文件到输出目录
        new CopyWebpackPlugin([
          {
            from: path.join(__dirname, 'node_modules/easy-modular-ui/public'),
            to: path.join(__dirname, outputDir)
          }
        ]),
        //设置index.html模板路径，使用easy-modular-ui/public中的模板
        new HtmlWebpackPlugin({
          title: title,
          filename: 'index.html',
          template: './node_modules/easy-modular-ui/public/index.html',
          inject: true
        })
      ]
    }

    if (isDev) {
      config.devtool = 'source-map'
    }

    return config
  }
}
