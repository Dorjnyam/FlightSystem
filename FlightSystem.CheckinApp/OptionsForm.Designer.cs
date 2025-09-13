namespace FlightSystem.CheckinApp
{
    partial class OptionsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelMain;
        private Label lblWelcome;
        private Label lblRole;
        private Button btnCheckin;
        private Button btnFlightPassengerManagement;
        private Button btnClose;
        private GroupBox grpOptions;

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
            this.panelMain = new Panel();
            this.btnClose = new Button();
            this.btnFlightPassengerManagement = new Button();
            this.btnCheckin = new Button();
            this.grpOptions = new GroupBox();
            this.lblRole = new Label();
            this.lblWelcome = new Label();
            
            this.panelMain.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();
            
            // panelMain
            this.panelMain.Controls.Add(this.btnClose);
            this.panelMain.Controls.Add(this.grpOptions);
            this.panelMain.Controls.Add(this.lblRole);
            this.panelMain.Controls.Add(this.lblWelcome);
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Location = new Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new Size(500, 400);
            this.panelMain.TabIndex = 0;
            
            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.Blue;
            this.lblWelcome.Location = new Point(150, 30);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new Size(200, 26);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome, Employee";
            
            // lblRole
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new Font("Microsoft Sans Serif", 12F);
            this.lblRole.ForeColor = System.Drawing.Color.Gray;
            this.lblRole.Location = new Point(180, 70);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new Size(140, 20);
            this.lblRole.TabIndex = 1;
            this.lblRole.Text = "Role: Check-in Agent";
            
            // grpOptions
            this.grpOptions.Controls.Add(this.btnFlightPassengerManagement);
            this.grpOptions.Controls.Add(this.btnCheckin);
            this.grpOptions.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.grpOptions.Location = new Point(50, 120);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new Size(400, 200);
            this.grpOptions.TabIndex = 2;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Select Option";
            
            // btnCheckin
            this.btnCheckin.BackColor = System.Drawing.Color.FromArgb(25, 118, 210);
            this.btnCheckin.FlatAppearance.BorderSize = 0;
            this.btnCheckin.FlatStyle = FlatStyle.Flat;
            this.btnCheckin.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            this.btnCheckin.ForeColor = System.Drawing.Color.White;
            this.btnCheckin.Location = new Point(50, 50);
            this.btnCheckin.Name = "btnCheckin";
            this.btnCheckin.Size = new Size(300, 50);
            this.btnCheckin.TabIndex = 0;
            this.btnCheckin.Text = "Passenger Check-in";
            this.btnCheckin.UseVisualStyleBackColor = false;
            this.btnCheckin.Click += new EventHandler(this.btnCheckin_Click);
            
            // btnFlightPassengerManagement
            this.btnFlightPassengerManagement.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnFlightPassengerManagement.FlatAppearance.BorderSize = 0;
            this.btnFlightPassengerManagement.FlatStyle = FlatStyle.Flat;
            this.btnFlightPassengerManagement.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            this.btnFlightPassengerManagement.ForeColor = System.Drawing.Color.White;
            this.btnFlightPassengerManagement.Location = new Point(50, 120);
            this.btnFlightPassengerManagement.Name = "btnFlightPassengerManagement";
            this.btnFlightPassengerManagement.Size = new Size(300, 50);
            this.btnFlightPassengerManagement.TabIndex = 1;
            this.btnFlightPassengerManagement.Text = "Flight Passenger Management";
            this.btnFlightPassengerManagement.UseVisualStyleBackColor = false;
            this.btnFlightPassengerManagement.Click += new EventHandler(this.btnFlightPassengerManagement_Click);
            
            // btnClose
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new Point(200, 340);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(100, 40);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            
            // OptionsForm
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(500, 400);
            this.Controls.Add(this.panelMain);
            this.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Name = "OptionsForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Flight System Options";
            
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
