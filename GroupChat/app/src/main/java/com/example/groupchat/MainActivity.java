package com.example.groupchat;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import androidx.appcompat.app.AppCompatActivity;

public class MainActivity extends AppCompatActivity {

    private Button loginButton, registerButton, groupListButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // 初始化按钮
        loginButton = findViewById(R.id.loginButton);
        registerButton = findViewById(R.id.registerButton);
        //groupListButton = findViewById(R.id.groupListButton);

        // 跳转到登录页面
        loginButton.setOnClickListener(view -> {
            Intent intent = new Intent(MainActivity.this, LoginActivity.class);
            startActivity(intent);
        });

        // 跳转到注册页面
        registerButton.setOnClickListener(view -> {
            Intent intent = new Intent(MainActivity.this, RegisterActivity.class);
            startActivity(intent);
        });

        // 跳转到群组列表页面
        //groupListButton.setOnClickListener(view -> {
        //    Intent intent = new Intent(MainActivity.this, GroupListActivity.class);
        //    startActivity(intent);
        //});
    }
}