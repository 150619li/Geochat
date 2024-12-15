package com.example.groupchat;
import java.util.List;
import java.util.ArrayList;
public class Group {
    private String groupName;
    private String groupId;
    private List<String> members; // 可选字段，用于添加成员列表

    public Group(String groupId,String groupName, List<String> members) {
        this.groupName = groupName;
        this.members = members;
        this.groupId = groupId;
    }

    public String getGroupId() {
        return groupId;
    }

    public void setGroupId(String groupId) {
        this.groupId = groupId;
    }
    public String getGroupName() {
        return groupName;
    }

    public void setGroupName(String groupName) {
        this.groupName = groupName;
    }

    public List<String> getMembers() {
        return members;
    }

    public void setMembers(List<String> members) {
        this.members = members;
    }
}
