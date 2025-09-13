namespace FlightSystem.CheckinApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelLogin;
        private Panel panelMain;
        private TextBox txtEmployeeCode;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblEmployeeCode;
        private Label lblPassword;
        private Label lblWelcome;
        private ComboBox cmbFlights;
        private Label lblSelectFlight;
        private Label lblFlightInfo;
        private TextBox txtPassportNumber;
        private Button btnSearchPassenger;
        private Label lblPassportNumber;
        private Label lblPassengerInfo;
        private Panel panelSeats;
        private Button btnUpdateFlightStatus;
        private TextBox txtLog;
        private Label lblLog;
        private GroupBox grpFlightInfo;
        private GroupBox grpPassengerInfo;
        private GroupBox grpSeatMap;
        private CheckBox chkSaveCredentials;
        private Panel loginContainer;
        private Label lblLoginTitle;
        private GroupBox grpBusinessClass;
        private GroupBox grpEconomyClass;
        private GroupBox grpFirstClass;
        private GroupBox grpPremiumEconomyClass;

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
            this.panelLogin = new Panel();
            this.panelMain = new Panel();
            this.txtEmployeeCode = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.lblEmployeeCode = new Label();
            this.lblPassword = new Label();
            this.lblWelcome = new Label();
            this.cmbFlights = new ComboBox();
            this.lblSelectFlight = new Label();
            this.lblFlightInfo = new Label();
            this.txtPassportNumber = new TextBox();
            this.btnSearchPassenger = new Button();
            this.lblPassportNumber = new Label();
            this.lblPassengerInfo = new Label();
            this.panelSeats = new Panel();
            this.btnUpdateFlightStatus = new Button();
            this.txtLog = new TextBox();
            this.lblLog = new Label();
            this.grpFlightInfo = new GroupBox();
            this.grpPassengerInfo = new GroupBox();
            this.grpSeatMap = new GroupBox();
            this.chkSaveCredentials = new CheckBox();
            this.loginContainer = new Panel();
            this.lblLoginTitle = new Label();
            this.grpBusinessClass = new GroupBox();
            this.grpEconomyClass = new GroupBox();
            this.grpFirstClass = new GroupBox();
            this.grpPremiumEconomyClass = new GroupBox();
            
            this.panelLogin.SuspendLayout();
            this.loginContainer.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.grpFlightInfo.SuspendLayout();
            this.grpPassengerInfo.SuspendLayout();
            this.grpSeatMap.SuspendLayout();
            this.grpBusinessClass.SuspendLayout();
            this.grpEconomyClass.SuspendLayout();
            this.grpFirstClass.SuspendLayout();
            this.grpPremiumEconomyClass.SuspendLayout();
            this.SuspendLayout();
            
            // panelLogin
            this.panelLogin.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            this.panelLogin.Controls.Add(this.loginContainer);
            this.panelLogin.Dock = DockStyle.Fill;
            this.panelLogin.Location = new Point(0, 0);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new Size(1200, 800);
            this.panelLogin.TabIndex = 0;
            
            // loginContainer
            this.loginContainer.BackColor = System.Drawing.Color.White;
            this.loginContainer.BorderStyle = BorderStyle.FixedSingle;
            this.loginContainer.Controls.Add(this.lblLoginTitle);
            this.loginContainer.Controls.Add(this.lblEmployeeCode);
            this.loginContainer.Controls.Add(this.txtEmployeeCode);
            this.loginContainer.Controls.Add(this.lblPassword);
            this.loginContainer.Controls.Add(this.txtPassword);
            this.loginContainer.Controls.Add(this.chkSaveCredentials);
            this.loginContainer.Controls.Add(this.btnLogin);
            this.loginContainer.Location = new Point(400, 250);
            this.loginContainer.Name = "loginContainer";
            this.loginContainer.Size = new Size(400, 300);
            this.loginContainer.TabIndex = 0;
            
            // lblLoginTitle
            this.lblLoginTitle.AutoSize = true;
            this.lblLoginTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            this.lblLoginTitle.ForeColor = System.Drawing.Color.FromArgb(25, 118, 210);
            this.lblLoginTitle.Location = new Point(120, 20);
            this.lblLoginTitle.Name = "lblLoginTitle";
            this.lblLoginTitle.Size = new Size(160, 26);
            this.lblLoginTitle.TabIndex = 0;
            this.lblLoginTitle.Text = "Системд нэвтрэх";
            
            // lblEmployeeCode
            this.lblEmployeeCode.AutoSize = true;
            this.lblEmployeeCode.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.lblEmployeeCode.Location = new Point(50, 70);
            this.lblEmployeeCode.Name = "lblEmployeeCode";
            this.lblEmployeeCode.Size = new Size(100, 17);
            this.lblEmployeeCode.TabIndex = 1;
            this.lblEmployeeCode.Text = "Ажилтны код:";
            
            // txtEmployeeCode
            this.txtEmployeeCode.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtEmployeeCode.Location = new Point(50, 90);
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.Size = new Size(300, 23);
            this.txtEmployeeCode.TabIndex = 2;
            
            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.lblPassword.Location = new Point(50, 130);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(70, 17);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Нууц үг:";
            
            // txtPassword
            this.txtPassword.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtPassword.Location = new Point(50, 150);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(300, 23);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.UseSystemPasswordChar = true;
            
            // chkSaveCredentials
            this.chkSaveCredentials.AutoSize = true;
            this.chkSaveCredentials.Font = new Font("Microsoft Sans Serif", 9F);
            this.chkSaveCredentials.Location = new Point(50, 185);
            this.chkSaveCredentials.Name = "chkSaveCredentials";
            this.chkSaveCredentials.Size = new Size(200, 19);
            this.chkSaveCredentials.TabIndex = 5;
            this.chkSaveCredentials.Text = "Нэвтрэх мэдээллийг хадгалах";
            this.chkSaveCredentials.UseVisualStyleBackColor = true;
            
            // btnLogin
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(25, 118, 210);
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new Point(120, 220);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(160, 40);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "Нэвтрэх";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            
            // panelMain
            this.panelMain.Controls.Add(this.lblWelcome);
            this.panelMain.Controls.Add(this.grpFlightInfo);
            this.panelMain.Controls.Add(this.grpPassengerInfo);
            this.panelMain.Controls.Add(this.grpSeatMap);
            this.panelMain.Controls.Add(this.lblLog);
            this.panelMain.Controls.Add(this.txtLog);
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Location = new Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new Size(1200, 800);
            this.panelMain.TabIndex = 1;
            this.panelMain.Visible = false;
            
            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.Blue;
            this.lblWelcome.Location = new Point(12, 9);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new Size(200, 24);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Тавтай морил";
            
            // grpFlightInfo
            this.grpFlightInfo.Controls.Add(this.lblSelectFlight);
            this.grpFlightInfo.Controls.Add(this.cmbFlights);
            this.grpFlightInfo.Controls.Add(this.lblFlightInfo);
            this.grpFlightInfo.Controls.Add(this.btnUpdateFlightStatus);
            this.grpFlightInfo.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.grpFlightInfo.Location = new Point(12, 40);
            this.grpFlightInfo.Name = "grpFlightInfo";
            this.grpFlightInfo.Size = new Size(380, 200);
            this.grpFlightInfo.TabIndex = 1;
            this.grpFlightInfo.TabStop = false;
            this.grpFlightInfo.Text = "Нислэгийн мэдээлэл";
            
            // lblSelectFlight
            this.lblSelectFlight.AutoSize = true;
            this.lblSelectFlight.Font = new Font("Microsoft Sans Serif", 9F);
            this.lblSelectFlight.Location = new Point(6, 25);
            this.lblSelectFlight.Name = "lblSelectFlight";
            this.lblSelectFlight.Size = new Size(90, 15);
            this.lblSelectFlight.TabIndex = 0;
            this.lblSelectFlight.Text = "Нислэг сонгох:";
            
            // cmbFlights
            this.cmbFlights.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFlights.Font = new Font("Microsoft Sans Serif", 9F);
            this.cmbFlights.FormattingEnabled = true;
            this.cmbFlights.Location = new Point(6, 43);
            this.cmbFlights.Name = "cmbFlights";
            this.cmbFlights.Size = new Size(200, 23);
            this.cmbFlights.TabIndex = 1;
            this.cmbFlights.SelectedIndexChanged += new EventHandler(this.cmbFlights_SelectedIndexChanged);
            
            // lblFlightInfo
            this.lblFlightInfo.Font = new Font("Microsoft Sans Serif", 9F);
            this.lblFlightInfo.Location = new Point(6, 75);
            this.lblFlightInfo.Name = "lblFlightInfo";
            this.lblFlightInfo.Size = new Size(360, 80);
            this.lblFlightInfo.TabIndex = 2;
            this.lblFlightInfo.Text = "Нислэг сонгоогүй";
            
            // btnUpdateFlightStatus
            this.btnUpdateFlightStatus.BackColor = System.Drawing.Color.Orange;
            this.btnUpdateFlightStatus.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            this.btnUpdateFlightStatus.ForeColor = System.Drawing.Color.White;
            this.btnUpdateFlightStatus.Location = new Point(6, 165);
            this.btnUpdateFlightStatus.Name = "btnUpdateFlightStatus";
            this.btnUpdateFlightStatus.Size = new Size(130, 30);
            this.btnUpdateFlightStatus.TabIndex = 3;
            this.btnUpdateFlightStatus.Text = "Төлөв өөрчлөх";
            this.btnUpdateFlightStatus.UseVisualStyleBackColor = false;
            this.btnUpdateFlightStatus.Click += new EventHandler(this.btnUpdateFlightStatus_Click);
            
            // grpPassengerInfo
            this.grpPassengerInfo.Controls.Add(this.lblPassportNumber);
            this.grpPassengerInfo.Controls.Add(this.txtPassportNumber);
            this.grpPassengerInfo.Controls.Add(this.btnSearchPassenger);
            this.grpPassengerInfo.Controls.Add(this.lblPassengerInfo);
            this.grpPassengerInfo.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.grpPassengerInfo.Location = new Point(410, 40);
            this.grpPassengerInfo.Name = "grpPassengerInfo";
            this.grpPassengerInfo.Size = new Size(380, 200);
            this.grpPassengerInfo.TabIndex = 2;
            this.grpPassengerInfo.TabStop = false;
            this.grpPassengerInfo.Text = "Зорчигчийн мэдээлэл";
            
            // lblPassportNumber
            this.lblPassportNumber.AutoSize = true;
            this.lblPassportNumber.Font = new Font("Microsoft Sans Serif", 9F);
            this.lblPassportNumber.Location = new Point(6, 25);
            this.lblPassportNumber.Name = "lblPassportNumber";
            this.lblPassportNumber.Size = new Size(110, 15);
            this.lblPassportNumber.TabIndex = 0;
            this.lblPassportNumber.Text = "Пасспортын дугаар:";
            
            // txtPassportNumber
            this.txtPassportNumber.Font = new Font("Microsoft Sans Serif", 9F);
            this.txtPassportNumber.Location = new Point(6, 43);
            this.txtPassportNumber.Name = "txtPassportNumber";
            this.txtPassportNumber.Size = new Size(200, 21);
            this.txtPassportNumber.TabIndex = 1;
            
            // btnSearchPassenger
            this.btnSearchPassenger.BackColor = System.Drawing.Color.Blue;
            this.btnSearchPassenger.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            this.btnSearchPassenger.ForeColor = System.Drawing.Color.White;
            this.btnSearchPassenger.Location = new Point(220, 41);
            this.btnSearchPassenger.Name = "btnSearchPassenger";
            this.btnSearchPassenger.Size = new Size(80, 25);
            this.btnSearchPassenger.TabIndex = 2;
            this.btnSearchPassenger.Text = "Хайх";
            this.btnSearchPassenger.UseVisualStyleBackColor = false;
            this.btnSearchPassenger.Click += new EventHandler(this.btnSearchPassenger_Click);
            
            // lblPassengerInfo
            this.lblPassengerInfo.Font = new Font("Microsoft Sans Serif", 9F);
            this.lblPassengerInfo.Location = new Point(6, 75);
            this.lblPassengerInfo.Name = "lblPassengerInfo";
            this.lblPassengerInfo.Size = new Size(360, 80);
            this.lblPassengerInfo.TabIndex = 3;
            this.lblPassengerInfo.Text = "Зорчигч хайгдаагүй";
            
            // grpSeatMap
            this.grpSeatMap.Controls.Add(this.grpFirstClass);
            this.grpSeatMap.Controls.Add(this.grpBusinessClass);
            this.grpSeatMap.Controls.Add(this.grpPremiumEconomyClass);
            this.grpSeatMap.Controls.Add(this.grpEconomyClass);
            this.grpSeatMap.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.grpSeatMap.Location = new Point(12, 250);
            this.grpSeatMap.Name = "grpSeatMap";
            this.grpSeatMap.Size = new Size(780, 450);
            this.grpSeatMap.TabIndex = 3;
            this.grpSeatMap.TabStop = false;
            this.grpSeatMap.Text = "✈️ Нислэгийн суудлын зураглал (Boeing 737)";
            
            // grpFirstClass
            this.grpFirstClass.Controls.Add(this.panelSeats);
            this.grpFirstClass.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold);
            this.grpFirstClass.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            this.grpFirstClass.Location = new Point(10, 25);
            this.grpFirstClass.Name = "grpFirstClass";
            this.grpFirstClass.Size = new Size(760, 80);
            this.grpFirstClass.TabIndex = 0;
            this.grpFirstClass.TabStop = false;
            this.grpFirstClass.Text = "🥇 First Class (Анхны анги) - Rows 1-2";
            
            // grpBusinessClass
            this.grpBusinessClass.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold);
            this.grpBusinessClass.ForeColor = System.Drawing.Color.FromArgb(25, 118, 210);
            this.grpBusinessClass.Location = new Point(10, 115);
            this.grpBusinessClass.Name = "grpBusinessClass";
            this.grpBusinessClass.Size = new Size(760, 80);
            this.grpBusinessClass.TabIndex = 1;
            this.grpBusinessClass.TabStop = false;
            this.grpBusinessClass.Text = "🥈 Business Class (Бизнес анги) - Rows 3-6";
            
            // grpPremiumEconomyClass
            this.grpPremiumEconomyClass.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold);
            this.grpPremiumEconomyClass.ForeColor = System.Drawing.Color.FromArgb(156, 39, 176);
            this.grpPremiumEconomyClass.Location = new Point(10, 205);
            this.grpPremiumEconomyClass.Name = "grpPremiumEconomyClass";
            this.grpPremiumEconomyClass.Size = new Size(760, 80);
            this.grpPremiumEconomyClass.TabIndex = 2;
            this.grpPremiumEconomyClass.TabStop = false;
            this.grpPremiumEconomyClass.Text = "🥉 Premium Economy (Дээд эдийн засгийн анги) - Rows 7-12";
            
            // grpEconomyClass
            this.grpEconomyClass.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold);
            this.grpEconomyClass.ForeColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.grpEconomyClass.Location = new Point(10, 295);
            this.grpEconomyClass.Name = "grpEconomyClass";
            this.grpEconomyClass.Size = new Size(760, 140);
            this.grpEconomyClass.TabIndex = 3;
            this.grpEconomyClass.TabStop = false;
            this.grpEconomyClass.Text = "🛫 Economy Class (Эдийн засгийн анги) - Rows 13-30";
            
            // panelSeats
            this.panelSeats.AutoScroll = true;
            this.panelSeats.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.panelSeats.BorderStyle = BorderStyle.FixedSingle;
            this.panelSeats.Dock = DockStyle.Fill;
            this.panelSeats.Location = new Point(3, 19);
            this.panelSeats.Name = "panelSeats";
            this.panelSeats.Size = new Size(754, 58);
            this.panelSeats.TabIndex = 0;
            
            // lblLog
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.lblLog.Location = new Point(810, 250);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new Size(85, 17);
            this.lblLog.TabIndex = 4;
            this.lblLog.Text = "Үйл явдлын лог";
            
            // txtLog
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.ForeColor = System.Drawing.Color.Lime;
            this.txtLog.Font = new Font("Courier New", 8F);
            this.txtLog.Location = new Point(810, 270);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = ScrollBars.Vertical;
            this.txtLog.Size = new Size(370, 330);
            this.txtLog.TabIndex = 5;
            
            // MainForm
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new Size(1200, 800);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.panelMain);
            this.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Нислэгийн зорчигч бүртгэлийн систем";
            this.WindowState = FormWindowState.Maximized;
            
            this.panelLogin.ResumeLayout(false);
            this.loginContainer.ResumeLayout(false);
            this.loginContainer.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.grpFlightInfo.ResumeLayout(false);
            this.grpFlightInfo.PerformLayout();
            this.grpPassengerInfo.ResumeLayout(false);
            this.grpPassengerInfo.PerformLayout();
            this.grpSeatMap.ResumeLayout(false);
            this.grpBusinessClass.ResumeLayout(false);
            this.grpEconomyClass.ResumeLayout(false);
            this.grpFirstClass.ResumeLayout(false);
            this.grpPremiumEconomyClass.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}