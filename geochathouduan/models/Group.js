const mongoose = require('mongoose');

const groupSchema = new mongoose.Schema({
  groupID: { type: mongoose.Schema.Types.ObjectId, required: true, unique: true },
  group_name: { type: String, required: true },
  memberID: [{ type: mongoose.Schema.Types.ObjectId, ref: 'User' }],
  messages: [{ type: mongoose.Schema.Types.ObjectId, ref: 'Message' }],
  lat: { type: Number },
  lon: { type: Number },
  radius: { type: Number }
});

const Group = mongoose.model('Group', groupSchema);

module.exports = Group;