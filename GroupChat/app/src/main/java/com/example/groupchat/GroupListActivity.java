package com.example.groupchat;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class GroupListActivity extends AppCompatActivity {

    private ListView groupListView;
    private Button createGroupButton;
    private ApiService apiService= ApiClient.getRetrofitInstance().create(ApiService.class);
    private List<Group> groupList = new ArrayList<>();
    private ArrayAdapter<String> groupAdapter;
    private String UserId = getIntent().getStringExtra("UserId");
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.group_list);

        groupListView = findViewById(R.id.groupListView);
        createGroupButton = findViewById(R.id.createGroupButton);

        groupAdapter = new ArrayAdapter<>(this, android.R.layout.simple_list_item_1, new ArrayList<>());
        groupListView.setAdapter(groupAdapter);

        // 加载群组数据
        loadGroups(UserId);

        createGroupButton.setOnClickListener(view -> {
            // 创建群组逻辑
            showCreateGroupDialog();
        });

        // 设置点击某个群组进入群聊界面
        groupListView.setOnItemClickListener((adapterView, view, position, id) -> {
            // 获取实际的 groupId
            String groupId = groupList.get(position).getGroupId();
            Intent intent = new Intent(GroupListActivity.this, ChatActivity.class);
            intent.putExtra("groupId", groupId);
            startActivity(intent);
        });
    }
    private void showCreateGroupDialog() {
        // 创建一个容器布局
        LinearLayout layout = new LinearLayout(this);
        layout.setOrientation(LinearLayout.VERTICAL);
        layout.setPadding(50, 40, 50, 40); // 设置内边距

        // 群组名称输入框
        final EditText inputGroupName = new EditText(this);
        inputGroupName.setHint("输入群组名称");
        layout.addView(inputGroupName);

        // 群组 ID 输入框
        final EditText inputGroupID = new EditText(this);
        inputGroupID.setHint("输入群组ID");
        layout.addView(inputGroupID);

        // 创建对话框
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle("创建群组");
        builder.setView(layout); // 设置容器为对话框视图

        // 设置确定按钮
        builder.setPositiveButton("创建", (dialog, which) -> {
            String groupName = inputGroupName.getText().toString().trim();
            String groupID = inputGroupID.getText().toString().trim();

            if (!groupName.isEmpty() && !groupID.isEmpty()) {
                createGroup(groupID, groupName);
            } else {
                Toast.makeText(this, "群组名称和ID不能为空", Toast.LENGTH_SHORT).show();
            }
        });

        // 设置取消按钮
        builder.setNegativeButton("取消", (dialog, which) -> dialog.cancel());

        // 显示对话框
        builder.show();
    }


    private void createGroup(String groupName ,String groupID) {
        // 创建群组对象
        Group group = new Group(groupName, null); // 假设不需要额外的成员列表

        // 调用 API 创建群组
        apiService.createGroup(group).enqueue(new Callback<Void>() {
            @Override
            public void onResponse(Call<Void> call, Response<Void> response) {
                if (response.isSuccessful()) {
                    Toast.makeText(GroupListActivity.this, "群组创建成功", Toast.LENGTH_SHORT).show();
                    loadGroups(UserId);
                } else {
                    Toast.makeText(GroupListActivity.this, "群组创建失败", Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<Void> call, Throwable t) {
                Toast.makeText(GroupListActivity.this, "网络错误：" + t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });
    }
    private void loadGroups(String UserId) {
        apiService.getGroups(UserId).enqueue(new Callback<List<Group>>() {
            @Override
            public void onResponse(Call<List<Group>> call, Response<List<Group>> response) {
                if (response.isSuccessful() && response.body() != null) {
                    groupList.clear();
                    groupList.addAll(response.body());

                    // 更新列表显示
                    List<String> groupNames = new ArrayList<>();
                    for (Group group : groupList) {
                        groupNames.add(group.getGroupName());
                    }
                    groupAdapter.clear();
                    groupAdapter.addAll(groupNames);
                    groupAdapter.notifyDataSetChanged();
                } else {
                    Toast.makeText(GroupListActivity.this, "无法加载群组", Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<List<Group>> call, Throwable t) {
                Toast.makeText(GroupListActivity.this, "加载群组失败：" + t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });
    }
}

