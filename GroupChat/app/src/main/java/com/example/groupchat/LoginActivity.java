package com.example.groupchat;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class LoginActivity extends AppCompatActivity {

    private EditText usernameEditText, passwordEditText;
    private Button loginButton;
    private ApiService apiService = ApiClient.getRetrofitInstance().create(ApiService.class);

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.login);

        apiService = ApiClient.getRetrofitInstance().create(ApiService.class);

        usernameEditText = findViewById(R.id.usernameEditText);
        passwordEditText = findViewById(R.id.passwordEditText);
        loginButton = findViewById(R.id.loginButton);

        loginButton.setOnClickListener(view -> {
            String username = usernameEditText.getText().toString();
            String password = passwordEditText.getText().toString();

            LoginRequest loginRequest = new LoginRequest(username, password);

            apiService.loginUser(loginRequest).enqueue(new Callback<LoginResponse>() {
                @Override
                public void onResponse(@NonNull Call<LoginResponse> call, @NonNull Response<LoginResponse> response) {

                    if (response.isSuccessful()) {
                        if (response.body() != null) {
                            String token = response.body().getToken();
                        }
                        Toast.makeText(LoginActivity.this, "登录成功", Toast.LENGTH_SHORT).show();
                        String UserId = response.body().getToken();
                        Intent intent = new Intent(LoginActivity.this, GroupListActivity.class);
                        intent.putExtra("UserId", UserId);
                        startActivity(intent);
                        finish();
                    } else {
                        Toast.makeText(LoginActivity.this, "登录失败", Toast.LENGTH_SHORT).show();

                        Intent intent = new Intent(LoginActivity.this, GroupListActivity.class);
                        startActivity(intent);
                    }
                }

                @Override
                public void onFailure(@NonNull Call<LoginResponse> call, @NonNull Throwable t) {
                    Toast.makeText(LoginActivity.this, "网络错误", Toast.LENGTH_SHORT).show();
                }
            });
        });
    }
}
