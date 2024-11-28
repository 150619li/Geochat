object LocationUtils {
    fun calculateDistance(
        lat1: Double, 
        lon1: Double, 
        lat2: Double, 
        lon2: Double
    ): Float {
        val results = FloatArray(1)
        Location.distanceBetween(lat1, lon1, lat2, lon2, results)
        return results[0]
    }

    fun isInRange(
        userLat: Double,
        userLon: Double,
        roomLat: Double,
        roomLon: Double,
        radiusInMeters: Int
    ): Boolean {
        val distance = calculateDistance(userLat, userLon, roomLat, roomLon)
        return distance <= radiusInMeters
    }
} 