const express = require('express');
const router = express.Router();
const Assess = require('../models/assess');  // 引入留言模型
const Group = require('../models/group');    // 假设你已经定义了 Group 模型

// 发送留言
router.post('/send', async (req, res) => {
  try {
    const { note_text, senderID, groupID, backID } = req.body;

    // 检查是否存在该群聊
    const group = await Group.findById(groupID);
    if (!group) {
      return res.status(404).json({ error: '群聊不存在' });
    }

    // 创建新留言
    const newAssess = new Assess({
      note_text,  // 留言内容
      senderID,   // 发送者的用户ID
      groupID,    // 留言所属的群聊ID
      backID      // 如果是回复留言，传入被回复留言的ID，否则为null
    });

    // 保存留言到数据库
    await newAssess.save();

    res.status(201).json({
      message: '留言成功',
      assess: newAssess
    });
  } catch (error) {
    res.status(400).json({ error: error.message });
  }
});

// 获取某个群聊的所有留言
router.get('/group/:groupID', async (req, res) => {
  try {
    const { groupID } = req.params;

    // 获取指定群聊的所有留言，按时间倒序排列
    const groupAssess = await Assess.find({ groupID })
                                    .sort({ createdAt: -1 })
                                    .populate('senderID', 'username')  // 使用 populate 来获取发送者的用户名
                                    .populate('backID');  // 可选，如果有回复，也可以填充回复内容

    res.status(200).json(groupAssess);
  } catch (error) {
    res.status(400).json({ error: error.message });
  }
});

// 获取某个留言的所有回复
router.get('/:noteID/replies', async (req, res) => {
  try {
    const { noteID } = req.params;

    // 获取所有回复某个留言的留言，使用 backID 字段查找
    const replies = await Assess.find({ backID: noteID })
                                .populate('senderID', 'username')  // 填充发送者信息
                                .populate('backID');  // 可选，填充回复内容

    res.status(200).json(replies);
  } catch (error) {
    res.status(400).json({ error: error.message });
  }
});

// 点赞留言
router.post('/:noteID/like', async (req, res) => {
  try {
    const { noteID } = req.params;

    // 找到留言并增加点赞数
    const assess = await Assess.findById(noteID);

    if (!assess) {
      return res.status(404).json({ error: '留言不存在' });
    }

    assess.likes += 1;  // 点赞数增加1

    // 保存点赞数的变化
    await assess.save();

    res.status(200).json({
      message: '点赞成功',
      likes: assess.likes
    });
  } catch (error) {
    res.status(400).json({ error: error.message });
  }
});

module.exports = router;
