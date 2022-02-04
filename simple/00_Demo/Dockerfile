#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#添加.net6基础镜像
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

#容器工作目录
WORKDIR /app

#暴露docker容器端口
EXPOSE 8025

#拷贝发布目录下的所有文件到容器的工作根目录
COPY . .

#容器入口点
ENTRYPOINT ["dotnet", "Demo.WebHost.dll"]