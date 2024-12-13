const express = require('express');
const router = express.Router();
const Message = require('../models/message');

// 发送消息
router.post('/send', async (req, res) => {
  try {
    const { sender, content } = req.body;
    const newMessage = new Message({ sender, content });
    await newMessage.save();
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