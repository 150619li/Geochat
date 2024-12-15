const express = require('express');
const Group = require('../models/Group');
const Message = require('../models/message');
const User = require('../models/user');
const router = express.Router();

// 创建群聊
router.post('/create', async (req, res) => {
  try {
    const { group_name, memberID, lat, lon, radius } = req.body;
    // 验证用户ID是否有效
    const validMembers = await User.find({ _id: { $in: memberID } });
    if (validMembers.length !== memberID.length) {
      return res.status(400).send('One or more user IDs are invalid');
    }
    const newGroup = new Group({ groupID: new mongoose.Types.ObjectId(), group_name, memberID, lat, lon, radius });
    await newGroup.save();
    res.status(201).send('Group created successfully');
  } catch (error) {
    res.status(400).send(error.message);
  }
});

// 添加群成员
router.post('/:groupId/addMember', async (req, res) => {
  try {
    const { groupId } = req.params;
    const { memberID } = req.body;
    const group = await Group.findById(groupId);
    if (!group) {
      return res.status(404).send('Group not found');
    }
    const user = await User.findById(memberID);
    if (!user) {
      return res.status(404).send('User not found');
    }
    group.memberID.push(memberID);
    await group.save();
    res.status(200).send('Member added successfully');
  } catch (error) {
    res.status(400).send(error.message);
  }
});

// 剔除群成员
router.post('/:groupId/removeMember', async (req, res) => {
  try {
    const { groupId } = req.params;
    const { memberID } = req.body;
    const group = await Group.findById(groupId);
    if (!group) {
      return res.status(404).send('Group not found');
    }
    group.memberID = group.memberID.filter(id => id.toString() !== memberID);
    await group.save();
    res.status(200).send('Member removed successfully');
  } catch (error) {
    res.status(400).send(error.message);
  }
});

// 获取群聊消息
router.get('/:groupId/messages', async (req, res) => {
  try {
    const { groupId } = req.params;
    const group = await Group.findById(groupId).populate('messages');
    if (!group) {
      return res.status(404).send('Group not found');
    }
    res.status(200).json(group.messages);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

// 发送消息
router.post('/:groupId/messages', async (req, res) => {
  try {
    const { groupId } = req.params;
    const { userID_send, message_text } = req.body;
    // 验证用户ID
    const user = await User.findById(userID_send);
    if (!user) {
      return res.status(400).send('Invalid user ID');
    }

    // 验证群聊ID
    const group = await Group.findById(groupId);
    if (!group) {
      return res.status(400).send('Invalid group ID');
    }

    const newMessage = new Message({ groupID: groupId, userID_send, message_text });
    await newMessage.save();

    // 将消息ID添加到群聊的消息列表中
    group.messages.push(newMessage._id);
    await group.save();

    res.status(201).send('Message sent successfully');
  } catch (error) {
    res.status(400).send(error.message);
  }
});

// 获取群聊经纬度和半径
router.get('/:groupId/location', async (req, res) => {
  try {
    const { groupId } = req.params;
    const group = await Group.findById(groupId);
    if (!group) {
      return res.status(404).send('Group not found');
    }
    const { lat, lon, radius } = group;
    res.status(200).json({ lat, lon, radius });
  } catch (error) {
    res.status(400).send(error.message);
  }
});

module.exports = router;