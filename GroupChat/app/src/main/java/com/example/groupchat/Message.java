package com.example.groupchat;
public class Message {
    private String senderId;
    private String content;

    // 构造函数
    public Message(String senderId, String content) {
        this.senderId = senderId;
        this.content = content;
    }

    // Getter 和 Setter 方法
    public String getSenderId() {
        return senderId;
    }

    public void setSenderId(String senderId) {
        this.senderId = senderId;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }
}
