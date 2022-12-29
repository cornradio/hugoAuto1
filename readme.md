# hugoAuto1
使用这个辅助软件，你可以方便的新建博客、本地编译查看、修改和上传！

不要因为写博客启动成本太高就不写啦！`hugoAuto1` 来帮你完成繁琐操作，你就专心写就成！

# 特性
- 快速打开source、output、Article目录
- 新建、写文章、本地预览、编译，一气呵成！
- 拉取与上传到github（支持proxy）
- 使用`combobox`快速的选择和修改历史文章。
- hugo下载网站链接，方便在新电脑上快速布置hugo环境。
- 可移植的配置文件
- 可设置的程序启动目录，自定义自己喜欢的浏览器、编辑器

# 使用简介

首次启动，程序会加载默认配置，包括一些我设定的本地hugo目录、git命令等，有一定的参考意义，可以根据自己的情况简单修改后使用。
- 源文件目录
- 编译后文件目录
- 文章目录

![image](https://user-images.githubusercontent.com/119606491/209915367-6ead5d58-f132-4084-846a-72798a7b9791.png)

在settings中可以修改程序位置、git命令等内容。
> 可以一键替换用户名为自己的用户名，可以避免手动配置一些内容（例如如程序目录，这些程序目录都是默认安装位置）

![image](https://user-images.githubusercontent.com/119606491/209915458-f458ca05-97ea-4833-b51d-603363b591aa.png)

# 截图

<!-- ![image-20211114205423433](https://tvax3.sinaimg.cn/large/006rgJELly1gwez3p67lbj30bo089mzm.jpg)
软件主要就是通过后台运行命令的方式来实现各类操作，所以针对不同的hugo主题，生成新文章【new】按钮的功能可以需要按照自己的需求打命令，以及有人自定义在D盘安装typora\chrome\github需要更改一下源码才能正常使用。

![image](https://user-images.githubusercontent.com/49443405/169944918-af35aa53-a7e3-4e08-b4ed-8c176b1cd922.png)
![image](https://user-images.githubusercontent.com/49443405/169944946-fae1577f-1fa2-4f57-a9b4-f1e18e7e0d89.png)
![image](https://user-images.githubusercontent.com/49443405/169944977-c35f1555-bee5-49f0-88d9-53b90ce458c6.png)
 -->
 
# 实现要点
- 主要依靠启动命令行并执行指定参数来完成大部分操作
- 通过`default.settings`来存储路径数据、程序位置、git命令等
- 完善设计的界面，好看！
- 
# 未来计划
- 对文章的查询，支持按照时间日期选择等操作
- 完善程序文档
