
<div align=center>
	<img src="https://raw.githubusercontent.com/doordie1991/PicBed/main/picture/20210222012759.png" width="400" />
</div>


## EasyModular是基于NeCore3.1的简易模块化框架，开箱即用，1分钟即可以搭建一个简单可用的模块化框架

![20210222002554](https://raw.githubusercontent.com/doordie1991/PicBed/main/picture/20210222002554.png)

## 安装使用
| 组件 | Nuget | 是否必须 |
| :----:| :----: | :----: |
| 模块化核心库 | Install-Package EasyModular | √ |
| 接口文档 | Install-Package EasyModular.Swagger | × |
| ORM | Install-Package EasyModular.SqlSugar | × |
| 模型验证 | Install-Package EasyModular.FluentValidation | × |
| 身份验证 | Install-Package EasyModular.Auth<br> Install-Package EasyModular.Jwt| × |
| 日志 | Install-Package EasyModular.Serilog | × |
| 缓存 | Install-Package EasyModular.Cache | × |
| 工具类库 | Install-Package EasyModular.Utils | × |


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
