class ChatAdapter : RecyclerView.Adapter<ChatAdapter.MessageViewHolder>() {
    private val messages = mutableListOf<Message>()
    private var currentUserId: String = ""

    fun setCurrentUserId(userId: String) {
        currentUserId = userId
    }

    fun updateMessages(newMessages: List<Message>) {
        messages.clear()
        messages.addAll(newMessages)
        notifyDataSetChanged()
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MessageViewHolder {
        val inflater = LayoutInflater.from(parent.context)
        val binding = ItemMessageBinding.inflate(inflater, parent, false)
        return MessageViewHolder(binding)
    }

    override fun onBindViewHolder(holder: MessageViewHolder, position: Int) {
        holder.bind(messages[position])
    }

    override fun getItemCount() = messages.size

    inner class MessageViewHolder(private val binding: ItemMessageBinding) : 
        RecyclerView.ViewHolder(binding.root) {
        
        fun bind(message: Message) {
            binding.apply {
                messageText.text = message.content
                senderName.text = message.senderName
                timeText.text = formatTime(message.timestamp)
                
                // 根据发送者调整消息布局
                messageContainer.layoutDirection = 
                    if (message.senderId == currentUserId) View.LAYOUT_DIRECTION_RTL
                    else View.LAYOUT_DIRECTION_LTR
            }
        }
    }
} 