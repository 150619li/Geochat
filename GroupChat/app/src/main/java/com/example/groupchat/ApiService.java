package com.example.groupchat;
import java.util.List;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;
import retrofit2.http.GET;
import retrofit2.http.Path;
public interface ApiService {
    @POST("/api/users/register")
    Call<Void> registerUser(@Body User user);
    @POST("/api/users/login")
    Call<LoginResponse> loginUser(@Body LoginRequest loginRequest);
    @POST("/api/groups/create")
    Call<Void> createGroup(@Body Group group);
    @GET("/api/messages/:groupId/messages")
    Call<List<Message>>getGroupMessages(@Path("groupId") String groupId);
    @POST("/api/messages/:groupId/messages")
    Call<Void>sendMessage(@Path("groupId") String groupId,@Body Message message);
    @GET("/api/groups")
    Call<List<Group>> getGroups(@Path("UserId") String UserId);

}
