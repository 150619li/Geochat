const express = require('express');
const mongoose = require('mongoose');
const userRouter = require('./routes/user');
const groupRouter = require('./routes/group');
const messageRouter = require('./routes/message');
const assessRouter = require('./routes/assess');
const path = require('path');
require('dotenv').config();

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

// 启动服务器
app.listen(port, () => {
  console.log(`Server is running on port ${port}`);
});