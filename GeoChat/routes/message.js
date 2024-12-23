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

//获取私聊信息
router.get('/private/:userId/:friendId', async (req, res) => {
  const { userId, friendId } = req.params;

  try {
    // 获取指定用户和朋友之间的消息
    const messages = await Message.find({
      $or: [
        { userID_send: userId, userID_receive: friendId },
        { userID_send: friendId, userID_receive: userId }
      ]
    }).sort({ message_time: 1 }); // 按时间升序排列

    res.json(messages); // 返回消息列表
  } catch (error) {
    res.status(500).send(error.message);
  }
});

// 发送私聊消息
router.post('/private/sendPrivate', async (req, res) => {
  const { message_text, sender_id, receiver_id } = req.body;

  try {
    const newMessage = new Message({
      messageID: new mongoose.Types.ObjectId(),
      message_text,
      userID_send: sender_id,
      userID_receive: receiver_id,
      message_time: new Date()
    });

    await newMessage.save();
    res.status(201).send('Message sent');
  } catch (error) {
    res.status(500).send(error.message);
  }
});

module.exports = router;