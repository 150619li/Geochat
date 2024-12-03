object FirebaseUtils {
    private val db = FirebaseFirestore.getInstance()
    private val auth = FirebaseAuth.getInstance()

    fun getCurrentUser() = auth.currentUser

    fun observeNearbyRooms(
        latitude: Double,
        longitude: Double,
        radius: Double,
        callback: (List<ChatRoom>) -> Unit
    ) {
        db.collection("chatrooms")
            .addSnapshotListener { snapshot, e ->
                if (e != null) return@addSnapshotListener

                val nearbyRooms = snapshot?.documents
                    ?.mapNotNull { it.toObject(ChatRoom::class.java) }
                    ?.filter { room ->
                        LocationUtils.isInRange(
                            latitude,
                            longitude,
                            room.latitude,
                            room.longitude,
                            room.radius
                        )
                    } ?: listOf()

                callback(nearbyRooms)
            }
    }

    fun sendMessage(roomId: String, message: Message) {
        db.collection("chatrooms")
            .document(roomId)
            .collection("messages")
            .add(message)
    }

    fun joinRoom(roomId: String, userId: String) {
        db.collection("chatrooms")
            .document(roomId)
            .update("participants", FieldValue.arrayUnion(userId))
    }

    fun leaveRoom(roomId: String, userId: String) {
        db.collection("chatrooms")
            .document(roomId)
            .update("participants", FieldValue.arrayRemove(userId))
    }
}