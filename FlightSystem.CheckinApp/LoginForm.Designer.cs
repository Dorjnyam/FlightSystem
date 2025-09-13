#nullable enable
namespace FlightSystem.CheckinApp
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelMain;
        private Panel loginContainer;
        private Label lblTitle;
        private Label lblEmployeeCode;
        private TextBox txtEmployeeCode;
        private Label lblPassword;
        private TextBox txtPassword;
        private CheckBox chkSaveCredentials;
        private Button btnLogin;
        private Button btnCancel;
        private PictureBox picLogo;

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
            this.loginContainer = new Panel();
            this.picLogo = new PictureBox();
            this.lblTitle = new Label();
            this.lblEmployeeCode = new Label();
            this.txtEmployeeCode = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.chkSaveCredentials = new CheckBox();
            this.btnLogin = new Button();
            this.btnCancel = new Button();
            
            this.panelMain.SuspendLayout();
            this.loginContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            
            // panelMain
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            this.panelMain.Controls.Add(this.loginContainer);
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Location = new Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new Size(500, 600);
            this.panelMain.TabIndex = 0;
            
            // loginContainer
            this.loginContainer.BackColor = System.Drawing.Color.White;
            this.loginContainer.BorderStyle = BorderStyle.FixedSingle;
            this.loginContainer.Controls.Add(this.picLogo);
            this.loginContainer.Controls.Add(this.lblTitle);
            this.loginContainer.Controls.Add(this.lblEmployeeCode);
            this.loginContainer.Controls.Add(this.txtEmployeeCode);
            this.loginContainer.Controls.Add(this.lblPassword);
            this.loginContainer.Controls.Add(this.txtPassword);
            this.loginContainer.Controls.Add(this.chkSaveCredentials);
            this.loginContainer.Controls.Add(this.btnLogin);
            this.loginContainer.Controls.Add(this.btnCancel);
            this.loginContainer.Location = new Point(50, 50);
            this.loginContainer.Name = "loginContainer";
            this.loginContainer.Size = new Size(400, 500);
            this.loginContainer.TabIndex = 0;
            
            // picLogo
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Image = System.Drawing.SystemIcons.Application.ToBitmap();
            this.picLogo.Location = new Point(150, 20);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new Size(100, 80);
            this.picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(25, 118, 210);
            this.lblTitle.Location = new Point(100, 120);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(200, 29);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Системд нэвтрэх";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            
            // lblEmployeeCode
            this.lblEmployeeCode.AutoSize = true;
            this.lblEmployeeCode.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.lblEmployeeCode.Location = new Point(50, 180);
            this.lblEmployeeCode.Name = "lblEmployeeCode";
            this.lblEmployeeCode.Size = new Size(100, 17);
            this.lblEmployeeCode.TabIndex = 2;
            this.lblEmployeeCode.Text = "Ажилтны код:";
            
            // txtEmployeeCode
            this.txtEmployeeCode.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtEmployeeCode.Location = new Point(50, 200);
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.Size = new Size(300, 23);
            this.txtEmployeeCode.TabIndex = 3;
            
            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.lblPassword.Location = new Point(50, 250);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(70, 17);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Нууц үг:";
            
            // txtPassword
            this.txtPassword.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtPassword.Location = new Point(50, 270);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(300, 23);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.KeyPress += new KeyPressEventHandler(this.txtPassword_KeyPress);
            
            // chkSaveCredentials
            this.chkSaveCredentials.AutoSize = true;
            this.chkSaveCredentials.Font = new Font("Microsoft Sans Serif", 9F);
            this.chkSaveCredentials.Location = new Point(50, 310);
            this.chkSaveCredentials.Name = "chkSaveCredentials";
            this.chkSaveCredentials.Size = new Size(200, 19);
            this.chkSaveCredentials.TabIndex = 6;
            this.chkSaveCredentials.Text = "Нэвтрэх мэдээллийг хадгалах";
            this.chkSaveCredentials.UseVisualStyleBackColor = true;
            
            // btnLogin
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(25, 118, 210);
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new Point(50, 350);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(140, 40);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "Нэвтрэх";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            
            // btnCancel
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new Point(210, 350);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(140, 40);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Цуцлах";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            
            // LoginForm
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(500, 600);
            this.Controls.Add(this.panelMain);
            this.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Нислэгийн систем - Нэвтрэх";
            
            this.panelMain.ResumeLayout(false);
            this.loginContainer.ResumeLayout(false);
            this.loginContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
        }

        private void txtPassword_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}
