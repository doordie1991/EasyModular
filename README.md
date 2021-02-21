![20210221155650](https://raw.githubusercontent.com/doordie1991/PicBed/main/picture/20210221155650.png)
## EasyModular
EasyModular是基于NeCore3.1的简易模块化框架，开箱即用，1分钟既可以搭建一个简单可用的模块化框架。

## 安装使用
**安装核心组件**：Install-Package EasyModular

EasyModular提供了很多实用的基础组件，可以按需去安装，如：管理接口文档的组件：EasyModular.Swagger，基于SqlSugar的模块化ORM组件：EasyModular.SqlSugar等等。

**Startup.cs 添加组件服务**
![20210221162959](https://raw.githubusercontent.com/doordie1991/PicBed/main/picture/20210221162959.png)

## 模块的构成
项目实例使用的是基于DDD领域驱动设计分层（用得不好，伪DDD:joy:），当然你也可以使用你习惯的分层架构。
![20210221164059](https://raw.githubusercontent.com/doordie1991/PicBed/main/picture/20210221164059.png)

## 如何管理模块
最简单粗暴的方式当然是直接添加模块项目引用把多个模块集成起来。项目模块比较多时，推荐使用Nuget去管理和集成模块。使用Nexus搭建Nuget私服（真的很方便:sunglasses:）。

## 官方文档
还在写.....:joy:

## 官方Demo
还在写.....:joy:








