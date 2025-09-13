namespace FlightSystem.CheckinApp
{
    partial class CheckinMainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelMain;
        private Label lblWelcome;
        private Label lblFlightInfo;
        private GroupBox grpPassengerInfo;
        private Label lblPassportNumber;
        private TextBox txtPassportNumber;
        private Button btnSearchPassenger;
        private Label lblPassengerInfo;
        private Label lblBookingStatus;
        private Button btnLoadSeatMap;
        private GroupBox grpSeatMap;
        private Panel panelSeats;
        private GroupBox grpBusinessClass;
        private GroupBox grpPremiumEconomyClass;
        private GroupBox grpEconomyClass;
        private Button btnUpdateFlightStatus;
        private Button btnLogout;
        private TextBox txtLog;
        private Label lblLog;

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
            this.lblLog = new Label();
            this.txtLog = new TextBox();
            this.btnLogout = new Button();
            this.btnUpdateFlightStatus = new Button();
            this.grpSeatMap = new GroupBox();
            this.grpEconomyClass = new GroupBox();
            this.grpPremiumEconomyClass = new GroupBox();
            this.grpBusinessClass = new GroupBox();
            this.panelSeats = new Panel();
            this.btnLoadSeatMap = new Button();
            this.lblBookingStatus = new Label();
            this.grpPassengerInfo = new GroupBox();
            this.lblPassengerInfo = new Label();
            this.btnSearchPassenger = new Button();
            this.txtPassportNumber = new TextBox();
            this.lblPassportNumber = new Label();
            this.lblFlightInfo = new Label();
            this.lblWelcome = new Label();
            
            this.panelMain.SuspendLayout();
            this.grpSeatMap.SuspendLayout();
            this.grpEconomyClass.SuspendLayout();
            this.grpPremiumEconomyClass.SuspendLayout();
            this.grpBusinessClass.SuspendLayout();
            this.grpPassengerInfo.SuspendLayout();
            this.SuspendLayout();
            
            // panelMain
            this.panelMain.Controls.Add(this.lblLog);
            this.panelMain.Controls.Add(this.txtLog);
            this.panelMain.Controls.Add(this.btnLogout);
            this.panelMain.Controls.Add(this.btnUpdateFlightStatus);
            this.panelMain.Controls.Add(this.grpSeatMap);
            this.panelMain.Controls.Add(this.btnLoadSeatMap);
            this.panelMain.Controls.Add(this.lblBookingStatus);
            this.panelMain.Controls.Add(this.grpPassengerInfo);
            this.panelMain.Controls.Add(this.lblFlightInfo);
            this.panelMain.Controls.Add(this.lblWelcome);
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Location = new Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new Size(1400, 900);
            this.panelMain.TabIndex = 0;
            
            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.Blue;
            this.lblWelcome.Location = new Point(20, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new Size(200, 24);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Тавтай морил";
            
            // lblFlightInfo
            this.lblFlightInfo.Font = new Font("Microsoft Sans Serif", 10F);
            this.lblFlightInfo.Location = new Point(20, 60);
            this.lblFlightInfo.Name = "lblFlightInfo";
            this.lblFlightInfo.Size = new Size(400, 100);
            this.lblFlightInfo.TabIndex = 1;
            this.lblFlightInfo.Text = "Нислэгийн мэдээлэл";
            
            // grpPassengerInfo
            this.grpPassengerInfo.Controls.Add(this.lblPassengerInfo);
            this.grpPassengerInfo.Controls.Add(this.btnSearchPassenger);
            this.grpPassengerInfo.Controls.Add(this.txtPassportNumber);
            this.grpPassengerInfo.Controls.Add(this.lblPassportNumber);
            this.grpPassengerInfo.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.grpPassengerInfo.Location = new Point(450, 20);
            this.grpPassengerInfo.Name = "grpPassengerInfo";
            this.grpPassengerInfo.Size = new Size(400, 200);
            this.grpPassengerInfo.TabIndex = 2;
            this.grpPassengerInfo.TabStop = false;
            this.grpPassengerInfo.Text = "Зорчигчийн мэдээлэл";
            
            // lblPassportNumber
            this.lblPassportNumber.AutoSize = true;
            this.lblPassportNumber.Font = new Font("Microsoft Sans Serif", 9F);
            this.lblPassportNumber.Location = new Point(20, 30);
            this.lblPassportNumber.Name = "lblPassportNumber";
            this.lblPassportNumber.Size = new Size(110, 15);
            this.lblPassportNumber.TabIndex = 0;
            this.lblPassportNumber.Text = "Пасспортын дугаар:";
            
            // txtPassportNumber
            this.txtPassportNumber.Font = new Font("Microsoft Sans Serif", 9F);
            this.txtPassportNumber.Location = new Point(20, 50);
            this.txtPassportNumber.Name = "txtPassportNumber";
            this.txtPassportNumber.Size = new Size(200, 21);
            this.txtPassportNumber.TabIndex = 1;
            
            // btnSearchPassenger
            this.btnSearchPassenger.BackColor = System.Drawing.Color.Blue;
            this.btnSearchPassenger.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            this.btnSearchPassenger.ForeColor = System.Drawing.Color.White;
            this.btnSearchPassenger.Location = new Point(230, 48);
            this.btnSearchPassenger.Name = "btnSearchPassenger";
            this.btnSearchPassenger.Size = new Size(80, 25);
            this.btnSearchPassenger.TabIndex = 2;
            this.btnSearchPassenger.Text = "Хайх";
            this.btnSearchPassenger.UseVisualStyleBackColor = false;
            this.btnSearchPassenger.Click += new EventHandler(this.btnSearchPassenger_Click);
            
            // lblPassengerInfo
            this.lblPassengerInfo.Font = new Font("Microsoft Sans Serif", 9F);
            this.lblPassengerInfo.Location = new Point(20, 85);
            this.lblPassengerInfo.Name = "lblPassengerInfo";
            this.lblPassengerInfo.Size = new Size(360, 100);
            this.lblPassengerInfo.TabIndex = 3;
            this.lblPassengerInfo.Text = "Зорчигч хайгдаагүй";
            
            // lblBookingStatus
            this.lblBookingStatus.AutoSize = true;
            this.lblBookingStatus.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.lblBookingStatus.Location = new Point(450, 240);
            this.lblBookingStatus.Name = "lblBookingStatus";
            this.lblBookingStatus.Size = new Size(150, 17);
            this.lblBookingStatus.TabIndex = 3;
            this.lblBookingStatus.Text = "Бүртгэлийн статус";
            
            // btnLoadSeatMap
            this.btnLoadSeatMap.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnLoadSeatMap.Enabled = false;
            this.btnLoadSeatMap.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.btnLoadSeatMap.ForeColor = System.Drawing.Color.White;
            this.btnLoadSeatMap.Location = new Point(450, 270);
            this.btnLoadSeatMap.Name = "btnLoadSeatMap";
            this.btnLoadSeatMap.Size = new Size(150, 30);
            this.btnLoadSeatMap.TabIndex = 4;
            this.btnLoadSeatMap.Text = "Суудлын зураглал";
            this.btnLoadSeatMap.UseVisualStyleBackColor = false;
            this.btnLoadSeatMap.Click += new EventHandler(this.btnLoadSeatMap_Click);
            
            // grpSeatMap
            this.grpSeatMap.Controls.Add(this.grpEconomyClass);
            this.grpSeatMap.Controls.Add(this.grpPremiumEconomyClass);
            this.grpSeatMap.Controls.Add(this.grpBusinessClass);
            this.grpSeatMap.Controls.Add(this.panelSeats);
            this.grpSeatMap.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.grpSeatMap.Location = new Point(20, 320);
            this.grpSeatMap.Name = "grpSeatMap";
            this.grpSeatMap.Size = new Size(1000, 500);
            this.grpSeatMap.TabIndex = 5;
            this.grpSeatMap.TabStop = false;
            this.grpSeatMap.Text = "✈️ Нислэгийн суудлын зураглал (Boeing 737)";
            
            // panelSeats
            this.panelSeats.AutoScroll = true;
            this.panelSeats.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.panelSeats.BorderStyle = BorderStyle.FixedSingle;
            this.panelSeats.Dock = DockStyle.Fill;
            this.panelSeats.Location = new Point(3, 19);
            this.panelSeats.Name = "panelSeats";
            this.panelSeats.Size = new Size(994, 478);
            this.panelSeats.TabIndex = 0;
            
            // grpBusinessClass
            this.grpBusinessClass.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold);
            this.grpBusinessClass.ForeColor = System.Drawing.Color.FromArgb(25, 118, 210);
            this.grpBusinessClass.Location = new Point(20, 50);
            this.grpBusinessClass.Name = "grpBusinessClass";
            this.grpBusinessClass.Size = new Size(960, 80);
            this.grpBusinessClass.TabIndex = 1;
            this.grpBusinessClass.TabStop = false;
            this.grpBusinessClass.Text = "🥈 Business Class (Бизнес анги) - Rows 3-6";
            
            // grpPremiumEconomyClass
            this.grpPremiumEconomyClass.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold);
            this.grpPremiumEconomyClass.ForeColor = System.Drawing.Color.FromArgb(156, 39, 176);
            this.grpPremiumEconomyClass.Location = new Point(20, 140);
            this.grpPremiumEconomyClass.Name = "grpPremiumEconomyClass";
            this.grpPremiumEconomyClass.Size = new Size(960, 80);
            this.grpPremiumEconomyClass.TabIndex = 2;
            this.grpPremiumEconomyClass.TabStop = false;
            this.grpPremiumEconomyClass.Text = "🥉 Premium Economy (Дээд эдийн засгийн анги) - Rows 7-12";
            
            // grpEconomyClass
            this.grpEconomyClass.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold);
            this.grpEconomyClass.ForeColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.grpEconomyClass.Location = new Point(20, 230);
            this.grpEconomyClass.Name = "grpEconomyClass";
            this.grpEconomyClass.Size = new Size(960, 240);
            this.grpEconomyClass.TabIndex = 3;
            this.grpEconomyClass.TabStop = false;
            this.grpEconomyClass.Text = "🛫 Economy Class (Эдийн засгийн анги) - Rows 13-30";
            
            // btnUpdateFlightStatus
            this.btnUpdateFlightStatus.BackColor = System.Drawing.Color.Orange;
            this.btnUpdateFlightStatus.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.btnUpdateFlightStatus.ForeColor = System.Drawing.Color.White;
            this.btnUpdateFlightStatus.Location = new Point(880, 60);
            this.btnUpdateFlightStatus.Name = "btnUpdateFlightStatus";
            this.btnUpdateFlightStatus.Size = new Size(130, 30);
            this.btnUpdateFlightStatus.TabIndex = 6;
            this.btnUpdateFlightStatus.Text = "Төлөв өөрчлөх";
            this.btnUpdateFlightStatus.UseVisualStyleBackColor = false;
            this.btnUpdateFlightStatus.Click += new EventHandler(this.btnUpdateFlightStatus_Click);
            
            // btnLogout
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnLogout.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new Point(880, 100);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new Size(130, 30);
            this.btnLogout.TabIndex = 7;
            this.btnLogout.Text = "Гарах";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new EventHandler(this.btnLogout_Click);
            
            // txtLog
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.ForeColor = System.Drawing.Color.Lime;
            this.txtLog.Font = new Font("Courier New", 8F);
            this.txtLog.Location = new Point(1050, 320);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = ScrollBars.Vertical;
            this.txtLog.Size = new Size(330, 500);
            this.txtLog.TabIndex = 8;
            
            // lblLog
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.lblLog.Location = new Point(1050, 300);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new Size(85, 17);
            this.lblLog.TabIndex = 9;
            this.lblLog.Text = "Үйл явдлын лог";
            
            // CheckinMainForm
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new Size(1400, 900);
            this.Controls.Add(this.panelMain);
            this.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Name = "CheckinMainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Нислэгийн зорчигч бүртгэлийн систем";
            this.WindowState = FormWindowState.Maximized;
            
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.grpSeatMap.ResumeLayout(false);
            this.grpEconomyClass.ResumeLayout(false);
            this.grpPremiumEconomyClass.ResumeLayout(false);
            this.grpBusinessClass.ResumeLayout(false);
            this.grpPassengerInfo.ResumeLayout(false);
            this.grpPassengerInfo.PerformLayout();
            this.ResumeLayout(false);
        }

        private void btnLoadSeatMap_Click(object sender, EventArgs e)
        {
            _ = LoadSeatMap();
        }
    }
}
