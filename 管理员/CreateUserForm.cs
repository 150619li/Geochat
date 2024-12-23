using System;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace π‹¿Ì‘±
{
    public partial class CreateUserForm : Form
    {
        private IMongoDatabase database;

        public event EventHandler UserCreated;

        public CreateUserForm(IMongoDatabase database)
        {
            InitializeComponent();
            this.database = database;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var usersCollection = database.GetCollection<BsonDocument>("users");
                var newUser = new BsonDocument
                {
                    { "username", txtUsername.Text },
                    { "email", txtEmail.Text },
                    { "password", txtPassword.Text },
                    { "is_superuser", false },
                    { "user_in_groups", new BsonArray() },
                    { "poi", new BsonDocument
                        {
                            { "lat", BsonNull.Value },
                            { "lon", BsonNull.Value }
                        }
                    },
                    { "care", new BsonArray() }
                };
                usersCollection.InsertOne(newUser);
                UserCreated?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("User created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
