data class User(
    val id: String = "",
    val name: String = "",
    val email: String = "",
    val avatarUrl: String = "",
    val currentLatitude: Double = 0.0,
    val currentLongitude: Double = 0.0,
    val joinedRooms: List<String> = listOf(),
    val lastActive: Long = System.currentTimeMillis()
) 