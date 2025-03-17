# 界之轨迹修改工具(.Net8)

一个用于修改`.tbl`文件及反编译`.dat`文件的工具（暂不支持重新编译，请配合下方推荐工具使用）

## 如何使用？

1. 运行`KnKModTools.exe`，首次启动需要进行以下设置：
   - 设置游戏安装目录
   - 设置输出目录
   - （其他必要设置项）

   ![首次启动界面](./KnKModTools/Image/firstrun.png)

2. 设置完成后等待程序加载完成

   ![加载界面](./KnKModTools/Image/load.png)

3. 当下方窗口显示操作界面后，即可开始编辑

   ![编辑界面](./KnKModTools/Image/edit.png)

- 1:tbl文件列表
- 2:节点列表
- 3:详细数据表
- 4:表头，点击后正序排序，再次点击倒序排序，切换节点即可还原顺序
- 5:全部保存
- 6:反编译dat脚本，反编译后的脚本存放在脚本输出目录
- PS:快捷键
   - 左Ctrl + S - 保存当前浏览的表
   - 数据表选中某一行 + Ins - 以该行为模板添加项
   - 数据表选中某一行 + Del - 删除该项
## 已知问题

- 数据表有些带有换行的长文本显示不全，修改时可复制出来，修改完成后再复制回去
- 暂未实现查找功能，但可通过排序功能快速定位条目
- 源文件字节已对齐，但保存功能未实现对齐处理（如发现游戏运行明显变慢请反馈）
- 部分脚本文件可能无法正确反编译（未处理不可规约流图，可忽略）
- 反编译生成的`.js`文件可能不完整（仅处理可预料情况，但多数结果可信）
- 当前版本暂不支持重新编译功能

## 推荐工具

- [KuroTools](https://github.com/nnguyen259/KuroTools) - 可用于修改脚本文件
- [kuro_mdl_tool](https://github.com/eArmada8/kuro_mdl_tool) - 模型处理工具
- [kuro_dlc_tool](https://github.com/eArmada8/kuro_dlc_tool) - DLC处理工具

## 社区支持

- Discord交流群组: [https://discord.gg/QdEYC6Cd](https://discord.gg/QdEYC6Cd)
- 轨迹系列中文社区: [茶会](https://trails-game.com/)

---

> 注意：本工具生成的反编译文件仅供参考，建议配合KuroTools进行脚本修改。如遇技术问题，欢迎通过Discord社区交流讨论。
