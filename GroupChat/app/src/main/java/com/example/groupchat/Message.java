package com.example.groupchat;

import java.util.Date;

public class Message {
    private String sender;
    private String content;
    private String groupId;  // 新增 groupId 字段

    public Message(String sender, String content, String groupId) {
        this.sender = sender;
        this.content = content;
        this.groupId = groupId;
    }
    // Getters 和 Setters
    public String getSender() {
        return sender;
    }

    public void setSender(String sender) {
        this.sender = sender;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }

    public String getGroupId() {
        return groupId;
    }

    public void setGroupId(String groupId) {
        this.groupId = groupId;
    }
}
