const express = require('express');
const bcrypt = require('bcrypt');
const jwt = require('jsonwebtoken');
const mongoose = require('mongoose');
const User = require('../models/user');
const router = express.Router();

// 用户注册路由
router.post('/register', async (req, res) => {
  try {
    const { username, email, password, lat, lon  } = req.body;
    const hashedPassword = await bcrypt.hash(password, 10); // 加密密码
    const newUser = new User({ 
      userID: new mongoose.Types.ObjectId(),
      username,
      email,
      password: hashedPassword,
      poi: { lat, lon }
    });
    await newUser.save();
    res.status(201).send('User registered successfully');
  } catch (error) {
    res.status(400).send(error.message);
  }
});

// 用户登录
router.post('/login', async (req, res) => {
  try {
    const { email, password } = req.body;
    const user = await User.findOne({ email });
    if (!user || !await bcrypt.compare(password, user.password)) {
      return res.status(401).send('Invalid email or password');
    }
    const token = jwt.sign({ userId: user._id }, 'your_jwt_secret');
    res.status(200).json({ token });
  } catch (error) {
    res.status(400).send(error.message);
  }
});

// 上传头像
router.post('/uploadProfile', async (req, res) => {
  try {
    const { userId, profileUrl } = req.body;
    const user = await User.findById(userId);
    if (!user) {
      return res.status(404).send('User not found');
    }
    user.profile = profileUrl;
    await user.save();
    res.status(200).send('Profile updated successfully');
  } catch (error) {
    res.status(400).send(error.message);
  }
});

// 设置为管理员
router.post('/setAdmin', async (req, res) => {
  try {
    const { userId } = req.body;
    const user = await User.findById(userId);
    if (!user) {
      return res.status(404).send('User not found');
    }
    user.is_superuser = true;
    await user.save();
    res.status(200).send('User set as admin successfully');
  } catch (error) {
    res.status(400).send(error.message);
  }
});

// 关注其他账号
router.post('/follow', async (req, res) => {
  try {
    const { userId, careId } = req.body;
    const user = await User.findById(userId);
    if (!user) {
      return res.status(404).send('User not found');
    }
    user.care.push({ careID: careId });
    await user.save();
    res.status(200).send('User followed successfully');
  } catch (error) {
    res.status(400).send(error.message);
  }
});

// 实时修改地理位置
router.post('/updateLocation', async (req, res) => {
  try {
    const { userId, lat, lon } = req.body;
    const user = await User.findById(userId);
    if (!user) {
      return res.status(404).send('User not found');
    }
    user.poi = { lat, lon };
    await user.save();
    res.status(200).send('Location updated successfully');
  } catch (error) {
    res.status(400).send(error.message);
  }
});

// 获取用户加入的所有群聊
router.get('/:userId/groups', async (req, res) => {
  try {
    const { userId } = req.params;
    const user = await User.findById(userId).populate('user_in_groups');
    if (!user) {
      return res.status(404).send('User not found');
    }
    res.status(200).json(user.user_in_groups);
  } catch (error) {
    res.status(400).send(error.message);
  }
});

// 获取用户地理位置
router.get('/getLocation/:userId', async (req, res) => {
  try {
    const { userId } = req.params;
    
    // 查找用户信息
    const user = await User.findById(userId);
    if (!user) {
      return res.status(404).send('User not found');
    }

    // 返回用户的地理位置
    res.status(200).json({
      lat: user.poi.lat,
      lon: user.poi.lon
    });
  } catch (error) {
    res.status(400).send(error.message);
  }
});

router.get('/followList/:userId', async (req, res) => {
  try {
    const { userId } = req.params;

    // 查找用户并使用 populate 填充 careID 的用户信息
    const user = await User.findById(userId).populate('care.careID');
    if (!user) {
      return res.status(404).send('User not found');
    }

    // 获取用户关注的所有用户信息
    const followList = user.care.map(care => ({
      careID: care.careID,  // 获取完整的用户信息对象
      addtime: care.addtime  // 获取关注时间
    }));

    res.status(200).json(followList);  // 返回关注列表
  } catch (error) {
    res.status(400).send(error.message);
  }
});



module.exports = router;