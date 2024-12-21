const express = require('express');
const mongoose = require('mongoose');
const router = express.Router();
const Message = require('../models/message');
const User = require('../models/user');
const Group = require('../models/Group');

// 发送消息
router.post('/send', async (req, res) => {
  try {
    const { message_text, userID_send, groupID } = req.body;

    // 验证 userID_send 是否有效
    const user = await User.findById(userID_send);
    if (!user) {
      return res.status(404).send('User not found');
    }

    // 验证 groupID 是否有效
    const group = await Group.findById(groupID);
    if (!group) {
      return res.status(404).send('Group not found');
    }

    const newMessage = new Message({
      messageID: new mongoose.Types.ObjectId(),
      message_text,
      userID_send,
      groupID,
      message_time: new Date()
    });

    await newMessage.save();

    // 将消息添加到群聊中
    group.messages.push(newMessage._id);
    await group.save();

    res.status(201).send('Message sent successfully');
  } catch (error) {
    res.status(400).send(error.message);
  }
});

// 获取所有消息
router.get('/all', async (req, res) => {
  try {
    const messages = await Message.find().populate('sender');
    res.status(200).json(messages);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

module.exports = router;