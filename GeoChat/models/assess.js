const mongoose = require('mongoose');

const assessSchema = new mongoose.Schema({
  noteID: { type: mongoose.Schema.Types.ObjectId, required: true, unique: true },
  note_text: { type: String, required: true },
  senderID: { type: mongoose.Schema.Types.ObjectId, ref: 'User', required: true },
  likes: { type: Number, default: 0 },
  backID: { type: mongoose.Schema.Types.ObjectId, ref: 'Assess' }
});

const Assess = mongoose.model('Assess', assessSchema);

module.exports = Assess;