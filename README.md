# SuPet

由于本人眼神不好，总是一眼找不到想打开的快捷方式。

所以写一个小程序将常用的软件及文件夹集中起来并分好类。

[![SuPet](https://img.shields.io/badge/SuPet-Alpha%20v0.1.0-green)](https://github.com/Sener1998/SuPet)

---

## 使用说明

## 1. 准备界面Gif

1. 准备一张喜欢的Gif图片，比如下面这只皮卡丘：

![Pikachu.gif](https://raw.githubusercontent.com/Sener1998/SuPet/master/MainProj/Resources/Pikachu.gif)

2. 位置随意，建议放在“SuPet.exe”附近

### 2. 编写配置文件

1. 新建配置文件
   
   在“SuPet.exe”的同一目录下创建配置文件，命名为“SuPetConfig.toml”。
   本程序使用的配置文件为toml格式，如有必要需自行学习toml相关语法。

2. 配置界面Gif

   可参考下面所给demo配置。由于目前只需配置界面Gif，只有一张图片，故只需要一项“[[ImageGroup.image]]”，将path属性设置为准备的Gif相对于“SuPet.exe”的位置。

   ```
   # 图像组，将图像分组管理
   [[ImageGroup]]
   name = "InitImage"   # 名字，一般将用途作为图像组的名字
   defaultDir = ""      # 默认图像所在的目录，默认目录的本质是将后续图片的path属性前面加上统一的前缀。
   isAll = false        # 若为true则会将默认目录下的所有图片加入本图像组，若为false则会将下面所列图片添加到图像组
   # 指定下面图片加入图像组
   [[ImageGroup.image]]             # 第一张图片
   path = "Resources\\Pikachu.gif"  # 图片位置，与主程序的相对位置
   [[ImageGroup.image]]             # 第二张图片
   path = "Resources\\xxx.gif"
   ```

3. 配置右键菜单栏（设置快速启动程序）
   
   可参考下面所给demo编写自己的配置。

   ```
   # Menu意义为右键菜单栏的某一项
   # Menu可嵌套Menu，叶子Menu必须设置为要打开的程序或文件夹
   [[Menu]]         # 右键菜单栏的第一项
   name = "Game"    # 名字，所有Menu的name必须不同
   text = "游戏"     # 在菜单中显示的文本
   isNeedAP = false # 是否需要管理员权限，非叶子Menu的该项随意设置
   program = ""     # 默认执行程序，非叶子Menu的该项随意设置
   path = ""        # 位置，非叶子Menu的该项随意设置
   [[Menu.Menu]]    # 这是上一级Menu的子Menu，该Menu没有子Menu，故为叶子Menu
   name = "Steam"
   text = "Steam"
   isNeedAP = false                     # 是否需要以管理员身份打开
   program = ""                         # 若program为""，则path必须为可执行文件
   path = "E:\\Games\\Steam\\Steam.exe" # 可执行文件位置

   [[Menu]]         # 右键菜单栏的第二项
   name = "Notes"
   text = "笔记"
   isNeedAP = false
   program = ""
   path = ""
   [[Menu.Menu]]
   name = "SuNotes"
   text = "SuNotes"
   isNeedAP = false
   program = "C:\\Program Files\\Microsoft Office\\root\\Office16\\ONENOTE.EXE" # 以指定程序打开path指定的文件
   path = "D:\\SuNotes\\打开笔记本.onetoc2"     # 文件位置
   [[Menu.Menu]]
   name = "OldNotes"
   text = "Notes"
   isNeedAP = false
   program = "Explorer.exe" # 使用资源管理器打开path指定的文件夹
   path = "D:\\Notes"       # 文件夹位置
   # [[Menu.Menu.Menu]]也可以使用，看个人需求是否需要添加
   ```
### 3. 启动程序

双击“SuPet.exe”即可。

### 4. 日志记录

程序第一次关闭后会在“SuPet.exe”所在目录下生成“WindowsConfig.log”文件。该文件记录了程序关闭时窗口的位置，下次打开程序时后以相同的位置启动。若该文件被删除，窗口会出现在屏幕中央。

---

## 注意事项

SuPet由C#编写，运行依赖于.NET 6.0。若不满足使用条件则需自行下载源码编译。

---

## License

MIT © Richard McRichface
