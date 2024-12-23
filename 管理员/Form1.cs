using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace 管理员
{
    public partial class Form1 : Form
    {
        private IMongoDatabase database;

        public Form1()
        {
            InitializeComponent();
            InitializeMongoDB();
        }
        private void InitializeMongoDB()
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            database = client.GetDatabase("chat_app");
        }
        private void btnViewUsers_Click(object sender, EventArgs e)
        {
            var usersCollection = database.GetCollection<BsonDocument>("users");
            var users = usersCollection.Find(new BsonDocument()).ToList();
            listBoxUsers.Items.Clear();
            foreach (var user in users)
            {
                listBoxUsers.Items.Add(user["username"]);
            }
        }

        private void btnViewGroups_Click(object sender, EventArgs e)
        {
            var groupsCollection = database.GetCollection<BsonDocument>("groups");
            var groups = groupsCollection.Find(new BsonDocument()).ToList();
            listBoxGroups.Items.Clear();
            foreach (var group in groups)
            {
                //listBoxGroups.Items.Add($"{group["_id"]}:{group["name"]}");
                listBoxGroups.Items.Add(group["group_name"]);
            }
        }

        private void listBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxGroups.SelectedItem != null)
            {
                var groupName = listBoxGroups.SelectedItem.ToString();
                var groupsCollection = database.GetCollection<BsonDocument>("groups");
                var groupFilter = Builders<BsonDocument>.Filter.Eq("group_name", groupName);
                var group = groupsCollection.Find(groupFilter).FirstOrDefault();

                if (group != null)
                {
                    var messagesCollection = database.GetCollection<BsonDocument>("messages");
                    var usersCollection = database.GetCollection<BsonDocument>("users");
                    listBoxMessages.Items.Clear();
                    listBoxGroupUsers.Items.Clear();

                    foreach (var messageId in group["messages"].AsBsonArray)
                    {
                        var messageFilter = Builders<BsonDocument>.Filter.Eq("_id", messageId.AsObjectId);
                        var message = messagesCollection.Find(messageFilter).FirstOrDefault();
                        if (message != null)
                        {
                            var senderId = message["userID_send"].AsObjectId;
                            var userFilter = Builders<BsonDocument>.Filter.Eq("_id", senderId);
                            var user = usersCollection.Find(userFilter).FirstOrDefault();
                            var senderName = user != null ? user["username"].AsString : "Unknown";
                            listBoxMessages.Items.Add($"{senderName}: {message["message_text"]} ({message["message_time"]})");

                        }
                    }
                    foreach (var userId in group["memberID"].AsBsonArray)
                    {
                        var userFilter = Builders<BsonDocument>.Filter.Eq("_id", userId.AsObjectId);
                        var user = usersCollection.Find(userFilter).FirstOrDefault();
                        if (user != null)
                        {
                            listBoxGroupUsers.Items.Add(user["username"]);
                        }
                    }
                }
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedItem != null)
            {
                var username = listBoxUsers.SelectedItem.ToString();
                var usersCollection = database.GetCollection<BsonDocument>("users");
                var filter = Builders<BsonDocument>.Filter.Eq("username", username);
                usersCollection.DeleteOne(filter);
                btnViewUsers_Click(sender, e); // Refresh the user list
            }
        }

        private void btnDeleteMessage_Click(object sender, EventArgs e)
        {
            if (listBoxMessages.SelectedItem != null)
            {
                var selectedMessage = listBoxMessages.SelectedItem.ToString();
                var parts = selectedMessage.Split(new[] { ": ", " (" }, StringSplitOptions.None);
                var senderName = parts[0];
                var text = parts[1];
                var timestamp = parts[2].TrimEnd(')');

                var usersCollection = database.GetCollection<BsonDocument>("users");
                var userFilter = Builders<BsonDocument>.Filter.Eq("username", senderName);
                var user = usersCollection.Find(userFilter).FirstOrDefault();
                var senderId = user != null ? user["_id"].AsObjectId : ObjectId.Empty;

                var messagesCollection = database.GetCollection<BsonDocument>("messages");
                var messageFilter = Builders<BsonDocument>.Filter.Eq("userID_send", senderId) &
                                    Builders<BsonDocument>.Filter.Eq("message_text", text) &
                                    Builders<BsonDocument>.Filter.Eq("message_time", BsonDateTime.Create(DateTime.Parse(timestamp)));
                var message = messagesCollection.Find(messageFilter).FirstOrDefault();

                if (message != null)
                {
                    var messageId = message["_id"].AsObjectId;
                    messagesCollection.DeleteOne(messageFilter);

                    var groupsCollection = database.GetCollection<BsonDocument>("groups");
                    var groupFilter = Builders<BsonDocument>.Filter.Eq("messages", messageId);
                    var update = Builders<BsonDocument>.Update.Pull("messages", messageId);
                    groupsCollection.UpdateOne(groupFilter, update);

                    listBoxGroups_SelectedIndexChanged(sender, e); // Refresh the message list
                }
            }
        }

        private void CreateUser_Click(object sender, EventArgs e)
        {
            var createUserForm = new CreateUserForm(database);
            createUserForm.UserCreated += (s, args) => btnViewUsers_Click(sender, e);
            createUserForm.ShowDialog();
        }

        private void CreateGroup_Click(object sender, EventArgs e)
        {
            var createGroupForm = new CreateGroupForm(database);
            createGroupForm.GroupCreated += (s, args) => btnViewGroups_Click(sender, e);
            createGroupForm.ShowDialog();
        }

        private void btnSetAdmin_Click(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedItem != null)
            {
                var username = listBoxUsers.SelectedItem.ToString();
                var usersCollection = database.GetCollection<BsonDocument>("users");
                var filter = Builders<BsonDocument>.Filter.Eq("username", username);
                var update = Builders<BsonDocument>.Update.Set("is_superuser", true);
                usersCollection.UpdateOne(filter, update);
                MessageBox.Show($"{username} 已被设置为管理员。");
            }
        }

        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            if (listBoxGroups.SelectedItem != null)
            {
                var groupName = listBoxGroups.SelectedItem.ToString();
                var groupsCollection = database.GetCollection<BsonDocument>("groups");
                var filter = Builders<BsonDocument>.Filter.Eq("group_name", groupName);
                groupsCollection.DeleteOne(filter);
                btnViewGroups_Click(sender, e); // Refresh the group list
                var username = listBoxUsers.SelectedItem.ToString();
            }
        }

        private void btnDeleteGroupUser_Click(object sender, EventArgs e)
        {
            if (listBoxGroups.SelectedItem != null && listBoxGroupUsers.SelectedItem != null)
            {
                var groupName = listBoxGroups.SelectedItem.ToString();
                var username = listBoxGroupUsers.SelectedItem.ToString();

                var groupsCollection = database.GetCollection<BsonDocument>("groups");
                var usersCollection = database.GetCollection<BsonDocument>("users");

                // 查找用户
                var userFilter = Builders<BsonDocument>.Filter.Eq("username", username);
                var user = usersCollection.Find(userFilter).FirstOrDefault();

                if (user != null)
                {
                    var userId = user["_id"].AsObjectId;

                    // 从群组中移除用户
                    var groupFilter = Builders<BsonDocument>.Filter.Eq("group_name", groupName);
                    var update = Builders<BsonDocument>.Update.Pull("memberID", userId);
                    groupsCollection.UpdateOne(groupFilter, update);

                    MessageBox.Show($"{username} 已被从群组 {groupName} 中移除。");

                    // 刷新群组用户列表
                    listBoxGroups_SelectedIndexChanged(sender, e);
                }
            }
        }
    }
}
