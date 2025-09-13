namespace FlightSystem.CheckinApp
{
    partial class AddFlightForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelMain;
        private GroupBox grpFlightInfo;
        private TextBox txtFlightNumber;
        private Label lblFlightNumber;
        private TextBox txtDepartureAirport;
        private Label lblDepartureAirport;
        private TextBox txtArrivalAirport;
        private Label lblArrivalAirport;
        private TextBox txtGateNumber;
        private Label lblGateNumber;
        private GroupBox grpSchedule;
        private DateTimePicker dtpScheduledDeparture;
        private Label lblScheduledDeparture;
        private DateTimePicker dtpScheduledArrival;
        private Label lblScheduledArrival;
        private GroupBox grpCheckinTimes;
        private DateTimePicker dtpCheckinOpen;
        private Label lblCheckinOpen;
        private DateTimePicker dtpCheckinClose;
        private Label lblCheckinClose;
        private GroupBox grpAircraft;
        private ComboBox cmbAircraft;
        private Label lblAircraft;
        private Button btnCreate;
        private Button btnCancel;

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
            this.btnCancel = new Button();
            this.btnCreate = new Button();
            this.grpAircraft = new GroupBox();
            this.cmbAircraft = new ComboBox();
            this.lblAircraft = new Label();
            this.grpCheckinTimes = new GroupBox();
            this.dtpCheckinClose = new DateTimePicker();
            this.lblCheckinClose = new Label();
            this.dtpCheckinOpen = new DateTimePicker();
            this.lblCheckinOpen = new Label();
            this.grpSchedule = new GroupBox();
            this.dtpScheduledArrival = new DateTimePicker();
            this.lblScheduledArrival = new Label();
            this.dtpScheduledDeparture = new DateTimePicker();
            this.lblScheduledDeparture = new Label();
            this.grpFlightInfo = new GroupBox();
            this.txtGateNumber = new TextBox();
            this.lblGateNumber = new Label();
            this.txtArrivalAirport = new TextBox();
            this.lblArrivalAirport = new Label();
            this.txtDepartureAirport = new TextBox();
            this.lblDepartureAirport = new Label();
            this.txtFlightNumber = new TextBox();
            this.lblFlightNumber = new Label();
            
            this.panelMain.SuspendLayout();
            this.grpAircraft.SuspendLayout();
            this.grpCheckinTimes.SuspendLayout();
            this.grpSchedule.SuspendLayout();
            this.grpFlightInfo.SuspendLayout();
            this.SuspendLayout();
            
            // panelMain
            this.panelMain.Controls.Add(this.btnCancel);
            this.panelMain.Controls.Add(this.btnCreate);
            this.panelMain.Controls.Add(this.grpAircraft);
            this.panelMain.Controls.Add(this.grpCheckinTimes);
            this.panelMain.Controls.Add(this.grpSchedule);
            this.panelMain.Controls.Add(this.grpFlightInfo);
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Location = new Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new Size(600, 700);
            this.panelMain.TabIndex = 0;
            
            // grpFlightInfo
            this.grpFlightInfo.Controls.Add(this.txtGateNumber);
            this.grpFlightInfo.Controls.Add(this.lblGateNumber);
            this.grpFlightInfo.Controls.Add(this.txtArrivalAirport);
            this.grpFlightInfo.Controls.Add(this.lblArrivalAirport);
            this.grpFlightInfo.Controls.Add(this.txtDepartureAirport);
            this.grpFlightInfo.Controls.Add(this.lblDepartureAirport);
            this.grpFlightInfo.Controls.Add(this.txtFlightNumber);
            this.grpFlightInfo.Controls.Add(this.lblFlightNumber);
            this.grpFlightInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpFlightInfo.Location = new Point(20, 20);
            this.grpFlightInfo.Name = "grpFlightInfo";
            this.grpFlightInfo.Size = new Size(560, 180);
            this.grpFlightInfo.TabIndex = 0;
            this.grpFlightInfo.TabStop = false;
            this.grpFlightInfo.Text = "Flight Information";
            
            // lblFlightNumber
            this.lblFlightNumber.AutoSize = true;
            this.lblFlightNumber.Font = new Font("Segoe UI", 10F);
            this.lblFlightNumber.Location = new Point(20, 30);
            this.lblFlightNumber.Name = "lblFlightNumber";
            this.lblFlightNumber.Size = new Size(90, 19);
            this.lblFlightNumber.TabIndex = 0;
            this.lblFlightNumber.Text = "Flight Number:";
            
            // txtFlightNumber
            this.txtFlightNumber.Font = new Font("Segoe UI", 10F);
            this.txtFlightNumber.Location = new Point(130, 28);
            this.txtFlightNumber.Name = "txtFlightNumber";
            this.txtFlightNumber.Size = new Size(150, 25);
            this.txtFlightNumber.TabIndex = 1;
            
            // lblDepartureAirport
            this.lblDepartureAirport.AutoSize = true;
            this.lblDepartureAirport.Font = new Font("Segoe UI", 10F);
            this.lblDepartureAirport.Location = new Point(20, 65);
            this.lblDepartureAirport.Name = "lblDepartureAirport";
            this.lblDepartureAirport.Size = new Size(115, 19);
            this.lblDepartureAirport.TabIndex = 2;
            this.lblDepartureAirport.Text = "Departure Airport:";
            
            // txtDepartureAirport
            this.txtDepartureAirport.Font = new Font("Segoe UI", 10F);
            this.txtDepartureAirport.Location = new Point(150, 63);
            this.txtDepartureAirport.Name = "txtDepartureAirport";
            this.txtDepartureAirport.Size = new Size(130, 25);
            this.txtDepartureAirport.TabIndex = 3;
            
            // lblArrivalAirport
            this.lblArrivalAirport.AutoSize = true;
            this.lblArrivalAirport.Font = new Font("Segoe UI", 10F);
            this.lblArrivalAirport.Location = new Point(300, 65);
            this.lblArrivalAirport.Name = "lblArrivalAirport";
            this.lblArrivalAirport.Size = new Size(95, 19);
            this.lblArrivalAirport.TabIndex = 4;
            this.lblArrivalAirport.Text = "Arrival Airport:";
            
            // txtArrivalAirport
            this.txtArrivalAirport.Font = new Font("Segoe UI", 10F);
            this.txtArrivalAirport.Location = new Point(410, 63);
            this.txtArrivalAirport.Name = "txtArrivalAirport";
            this.txtArrivalAirport.Size = new Size(130, 25);
            this.txtArrivalAirport.TabIndex = 5;
            
            // lblGateNumber
            this.lblGateNumber.AutoSize = true;
            this.lblGateNumber.Font = new Font("Segoe UI", 10F);
            this.lblGateNumber.Location = new Point(20, 100);
            this.lblGateNumber.Name = "lblGateNumber";
            this.lblGateNumber.Size = new Size(90, 19);
            this.lblGateNumber.TabIndex = 6;
            this.lblGateNumber.Text = "Gate Number:";
            
            // txtGateNumber
            this.txtGateNumber.Font = new Font("Segoe UI", 10F);
            this.txtGateNumber.Location = new Point(130, 98);
            this.txtGateNumber.Name = "txtGateNumber";
            this.txtGateNumber.Size = new Size(100, 25);
            this.txtGateNumber.TabIndex = 7;
            
            // grpSchedule
            this.grpSchedule.Controls.Add(this.dtpScheduledArrival);
            this.grpSchedule.Controls.Add(this.lblScheduledArrival);
            this.grpSchedule.Controls.Add(this.dtpScheduledDeparture);
            this.grpSchedule.Controls.Add(this.lblScheduledDeparture);
            this.grpSchedule.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpSchedule.Location = new Point(20, 220);
            this.grpSchedule.Name = "grpSchedule";
            this.grpSchedule.Size = new Size(560, 100);
            this.grpSchedule.TabIndex = 1;
            this.grpSchedule.TabStop = false;
            this.grpSchedule.Text = "Schedule";
            
            // lblScheduledDeparture
            this.lblScheduledDeparture.AutoSize = true;
            this.lblScheduledDeparture.Font = new Font("Segoe UI", 10F);
            this.lblScheduledDeparture.Location = new Point(20, 35);
            this.lblScheduledDeparture.Name = "lblScheduledDeparture";
            this.lblScheduledDeparture.Size = new Size(135, 19);
            this.lblScheduledDeparture.TabIndex = 0;
            this.lblScheduledDeparture.Text = "Scheduled Departure:";
            
            // dtpScheduledDeparture
            this.dtpScheduledDeparture.Font = new Font("Segoe UI", 10F);
            this.dtpScheduledDeparture.Format = DateTimePickerFormat.Custom;
            this.dtpScheduledDeparture.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpScheduledDeparture.Location = new Point(170, 33);
            this.dtpScheduledDeparture.Name = "dtpScheduledDeparture";
            this.dtpScheduledDeparture.ShowUpDown = true;
            this.dtpScheduledDeparture.Size = new Size(150, 25);
            this.dtpScheduledDeparture.TabIndex = 1;
            
            // lblScheduledArrival
            this.lblScheduledArrival.AutoSize = true;
            this.lblScheduledArrival.Font = new Font("Segoe UI", 10F);
            this.lblScheduledArrival.Location = new Point(340, 35);
            this.lblScheduledArrival.Name = "lblScheduledArrival";
            this.lblScheduledArrival.Size = new Size(115, 19);
            this.lblScheduledArrival.TabIndex = 2;
            this.lblScheduledArrival.Text = "Scheduled Arrival:";
            
            // dtpScheduledArrival
            this.dtpScheduledArrival.Font = new Font("Segoe UI", 10F);
            this.dtpScheduledArrival.Format = DateTimePickerFormat.Custom;
            this.dtpScheduledArrival.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpScheduledArrival.Location = new Point(470, 33);
            this.dtpScheduledArrival.Name = "dtpScheduledArrival";
            this.dtpScheduledArrival.ShowUpDown = true;
            this.dtpScheduledArrival.Size = new Size(150, 25);
            this.dtpScheduledArrival.TabIndex = 3;
            
            // grpCheckinTimes
            this.grpCheckinTimes.Controls.Add(this.dtpCheckinClose);
            this.grpCheckinTimes.Controls.Add(this.lblCheckinClose);
            this.grpCheckinTimes.Controls.Add(this.dtpCheckinOpen);
            this.grpCheckinTimes.Controls.Add(this.lblCheckinOpen);
            this.grpCheckinTimes.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpCheckinTimes.Location = new Point(20, 340);
            this.grpCheckinTimes.Name = "grpCheckinTimes";
            this.grpCheckinTimes.Size = new Size(560, 100);
            this.grpCheckinTimes.TabIndex = 2;
            this.grpCheckinTimes.TabStop = false;
            this.grpCheckinTimes.Text = "Check-in Times";
            
            // lblCheckinOpen
            this.lblCheckinOpen.AutoSize = true;
            this.lblCheckinOpen.Font = new Font("Segoe UI", 10F);
            this.lblCheckinOpen.Location = new Point(20, 35);
            this.lblCheckinOpen.Name = "lblCheckinOpen";
            this.lblCheckinOpen.Size = new Size(90, 19);
            this.lblCheckinOpen.TabIndex = 0;
            this.lblCheckinOpen.Text = "Check-in Open:";
            
            // dtpCheckinOpen
            this.dtpCheckinOpen.Font = new Font("Segoe UI", 10F);
            this.dtpCheckinOpen.Format = DateTimePickerFormat.Custom;
            this.dtpCheckinOpen.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpCheckinOpen.Location = new Point(130, 33);
            this.dtpCheckinOpen.Name = "dtpCheckinOpen";
            this.dtpCheckinOpen.ShowUpDown = true;
            this.dtpCheckinOpen.Size = new Size(150, 25);
            this.dtpCheckinOpen.TabIndex = 1;
            
            // lblCheckinClose
            this.lblCheckinClose.AutoSize = true;
            this.lblCheckinClose.Font = new Font("Segoe UI", 10F);
            this.lblCheckinClose.Location = new Point(300, 35);
            this.lblCheckinClose.Name = "lblCheckinClose";
            this.lblCheckinClose.Size = new Size(95, 19);
            this.lblCheckinClose.TabIndex = 2;
            this.lblCheckinClose.Text = "Check-in Close:";
            
            // dtpCheckinClose
            this.dtpCheckinClose.Font = new Font("Segoe UI", 10F);
            this.dtpCheckinClose.Format = DateTimePickerFormat.Custom;
            this.dtpCheckinClose.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpCheckinClose.Location = new Point(410, 33);
            this.dtpCheckinClose.Name = "dtpCheckinClose";
            this.dtpCheckinClose.ShowUpDown = true;
            this.dtpCheckinClose.Size = new Size(150, 25);
            this.dtpCheckinClose.TabIndex = 3;
            
            // grpAircraft
            this.grpAircraft.Controls.Add(this.cmbAircraft);
            this.grpAircraft.Controls.Add(this.lblAircraft);
            this.grpAircraft.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpAircraft.Location = new Point(20, 460);
            this.grpAircraft.Name = "grpAircraft";
            this.grpAircraft.Size = new Size(560, 80);
            this.grpAircraft.TabIndex = 3;
            this.grpAircraft.TabStop = false;
            this.grpAircraft.Text = "Aircraft";
            
            // lblAircraft
            this.lblAircraft.AutoSize = true;
            this.lblAircraft.Font = new Font("Segoe UI", 10F);
            this.lblAircraft.Location = new Point(20, 35);
            this.lblAircraft.Name = "lblAircraft";
            this.lblAircraft.Size = new Size(55, 19);
            this.lblAircraft.TabIndex = 0;
            this.lblAircraft.Text = "Aircraft:";
            
            // cmbAircraft
            this.cmbAircraft.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbAircraft.Font = new Font("Segoe UI", 10F);
            this.cmbAircraft.FormattingEnabled = true;
            this.cmbAircraft.Location = new Point(90, 32);
            this.cmbAircraft.Name = "cmbAircraft";
            this.cmbAircraft.Size = new Size(450, 25);
            this.cmbAircraft.TabIndex = 1;
            
            // btnCreate
            this.btnCreate.BackColor = Color.FromArgb(76, 175, 80);
            this.btnCreate.FlatAppearance.BorderSize = 0;
            this.btnCreate.FlatStyle = FlatStyle.Flat;
            this.btnCreate.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnCreate.ForeColor = Color.White;
            this.btnCreate.Location = new Point(350, 600);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new Size(100, 40);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new EventHandler(this.btnCreate_Click);
            
            // btnCancel
            this.btnCancel.BackColor = Color.FromArgb(158, 158, 158);
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(470, 600);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(100, 40);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            
            // AddFlightForm
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(600, 700);
            this.Controls.Add(this.panelMain);
            this.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Name = "AddFlightForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Add New Flight";
            
            this.panelMain.ResumeLayout(false);
            this.grpAircraft.ResumeLayout(false);
            this.grpAircraft.PerformLayout();
            this.grpCheckinTimes.ResumeLayout(false);
            this.grpCheckinTimes.PerformLayout();
            this.grpSchedule.ResumeLayout(false);
            this.grpSchedule.PerformLayout();
            this.grpFlightInfo.ResumeLayout(false);
            this.grpFlightInfo.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
