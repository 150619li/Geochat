class RoomListAdapter(private val onRoomClick: (ChatRoom) -> Unit) : 
    RecyclerView.Adapter<RoomListAdapter.RoomViewHolder>() {
    
    private val rooms = mutableListOf<ChatRoom>()

    fun updateRooms(newRooms: List<ChatRoom>) {
        rooms.clear()
        rooms.addAll(newRooms)
        notifyDataSetChanged()
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): RoomViewHolder {
        val inflater = LayoutInflater.from(parent.context)
        val binding = ItemRoomBinding.inflate(inflater, parent, false)
        return RoomViewHolder(binding)
    }

    override fun onBindViewHolder(holder: RoomViewHolder, position: Int) {
        holder.bind(rooms[position])
    }

    override fun getItemCount() = rooms.size

    inner class RoomViewHolder(private val binding: ItemRoomBinding) : 
        RecyclerView.ViewHolder(binding.root) {
        
        fun bind(room: ChatRoom) {
            binding.apply {
                roomName.text = room.name
                roomType.text = room.type
                participantsCount.text = "${room.participants.size} 人在线"
                
                root.setOnClickListener { onRoomClick(room) }
            }
        }
    }
} 