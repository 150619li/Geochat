package com.example.locationchat.activities

import android.content.Intent
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.example.locationchat.R
import com.example.locationchat.adapters.RoomListAdapter
import com.example.locationchat.databinding.ActivityMainBinding
import com.example.locationchat.services.LocationService

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding
    private lateinit var roomsAdapter: RoomListAdapter
    
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)
        
        // 初始化位置服务
        initLocationService()
        
        // 初始化聊天室列表
        setupRoomsList()
        
        // 监听附近的聊天室
        observeNearbyRooms()
    }
    
    private fun initLocationService() {
        // 检查并请求位置权限
        if (checkLocationPermission()) {
            startService(Intent(this, LocationService::class.java))
        } else {
            requestLocationPermission()
        }
    }
    
    private fun observeNearbyRooms() {
        // 使用 Firebase 或其他后端服务监听附近的聊天室
    }
}