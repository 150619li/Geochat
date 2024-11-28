data class ChatRoom(
    val id: String = "",
    val name: String = "",
    val latitude: Double = 0.0,
    val longitude: Double = 0.0,
    val radius: Int = 100, // 范围半径（米）
    val type: String = "", // 例如：图书馆、食堂等
    val participants: List<String> = listOf(), // 用户ID列表
    val createdAt: Long = System.currentTimeMillis()
)