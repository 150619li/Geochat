using System;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace π‹¿Ì‘±
{
    public partial class CreateGroupForm : Form
    {
        private IMongoDatabase database;

        public event EventHandler GroupCreated;

        public CreateGroupForm(IMongoDatabase database)
        {
            InitializeComponent();
            this.database = database;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var groupsCollection = database.GetCollection<BsonDocument>("groups");
                var newGroup = new BsonDocument
                {
                    { "group_name", txtGroupName.Text },
                    { "memberID", new BsonArray() },
                    { "messages", new BsonArray() },
                    { "lat", lat.Text },
                    { "lon", lon.Text },
                    { "radius", radius.Text }
                };
                groupsCollection.InsertOne(newGroup);
                GroupCreated?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Group created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating group: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
