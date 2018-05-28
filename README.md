# V2Switcher
极简V2Ray配置切换器

V2Ray的Windows客户端实在是太糟糕了，直接用Core还不方便，尤其是你有很多线路的时候。

所以我写了这个切换器方便自己也方便各位使用多条线路的dalao使用。

![运行效果](https://i.loli.net/2017/11/12/5a07f18482120.jpg)

## 开发环境
VisualStudio 2017 + .Net Framework 4.5.2

## 如何运行
如果你是Windows10用户可以下载已编译好的版本，如果是其他平台用户安装.Net4.5.2后应该也可以使用已编译版本。
其他平台可以自己重新编译合适的版本。

确保你的文件夹下有如下文件：
1. V2Switcher.exe(程序本体)
2. Newtonsoft.Json.dll(JSON格式支持库)
3. v2ray.exe或wv2ray.exe
4. 至少一个v2ray的配置文件json

![一个典型的例子](https://i.loli.net/2017/11/12/5a07f31c3a0fc.jpg)

## 补充说明
- 右键菜单中的配置名就是配置文件的文件名
- 程序会读取Inbound中的Port组合成设置代理的字符串，务必保持该项存在。
- ~~目前只支持V2Ray-Core开放出来的http代理设置，不支持socks。~~
- ~~个人建议保持所有配置项的Inbound.Port一致，实测Chrome等浏览器换端口的话都不是实时生效，要卡一阵才能切到新代理地址。~~
- 代理不能实时切换的问题已修复，增加了系统代理配置的刷新操作。
- 本程序同时支持无界面的wv2ray和命令行窗口的v2ray，有wv2ray的情况下会优先使用无窗口的wv2ray

