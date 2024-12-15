package com.example.groupchat;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import java.util.ArrayList;
import java.util.List;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ChatActivity extends AppCompatActivity {

    private RecyclerView messageRecyclerView;
    private EditText messageEditText;
    private Button sendButton;
    private MessageAdapter messageAdapter;
    private List<Message> messageList = new ArrayList<>();

    private ApiService apiService;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.chat);

        messageRecyclerView = findViewById(R.id.messageRecyclerView);
        messageEditText = findViewById(R.id.messageEditText);
        sendButton = findViewById(R.id.sendButton);


        // 初始化 RecyclerView
        messageRecyclerView.setLayoutManager(new LinearLayoutManager(this));
        messageAdapter = new MessageAdapter(messageList);
        messageRecyclerView.setAdapter(messageAdapter);

        // 初始化 ApiService
        apiService = ApiClient.getRetrofitInstance().create(ApiService.class);

        // 获取传递的 groupId
        String groupId = getIntent().getStringExtra("groupId");

        if (groupId == null) {
            Toast.makeText(this, "无法获取群组 ID", Toast.LENGTH_SHORT).show();
            finish();
            return;
        }

        // 使用 groupId 加载聊天内容
        loadMessages(groupId);

        sendButton.setOnClickListener(view -> {
            String messageContent = messageEditText.getText().toString().trim();
            if (!messageContent.isEmpty()) {
                sendMessage(messageContent,groupId);
            } else {
                Toast.makeText(ChatActivity.this, "消息不能为空", Toast.LENGTH_SHORT).show();
            }
        });
    }

    private void loadMessages(String groupId) {
        apiService.getGroupMessages(groupId).enqueue(new Callback<List<Message>>() {
            @Override
            public void onResponse(Call<List<Message>> call, Response<List<Message>> response) {
                if (response.isSuccessful() && response.body() != null) {
                    messageList.clear();
                    messageList.addAll(response.body());
                    messageAdapter.notifyDataSetChanged();
                } else {
                    Toast.makeText(ChatActivity.this, "无法加载消息", Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<List<Message>> call, Throwable t) {
                Toast.makeText(ChatActivity.this, "加载消息失败：" + t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });
    }
    private void sendMessage(String messageContent,String groupId) {
        Message message = new Message("user_id_here", messageContent); // 替换为实际的用户 ID
        apiService.sendMessage(groupId, message).enqueue(new Callback<Void>() {
            @Override
            public void onResponse(Call<Void> call, Response<Void> response) {
                if (response.isSuccessful()) {
                    Toast.makeText(ChatActivity.this, "消息已发送", Toast.LENGTH_SHORT).show();
                    messageEditText.setText(""); // 清空输入框
                    loadMessages(groupId); // 重新加载消息
                } else {
                    Toast.makeText(ChatActivity.this, "发送失败", Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<Void> call, Throwable t) {
                Toast.makeText(ChatActivity.this, "发送失败：" + t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });
    }
}
