package com.example.groupchat;

public class LoginRequest {
    private String useremail;
    private String password;

    // 构造函数
    public LoginRequest(String useremail, String password) {
        this.useremail= useremail;
        this.password = password;
    }

    // Getter 和 Setter 方法
    public String getUseremail() {
        return useremail;
    }

    public void setUseremail(String useremail) {
        this.useremail = useremail;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }
}
