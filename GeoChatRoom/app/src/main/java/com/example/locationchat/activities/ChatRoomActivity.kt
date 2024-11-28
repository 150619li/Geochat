class ChatRoomActivity : AppCompatActivity() {
    private lateinit var binding: ActivityChatRoomBinding
    private lateinit var chatAdapter: ChatAdapter
    private var roomId: String = ""
    
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityChatRoomBinding.inflate(layoutInflater)
        setContentView(binding.root)
        
        roomId = intent.getStringExtra("room_id") ?: return
        
        setupChat()
        observeMessages()
    }
    
    private fun setupChat() {
        chatAdapter = ChatAdapter()
        binding.recyclerView.adapter = chatAdapter
        
        binding.sendButton.setOnClickListener {
            val message = binding.messageInput.text.toString()
            if (message.isNotEmpty()) {
                sendMessage(message)
                binding.messageInput.setText("")
            }
        }
    }
    
    private fun observeMessages() {
        // 实现消息监听逻辑
    }
    
    private fun sendMessage(message: String) {
        // 实现发送消息逻辑
    }
} 