const express = require('express');
const mongoose = require('mongoose');
const userRouter = require('./routes/user');
const groupRouter = require('./routes/group');
const messageRouter = require('./routes/message');
const assessRouter = require('./routes/assess');
const path = require('path');
require('dotenv').config();
const os = require('os');

const app = express();
const port = process.env.PORT || 5000;

// 中间件
app.use(express.json());
app.use(express.static(path.join(__dirname, 'public')));

// 连接MongoDB
mongoose.connect(process.env.MONGODB_URI)
  .then(() => console.log('Connected to MongoDB'))
  .catch(err => console.error('Could not connect to MongoDB', err));

// 路由
app.use('/api/users', userRouter);
app.use('/api/groups', groupRouter);
app.use('/api/messages', messageRouter);
app.use('/api/assess', assessRouter);


function findWiFiIPv4() {
  const interfaces = os.networkInterfaces();
  for (const interfaceName in interfaces) {
      // 假设 Wi-Fi 接口的名称包含 "Wi-Fi" 或 "wlan" 关键字，不同操作系统可能不同
      if (interfaceName.toLowerCase().includes('wi-fi') || interfaceName.toLowerCase().includes('wlan')) {
          const interfaceData = interfaces[interfaceName];
          for (const data of interfaceData) {
              // 查找 IPv4 地址，排除环回地址和内部网络地址
              if (data.family === 'IPv4' &&!data.internal) {
                  return data.address;
              }
          }
      }
  }
  return '127.0.0.1';
}
// 启动服务器
app.listen(port,findWiFiIPv4(), () => {
  console.log(`Server is running on port ${findWiFiIPv4()}:${port}`);
});