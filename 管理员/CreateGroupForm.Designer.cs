namespace 管理员
{
    partial class CreateGroupForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lon = new System.Windows.Forms.TextBox();
            this.lat = new System.Windows.Forms.TextBox();
            this.radius = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(97, 22);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(173, 25);
            this.txtGroupName.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(97, 206);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(173, 35);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "创建";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "群聊名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "经度：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "纬度：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "半径：";
            // 
            // lon
            // 
            this.lon.Location = new System.Drawing.Point(97, 70);
            this.lon.Name = "lon";
            this.lon.Size = new System.Drawing.Size(173, 25);
            this.lon.TabIndex = 6;
            this.lon.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lat
            // 
            this.lat.Location = new System.Drawing.Point(97, 120);
            this.lat.Name = "lat";
            this.lat.Size = new System.Drawing.Size(173, 25);
            this.lat.TabIndex = 7;
            // 
            // radius
            // 
            this.radius.Location = new System.Drawing.Point(97, 169);
            this.radius.Name = "radius";
            this.radius.Size = new System.Drawing.Size(173, 25);
            this.radius.TabIndex = 8;
            // 
            // CreateGroupForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.radius);
            this.Controls.Add(this.lat);
            this.Controls.Add(this.lon);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtGroupName);
            this.Name = "CreateGroupForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //private System.Windows.Forms.TextBox txtGroupName;
       // private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox lon;
        private System.Windows.Forms.TextBox lat;
        private System.Windows.Forms.TextBox radius;
    }
}
