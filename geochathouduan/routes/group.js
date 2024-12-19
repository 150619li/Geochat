const express = require('express');
const mongoose = require('mongoose');
const Group = require('../models/Group');
const Message = require('../models/message');
const User = require('../models/user');
const router = express.Router();

// 创建群聊
router.post('/create', async (req, res) => {
  try {
    const { group_name, memberID, lat, lon, radius } = req.body;

     // 验证 group_name 是否为空
     if (!group_name) {
      return res.status(400).send('Group name is required');
    }

     // 检查 group_name 是否已经存在
     const existingGroup = await Group.findOne({ group_name });
     if (existingGroup) {
       return res.status(400).send('Group name already exists');
     }
     
    // 验证 memberID 是否为数组
    if (!Array.isArray(memberID)) {
      return res.status(400).send('memberID should be an array');
    }

    // 验证用户ID是否有效
    const validMembers = await User.find({ _id: { $in: memberID } });
    if (validMembers.length !== memberID.length) {
      return res.status(400).send('One or more user IDs are invalid');
    }

    const newGroup = new Group({ 
      groupID: new mongoose.Types.ObjectId(), 
      group_name, 
      memberID, 
      lat, 
      lon, 
      radius 
    });

    await newGroup.save();
    console.log('Group created successfully');
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
    user.user_in_groups.push(groupId);
    await user.save();
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

    // 验证 memberID 是否为单一的 ObjectId
    if (!mongoose.Types.ObjectId.isValid(memberID)) {
      return res.status(400).send('Invalid memberID');
    }

    const group = await Group.findById(groupId);
    if (!group) {
      return res.status(404).send('Group not found');
    }
    
    const user = await User.findById(memberID);
    if (!user) {
      return res.status(404).send('User not found');
    }

    group.memberID = group.memberID.filter(id => id.toString() !== memberID);
    await group.save();

    // 从用户的 user_in_groups 中移除群聊
    user.user_in_groups = user.user_in_groups.filter(id => id.toString() !== groupId);
    await user.save();
    
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