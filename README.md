# Geochat
## 软件工程课程作业

**目标**：实现一个安卓手机端的应用APP，后续将在Android Studio开发。应用是一个基于地理位置的聊天室应用，希望实现的具体功能如下:通过用户的实时位置，自动将其加入附近的群聊或活动群组。例如，当进入某个区域(如图书馆、教学楼、食堂等)，自动加入该区域相关的群组，讨论该区域的事件或活动。

**考虑以下的工程体系架构和文件结构**：这个架构将包括前端和后端的设计，确保应用能够处理用户的实时位置并自动加入相关的群组。（待讨论）

### 工程体系架构

1. **前端（Android 应用）**
   - 使用 Kotlin 或 Java 开发 Android 应用。
   - Firebase Realtime Database 实现实时聊天功能。

2. **后端**
   - 使用 Node.js 或 Python（Flask/Django）作为后端服务。
   - 使用数据库Firebase存储用户信息、群组信息和聊天记录。
   - 实现位置服务逻辑，自动将用户加入相应的群组。

3. **位置服务**
   - 使用地理围栏（Geofencing）技术来监测用户进入特定区域。
   - 处理用户位置更新并根据位置更新群组信息。

### 文件结构

以下是一个可能的文件结构示例：

```
/ChatApp
├── /app
│   ├── /src
│   │   ├── /main
│   │   │   ├── /java
│   │   │   │   └── com.example.chatapp
│   │   │   │       ├── MainActivity.kt
│   │   │   │       ├── ChatActivity.kt
│   │   │   │       ├── LocationService.kt
│   │   │   │       └── GroupChatAdapter.kt
│   │   │   ├── /res
│   │   │   │   ├── /layout
│   │   │   │   │   ├── activity_main.xml
│   │   │   │   │   └── activity_chat.xml
│   │   │   │   ├── /values
│   │   │   │   │   └── strings.xml
│   │   │   │   └── /drawable
│   │   │   │       └── ic_launcher.png
│   │   │   └── AndroidManifest.xml
├── /backend
│   ├── /server
│   │   ├── app.js
│   │   ├── routes
│   │   │   └── chat.js
│   │   ├── models
│   │   │   └── User.js
│   │   └── config
│   │       └── db.js
└── README.md
```

### 关键文件说明

- **前端**
  - `MainActivity.kt`: 应用的主界面，负责初始化和导航。
  - `ChatActivity.kt`: 聊天界面，显示聊天记录和发送消息。
  - `LocationService.kt`: 处理用户位置更新和地理围栏逻辑。
  - `GroupChatAdapter.kt`: 适配器，用于显示群聊消息。

- **后端**
  - `app.js`: 后端应用的入口文件，设置服务器和中间件。
  - `chat.js`: 处理聊天相关的路由。
  - `User.js`: 用户模型，定义用户数据结构。
  - `db.js`: 数据库配置和连接。
