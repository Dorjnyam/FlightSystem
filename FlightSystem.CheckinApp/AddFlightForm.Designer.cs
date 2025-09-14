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
            panelMain = new Panel();
            btnCancel = new Button();
            btnCreate = new Button();
            grpAircraft = new GroupBox();
            cmbAircraft = new ComboBox();
            lblAircraft = new Label();
            grpCheckinTimes = new GroupBox();
            dtpCheckinClose = new DateTimePicker();
            lblCheckinClose = new Label();
            dtpCheckinOpen = new DateTimePicker();
            lblCheckinOpen = new Label();
            grpSchedule = new GroupBox();
            dtpScheduledArrival = new DateTimePicker();
            lblScheduledArrival = new Label();
            dtpScheduledDeparture = new DateTimePicker();
            lblScheduledDeparture = new Label();
            grpFlightInfo = new GroupBox();
            txtGateNumber = new TextBox();
            lblGateNumber = new Label();
            txtArrivalAirport = new TextBox();
            lblArrivalAirport = new Label();
            txtDepartureAirport = new TextBox();
            lblDepartureAirport = new Label();
            txtFlightNumber = new TextBox();
            lblFlightNumber = new Label();
            panelMain.SuspendLayout();
            grpAircraft.SuspendLayout();
            grpCheckinTimes.SuspendLayout();
            grpSchedule.SuspendLayout();
            grpFlightInfo.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(btnCancel);
            panelMain.Controls.Add(btnCreate);
            panelMain.Controls.Add(grpAircraft);
            panelMain.Controls.Add(grpCheckinTimes);
            panelMain.Controls.Add(grpSchedule);
            panelMain.Controls.Add(grpFlightInfo);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(648, 700);
            panelMain.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(158, 158, 158);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(470, 600);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 40);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.FromArgb(76, 175, 80);
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCreate.ForeColor = Color.White;
            btnCreate.Location = new Point(350, 600);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(100, 40);
            btnCreate.TabIndex = 4;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += btnCreate_Click;
            // 
            // grpAircraft
            // 
            grpAircraft.Controls.Add(cmbAircraft);
            grpAircraft.Controls.Add(lblAircraft);
            grpAircraft.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpAircraft.Location = new Point(20, 460);
            grpAircraft.Name = "grpAircraft";
            grpAircraft.Size = new Size(616, 80);
            grpAircraft.TabIndex = 3;
            grpAircraft.TabStop = false;
            grpAircraft.Text = "Aircraft";
            // 
            // cmbAircraft
            // 
            cmbAircraft.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAircraft.Font = new Font("Segoe UI", 10F);
            cmbAircraft.FormattingEnabled = true;
            cmbAircraft.Location = new Point(90, 32);
            cmbAircraft.Name = "cmbAircraft";
            cmbAircraft.Size = new Size(450, 25);
            cmbAircraft.TabIndex = 1;
            // 
            // lblAircraft
            // 
            lblAircraft.AutoSize = true;
            lblAircraft.Font = new Font("Segoe UI", 10F);
            lblAircraft.Location = new Point(20, 35);
            lblAircraft.Name = "lblAircraft";
            lblAircraft.Size = new Size(56, 19);
            lblAircraft.TabIndex = 0;
            lblAircraft.Text = "Aircraft:";
            // 
            // grpCheckinTimes
            // 
            grpCheckinTimes.Controls.Add(dtpCheckinClose);
            grpCheckinTimes.Controls.Add(lblCheckinClose);
            grpCheckinTimes.Controls.Add(dtpCheckinOpen);
            grpCheckinTimes.Controls.Add(lblCheckinOpen);
            grpCheckinTimes.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpCheckinTimes.Location = new Point(20, 340);
            grpCheckinTimes.Name = "grpCheckinTimes";
            grpCheckinTimes.Size = new Size(616, 100);
            grpCheckinTimes.TabIndex = 2;
            grpCheckinTimes.TabStop = false;
            grpCheckinTimes.Text = "Check-in Times";
            // 
            // dtpCheckinClose
            // 
            dtpCheckinClose.CustomFormat = "yyyy-MM-dd HH:mm";
            dtpCheckinClose.Font = new Font("Segoe UI", 10F);
            dtpCheckinClose.Format = DateTimePickerFormat.Custom;
            dtpCheckinClose.Location = new Point(410, 33);
            dtpCheckinClose.Name = "dtpCheckinClose";
            dtpCheckinClose.ShowUpDown = true;
            dtpCheckinClose.Size = new Size(150, 25);
            dtpCheckinClose.TabIndex = 3;
            // 
            // lblCheckinClose
            // 
            lblCheckinClose.AutoSize = true;
            lblCheckinClose.Font = new Font("Segoe UI", 10F);
            lblCheckinClose.Location = new Point(300, 35);
            lblCheckinClose.Name = "lblCheckinClose";
            lblCheckinClose.Size = new Size(103, 19);
            lblCheckinClose.TabIndex = 2;
            lblCheckinClose.Text = "Check-in Close:";
            // 
            // dtpCheckinOpen
            // 
            dtpCheckinOpen.CustomFormat = "yyyy-MM-dd HH:mm";
            dtpCheckinOpen.Font = new Font("Segoe UI", 10F);
            dtpCheckinOpen.Format = DateTimePickerFormat.Custom;
            dtpCheckinOpen.Location = new Point(130, 33);
            dtpCheckinOpen.Name = "dtpCheckinOpen";
            dtpCheckinOpen.ShowUpDown = true;
            dtpCheckinOpen.Size = new Size(150, 25);
            dtpCheckinOpen.TabIndex = 1;
            // 
            // lblCheckinOpen
            // 
            lblCheckinOpen.AutoSize = true;
            lblCheckinOpen.Font = new Font("Segoe UI", 10F);
            lblCheckinOpen.Location = new Point(20, 35);
            lblCheckinOpen.Name = "lblCheckinOpen";
            lblCheckinOpen.Size = new Size(104, 19);
            lblCheckinOpen.TabIndex = 0;
            lblCheckinOpen.Text = "Check-in Open:";
            // 
            // grpSchedule
            // 
            grpSchedule.Controls.Add(dtpScheduledArrival);
            grpSchedule.Controls.Add(lblScheduledArrival);
            grpSchedule.Controls.Add(dtpScheduledDeparture);
            grpSchedule.Controls.Add(lblScheduledDeparture);
            grpSchedule.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpSchedule.Location = new Point(20, 220);
            grpSchedule.Name = "grpSchedule";
            grpSchedule.Size = new Size(616, 100);
            grpSchedule.TabIndex = 1;
            grpSchedule.TabStop = false;
            grpSchedule.Text = "Schedule";
            // 
            // dtpScheduledArrival
            // 
            dtpScheduledArrival.CustomFormat = "yyyy-MM-dd HH:mm";
            dtpScheduledArrival.Font = new Font("Segoe UI", 10F);
            dtpScheduledArrival.Format = DateTimePickerFormat.Custom;
            dtpScheduledArrival.Location = new Point(470, 33);
            dtpScheduledArrival.Name = "dtpScheduledArrival";
            dtpScheduledArrival.ShowUpDown = true;
            dtpScheduledArrival.Size = new Size(150, 25);
            dtpScheduledArrival.TabIndex = 3;
            // 
            // lblScheduledArrival
            // 
            lblScheduledArrival.AutoSize = true;
            lblScheduledArrival.Font = new Font("Segoe UI", 10F);
            lblScheduledArrival.Location = new Point(340, 35);
            lblScheduledArrival.Name = "lblScheduledArrival";
            lblScheduledArrival.Size = new Size(117, 19);
            lblScheduledArrival.TabIndex = 2;
            lblScheduledArrival.Text = "Scheduled Arrival:";
            // 
            // dtpScheduledDeparture
            // 
            dtpScheduledDeparture.CustomFormat = "yyyy-MM-dd HH:mm";
            dtpScheduledDeparture.Font = new Font("Segoe UI", 10F);
            dtpScheduledDeparture.Format = DateTimePickerFormat.Custom;
            dtpScheduledDeparture.Location = new Point(170, 33);
            dtpScheduledDeparture.Name = "dtpScheduledDeparture";
            dtpScheduledDeparture.ShowUpDown = true;
            dtpScheduledDeparture.Size = new Size(150, 25);
            dtpScheduledDeparture.TabIndex = 1;
            // 
            // lblScheduledDeparture
            // 
            lblScheduledDeparture.AutoSize = true;
            lblScheduledDeparture.Font = new Font("Segoe UI", 10F);
            lblScheduledDeparture.Location = new Point(20, 35);
            lblScheduledDeparture.Name = "lblScheduledDeparture";
            lblScheduledDeparture.Size = new Size(140, 19);
            lblScheduledDeparture.TabIndex = 0;
            lblScheduledDeparture.Text = "Scheduled Departure:";
            // 
            // grpFlightInfo
            // 
            grpFlightInfo.Controls.Add(txtGateNumber);
            grpFlightInfo.Controls.Add(lblGateNumber);
            grpFlightInfo.Controls.Add(txtArrivalAirport);
            grpFlightInfo.Controls.Add(lblArrivalAirport);
            grpFlightInfo.Controls.Add(txtDepartureAirport);
            grpFlightInfo.Controls.Add(lblDepartureAirport);
            grpFlightInfo.Controls.Add(txtFlightNumber);
            grpFlightInfo.Controls.Add(lblFlightNumber);
            grpFlightInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpFlightInfo.Location = new Point(20, 20);
            grpFlightInfo.Name = "grpFlightInfo";
            grpFlightInfo.Size = new Size(616, 180);
            grpFlightInfo.TabIndex = 0;
            grpFlightInfo.TabStop = false;
            grpFlightInfo.Text = "Flight Information";
            // 
            // txtGateNumber
            // 
            txtGateNumber.Font = new Font("Segoe UI", 10F);
            txtGateNumber.Location = new Point(130, 98);
            txtGateNumber.Name = "txtGateNumber";
            txtGateNumber.Size = new Size(100, 25);
            txtGateNumber.TabIndex = 7;
            // 
            // lblGateNumber
            // 
            lblGateNumber.AutoSize = true;
            lblGateNumber.Font = new Font("Segoe UI", 10F);
            lblGateNumber.Location = new Point(20, 100);
            lblGateNumber.Name = "lblGateNumber";
            lblGateNumber.Size = new Size(95, 19);
            lblGateNumber.TabIndex = 6;
            lblGateNumber.Text = "Gate Number:";
            // 
            // txtArrivalAirport
            // 
            txtArrivalAirport.Font = new Font("Segoe UI", 10F);
            txtArrivalAirport.Location = new Point(410, 63);
            txtArrivalAirport.Name = "txtArrivalAirport";
            txtArrivalAirport.Size = new Size(130, 25);
            txtArrivalAirport.TabIndex = 5;
            // 
            // lblArrivalAirport
            // 
            lblArrivalAirport.AutoSize = true;
            lblArrivalAirport.Font = new Font("Segoe UI", 10F);
            lblArrivalAirport.Location = new Point(300, 65);
            lblArrivalAirport.Name = "lblArrivalAirport";
            lblArrivalAirport.Size = new Size(98, 19);
            lblArrivalAirport.TabIndex = 4;
            lblArrivalAirport.Text = "Arrival Airport:";
            // 
            // txtDepartureAirport
            // 
            txtDepartureAirport.Font = new Font("Segoe UI", 10F);
            txtDepartureAirport.Location = new Point(150, 63);
            txtDepartureAirport.Name = "txtDepartureAirport";
            txtDepartureAirport.Size = new Size(130, 25);
            txtDepartureAirport.TabIndex = 3;
            // 
            // lblDepartureAirport
            // 
            lblDepartureAirport.AutoSize = true;
            lblDepartureAirport.Font = new Font("Segoe UI", 10F);
            lblDepartureAirport.Location = new Point(20, 65);
            lblDepartureAirport.Name = "lblDepartureAirport";
            lblDepartureAirport.Size = new Size(121, 19);
            lblDepartureAirport.TabIndex = 2;
            lblDepartureAirport.Text = "Departure Airport:";
            // 
            // txtFlightNumber
            // 
            txtFlightNumber.Font = new Font("Segoe UI", 10F);
            txtFlightNumber.Location = new Point(130, 28);
            txtFlightNumber.Name = "txtFlightNumber";
            txtFlightNumber.Size = new Size(150, 25);
            txtFlightNumber.TabIndex = 1;
            // 
            // lblFlightNumber
            // 
            lblFlightNumber.AutoSize = true;
            lblFlightNumber.Font = new Font("Segoe UI", 10F);
            lblFlightNumber.Location = new Point(20, 30);
            lblFlightNumber.Name = "lblFlightNumber";
            lblFlightNumber.Size = new Size(100, 19);
            lblFlightNumber.TabIndex = 0;
            lblFlightNumber.Text = "Flight Number:";
            // 
            // AddFlightForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(648, 700);
            Controls.Add(panelMain);
            Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "AddFlightForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add New Flight";
            panelMain.ResumeLayout(false);
            grpAircraft.ResumeLayout(false);
            grpAircraft.PerformLayout();
            grpCheckinTimes.ResumeLayout(false);
            grpCheckinTimes.PerformLayout();
            grpSchedule.ResumeLayout(false);
            grpSchedule.PerformLayout();
            grpFlightInfo.ResumeLayout(false);
            grpFlightInfo.PerformLayout();
            ResumeLayout(false);
        }
    }
}
