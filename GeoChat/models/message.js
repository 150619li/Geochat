const mongoose = require('mongoose');

const messageSchema = new mongoose.Schema({
  messageID: { type: mongoose.Schema.Types.ObjectId, required: true, unique: true },
  message_text: { type: String, required: true },
  userID_send: { type: mongoose.Schema.Types.ObjectId, ref: 'User', required: true },
  userID_receive: { type: mongoose.Schema.Types.ObjectId, ref: 'User' },
  groupID: { type: mongoose.Schema.Types.ObjectId, ref: 'Group' },
  message_time: { type: Date, default: Date.now }
});

const Message = mongoose.model('Message', messageSchema);

module.exports = Message;