# ThirdPersonScript
源文件链接：（链接: https://caiyun.139.com/m/i?125CdlXCL50wF  提取码:eULL  复制内容打开中国移动云盘手机APP，操作更方便哦）
演示视频（过审ing)
  一个第三人称的动作游戏，采用Manager Of Managers的框架设计，使用单例模式管理游戏的各个系统。玩家和敌人的行为采用逻辑和表现分开的编程模式，FSM状态机模式实现逻辑，表现由继承CoreComponent的脚本实现。UI系统采用单例模式管理，UIPage脚本负责选择页面切换的动画和音效效果，并使用协程实现UI动画和音效播放。任务系统实现，将特定任务的所有静态信息存储在QuestInfoS0中并继承ScriptableObject，实现数据化存储，由Quest管理任务的进度，QuestManager获取和管理所有任务。
事件中心，观察者模式实现事件中心，使用Enum类型标识各个事件.战斗系统，攻击的静态信息以ScriptableObject存取,并封装不同的攻击系统，比如玩家的连击，敌人不同的攻击方式。基于Ink语言实现的有选项对话系统.
