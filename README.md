# dotnetGen_sqlserver
.NETCore + SqlServer 生成器

开发初期，它可以选择数据库表创建项目；

开发阶段，它可以即时更新项目中数据层 SDK，提供业务开发基础；

优势：
 * 1、根据主键、唯一键、外键（1对1，1对多，多对多）生成功能丰富的数据库 SDK；
 * 2、避免随意创建表，严格把控数据库，有标准的ER图；
 * 3、统一规范数据库操作类与方法，一条心堆业务；

部署：
 * 运行环境：.net 2.0+
 * 安装 ServerWinService 服务，启动前设置服务端口
 * 配置 MakeCode 服务器，向开发者发送 MakeCode 生成器

共勉，一起学习QQ群：8578575

-----------------

| <font color=gray>功能对比</font> | [dotnetGen](https://github.com/2881099/dotnetGen) | [dotnetGen_sqlserver](https://github.com/2881099/dotnetGen_sqlserver) | [dotnetGen_mysql](https://github.com/2881099/dotnetGen_mysql) | [dotnetGen_postgresql](https://github.com/2881099/dotnetGen_postgresql) |
| ----------------: | -------------:| --------------------:| --------------: | -------------------: |
| windows            | √ | √ | √ | √ |
| linux              | - | - | √ | √ |
| 连接池             | √ | √ | √ | √ |
| 事务               | √ | √ | √ | √ |
| 表                 | √ | √ | √ | √ |
| 表关系(1对1)        | √ | √ | √ | √ |
| 表关系(1对多)       | √ | √ | √ | √ |
| 表关系(多对多)      | √ | √ | √ | √ |
| 表主键             | √ | √ | √ | √ |
| 表唯一键           | √ | √ | √ | √ |
| 存储过程           | √ | - | - | - |
| 视图               | - | - | - | √ |
| 类型映射           | √ | √ | √ | √ |
| 枚举               | - | - | √ | √ |
| 自定义类型         | - | - | - | √ |
| 数组               | - | - | - | √ |
| xml               | - | - | - | - |
| json              | - | - | - | √ |
| 命令行生成         | - | - | - | √ |
| RESTful           | - | - | √ | √ |
| 后台管理功能       | √ | - | √ | √ |