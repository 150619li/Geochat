const mongoose = require('mongoose');

const userSchema = new mongoose.Schema({
  userID: { type: mongoose.Schema.Types.ObjectId, required: true, unique: true },
  username: { type: String, required: true },
  email: { type: String, required: true, unique: true },
  password: { type: String, required: true },
  is_superuser: { type: Boolean, default: false },
  user_in_groups: [{ type: mongoose.Schema.Types.ObjectId, ref: 'Group' }], // 用户加入的所有群聊
  poi: { 
    lat: { type: Number },
    lon: { type: Number }
   },
  profile: { type: String }, //图片的url
  care: [{
    careID: { type: mongoose.Schema.Types.ObjectId, ref: 'User' },
    addtime: { type: Date, default: Date.now }
  }]
});

const User = mongoose.model('User', userSchema);

module.exports = User;