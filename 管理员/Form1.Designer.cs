namespace 管理员
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnViewUsers = new System.Windows.Forms.Button();
            this.btnViewGroups = new System.Windows.Forms.Button();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.listBoxGroups = new System.Windows.Forms.ListBox();
            this.listBoxMessages = new System.Windows.Forms.ListBox();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.btnDeleteMessage = new System.Windows.Forms.Button();
            this.CreateUser = new System.Windows.Forms.Button();
            this.CreateGroup = new System.Windows.Forms.Button();
            this.btnDeleteGroup = new System.Windows.Forms.Button();
            this.btnSetAdmin = new System.Windows.Forms.Button();
            this.listBoxGroupUsers = new System.Windows.Forms.ListBox();
            this.btnDeleteGroupUser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnViewUsers
            // 
            this.btnViewUsers.Location = new System.Drawing.Point(13, 40);
            this.btnViewUsers.Name = "btnViewUsers";
            this.btnViewUsers.Size = new System.Drawing.Size(166, 35);
            this.btnViewUsers.TabIndex = 0;
            this.btnViewUsers.Text = "查看用户列表";
            this.btnViewUsers.UseVisualStyleBackColor = true;
            this.btnViewUsers.Click += new System.EventHandler(this.btnViewUsers_Click);
            // 
            // btnViewGroups
            // 
            this.btnViewGroups.Location = new System.Drawing.Point(12, 81);
            this.btnViewGroups.Name = "btnViewGroups";
            this.btnViewGroups.Size = new System.Drawing.Size(166, 35);
            this.btnViewGroups.TabIndex = 1;
            this.btnViewGroups.Text = "查看群聊列表";
            this.btnViewGroups.UseVisualStyleBackColor = true;
            this.btnViewGroups.Click += new System.EventHandler(this.btnViewGroups_Click);
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.IntegralHeight = false;
            this.listBoxUsers.ItemHeight = 15;
            this.listBoxUsers.Location = new System.Drawing.Point(13, 130);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.ScrollAlwaysVisible = true;
            this.listBoxUsers.Size = new System.Drawing.Size(166, 274);
            this.listBoxUsers.TabIndex = 2;
            this.listBoxUsers.SelectedIndexChanged += new System.EventHandler(this.listBoxUsers_SelectedIndexChanged);
            // 
            // listBoxGroups
            // 
            this.listBoxGroups.FormattingEnabled = true;
            this.listBoxGroups.IntegralHeight = false;
            this.listBoxGroups.ItemHeight = 15;
            this.listBoxGroups.Location = new System.Drawing.Point(210, 40);
            this.listBoxGroups.Name = "listBoxGroups";
            this.listBoxGroups.ScrollAlwaysVisible = true;
            this.listBoxGroups.Size = new System.Drawing.Size(327, 364);
            this.listBoxGroups.TabIndex = 3;
            this.listBoxGroups.SelectedIndexChanged += new System.EventHandler(this.listBoxGroups_SelectedIndexChanged);
            // 
            // listBoxMessages
            // 
            this.listBoxMessages.FormattingEnabled = true;
            this.listBoxMessages.IntegralHeight = false;
            this.listBoxMessages.ItemHeight = 15;
            this.listBoxMessages.Location = new System.Drawing.Point(556, 39);
            this.listBoxMessages.Name = "listBoxMessages";
            this.listBoxMessages.ScrollAlwaysVisible = true;
            this.listBoxMessages.Size = new System.Drawing.Size(464, 184);
            this.listBoxMessages.TabIndex = 4;
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(12, 456);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(166, 35);
            this.btnDeleteUser.TabIndex = 5;
            this.btnDeleteUser.Text = "删除用户";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // btnDeleteMessage
            // 
            this.btnDeleteMessage.Location = new System.Drawing.Point(556, 415);
            this.btnDeleteMessage.Name = "btnDeleteMessage";
            this.btnDeleteMessage.Size = new System.Drawing.Size(464, 35);
            this.btnDeleteMessage.TabIndex = 6;
            this.btnDeleteMessage.Text = "删除群聊消息";
            this.btnDeleteMessage.UseVisualStyleBackColor = true;
            this.btnDeleteMessage.Click += new System.EventHandler(this.btnDeleteMessage_Click);
            // 
            // CreateUser
            // 
            this.CreateUser.Location = new System.Drawing.Point(12, 415);
            this.CreateUser.Name = "CreateUser";
            this.CreateUser.Size = new System.Drawing.Size(167, 35);
            this.CreateUser.TabIndex = 7;
            this.CreateUser.Text = "创建用户";
            this.CreateUser.UseVisualStyleBackColor = true;
            this.CreateUser.Click += new System.EventHandler(this.CreateUser_Click);
            // 
            // CreateGroup
            // 
            this.CreateGroup.Location = new System.Drawing.Point(210, 415);
            this.CreateGroup.Name = "CreateGroup";
            this.CreateGroup.Size = new System.Drawing.Size(327, 35);
            this.CreateGroup.TabIndex = 8;
            this.CreateGroup.Text = "创建群聊";
            this.CreateGroup.UseVisualStyleBackColor = true;
            this.CreateGroup.Click += new System.EventHandler(this.CreateGroup_Click);
            // 
            // btnDeleteGroup
            // 
            this.btnDeleteGroup.Location = new System.Drawing.Point(210, 456);
            this.btnDeleteGroup.Name = "btnDeleteGroup";
            this.btnDeleteGroup.Size = new System.Drawing.Size(327, 35);
            this.btnDeleteGroup.TabIndex = 9;
            this.btnDeleteGroup.Text = "删除群聊";
            this.btnDeleteGroup.UseVisualStyleBackColor = true;
            this.btnDeleteGroup.Click += new System.EventHandler(this.btnDeleteGroup_Click);
            // 
            // btnSetAdmin
            // 
            this.btnSetAdmin.Location = new System.Drawing.Point(13, 497);
            this.btnSetAdmin.Name = "btnSetAdmin";
            this.btnSetAdmin.Size = new System.Drawing.Size(166, 35);
            this.btnSetAdmin.TabIndex = 10;
            this.btnSetAdmin.Text = "设置用户为管理员";
            this.btnSetAdmin.UseVisualStyleBackColor = true;
            this.btnSetAdmin.Click += new System.EventHandler(this.btnSetAdmin_Click);
            // 
            // listBoxGroupUsers
            // 
            this.listBoxGroupUsers.FormattingEnabled = true;
            this.listBoxGroupUsers.IntegralHeight = false;
            this.listBoxGroupUsers.ItemHeight = 15;
            this.listBoxGroupUsers.Location = new System.Drawing.Point(556, 247);
            this.listBoxGroupUsers.Name = "listBoxGroupUsers";
            this.listBoxGroupUsers.ScrollAlwaysVisible = true;
            this.listBoxGroupUsers.Size = new System.Drawing.Size(464, 155);
            this.listBoxGroupUsers.TabIndex = 11;
            // 
            // btnDeleteGroupUser
            // 
            this.btnDeleteGroupUser.Location = new System.Drawing.Point(556, 456);
            this.btnDeleteGroupUser.Name = "btnDeleteGroupUser";
            this.btnDeleteGroupUser.Size = new System.Drawing.Size(464, 35);
            this.btnDeleteGroupUser.TabIndex = 12;
            this.btnDeleteGroupUser.Text = "剔除群成员";
            this.btnDeleteGroupUser.UseVisualStyleBackColor = true;
            this.btnDeleteGroupUser.Click += new System.EventHandler(this.btnDeleteGroupUser_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 551);
            this.Controls.Add(this.btnDeleteGroupUser);
            this.Controls.Add(this.listBoxGroupUsers);
            this.Controls.Add(this.btnSetAdmin);
            this.Controls.Add(this.btnDeleteGroup);
            this.Controls.Add(this.CreateGroup);
            this.Controls.Add(this.CreateUser);
            this.Controls.Add(this.btnDeleteMessage);
            this.Controls.Add(this.btnDeleteUser);
            this.Controls.Add(this.listBoxMessages);
            this.Controls.Add(this.listBoxGroups);
            this.Controls.Add(this.listBoxUsers);
            this.Controls.Add(this.btnViewGroups);
            this.Controls.Add(this.btnViewUsers);
            this.Name = "Form1";
            this.Text = "管理员";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnViewUsers;
        private System.Windows.Forms.Button btnViewGroups;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.ListBox listBoxGroups;
        private System.Windows.Forms.ListBox listBoxMessages;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Button btnDeleteMessage;
        private System.Windows.Forms.Button CreateUser;
        private System.Windows.Forms.Button CreateGroup;
        private System.Windows.Forms.Button btnDeleteGroup;
        private System.Windows.Forms.Button btnSetAdmin;
        private System.Windows.Forms.ListBox listBoxGroupUsers;
        private System.Windows.Forms.Button btnDeleteGroupUser;
    }
}

