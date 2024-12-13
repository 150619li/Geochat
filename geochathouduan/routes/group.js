const express = require('express');
const Group = require('../models/group');
const Message = require('../models/message');
const User = require('../models/user');
const router = express.Router();

// 创建群聊
router.post('/create', async (req, res) => {
  try {
    const { name, members } = req.body;
    // 验证用户ID是否有效
    const validMembers = await User.find({ _id: { $in: members } });
    if (validMembers.length !== members.length) {
      return res.status(400).send('One or more user IDs are invalid');
    }
    const newGroup = new Group({ name, members });
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
    const { memberId } = req.body;

    //console.log(`Adding member ${memberId} to group ${groupId}`);

    // 验证用户ID是否有效
    const user = await User.findById(memberId);
    if (!user) {
      return res.status(400).send('Invalid user ID');
    }

    // 更新群聊成员列表
    const group = await Group.findByIdAndUpdate(
      groupId,
      { $addToSet: { members: memberId } },
      { new: true }
    );
    if (!group) {
      //console.log(`Group not found: ${groupId}`);
      return res.status(404).send('Group not found');
    }
    //console.log(`Member ${memberId} added to group ${groupId} successfully`);
    res.status(200).send('Member added successfully');
  } catch (error) {
    //console.error('Error adding member:', error);
    res.status(400).send(error.message);
  }
});

// 剔除群成员
router.post('/:groupId/removeMember', async (req, res) => {
  try {
    const { groupId } = req.params;
    const { memberId } = req.body;

    // 验证用户ID是否有效
    const user = await User.findById(memberId);
    if (!user) {
      return res.status(400).send('Invalid user ID');
    }

    // 更新群聊成员列表
    const group = await Group.findByIdAndUpdate(
      groupId,
      { $pull: { members: memberId } },
      { new: true }
    );
    if (!group) {
      return res.status(404).send('Group not found');
    }

    res.status(200).send('Member removed successfully');
  } catch (error) {
    res.status(400).send(error.message);
  }
});

// 获取群聊消息
router.get('/:groupId/messages', async (req, res) => {
  try {
    const { groupId } = req.params;
    const group = await Group.findById(groupId).populate({
      path: 'messages',
      populate: { path: 'sender', select: 'username' }
    });
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
    const { sender, content } = req.body;
    
    // 验证用户ID
    const user = await User.findById(sender);
    if (!user) {
      return res.status(400).send('Invalid user ID');
    }

    // 验证群聊ID
    const group = await Group.findById(groupId);
    if (!group) {
      return res.status(400).send('Invalid group ID');
    }

    const newMessage = new Message({ group: groupId, sender, content });
    await newMessage.save();

    // 将消息ID添加到群聊的消息列表中
    group.messages.push(newMessage._id);
    await group.save();
    
    res.status(201).send('Message sent successfully');
  } catch (error) {
    res.status(400).send(error.message);
  }
});

module.exports = router;