namespace FlightSystem.CheckinApp
{
    partial class FlightPassengerManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelMain;
        private Label lblWelcome;
        private Label lblStatus;
        private Button btnRefresh;
        private Button btnClose;
        private ListView lstFlightPassengers;
        private ColumnHeader colBookingReference;
        private ColumnHeader colFlightNumber;
        private ColumnHeader colPassengerName;
        private ColumnHeader colPassport;
        private ColumnHeader colCheckedIn;
        private ColumnHeader colDepartureDate;
        private GroupBox grpDetails;
        private TextBox txtDetails;
        private GroupBox grpActions;
        private Button btnCreate;
        private Button btnUpdate;
        private Button btnDelete;
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
            this.btnDelete = new Button();
            this.btnUpdate = new Button();
            this.btnCreate = new Button();
            this.grpActions = new GroupBox();
            this.grpDetails = new GroupBox();
            this.txtDetails = new TextBox();
            this.lstFlightPassengers = new ListView();
            this.colDepartureDate = new ColumnHeader();
            this.colCheckedIn = new ColumnHeader();
            this.colPassport = new ColumnHeader();
            this.colPassengerName = new ColumnHeader();
            this.colFlightNumber = new ColumnHeader();
            this.colBookingReference = new ColumnHeader();
            this.btnClose = new Button();
            this.btnRefresh = new Button();
            this.lblStatus = new Label();
            this.lblWelcome = new Label();
            
            this.panelMain.SuspendLayout();
            this.grpActions.SuspendLayout();
            this.grpDetails.SuspendLayout();
            this.SuspendLayout();
            
            // panelMain
            this.panelMain.Controls.Add(this.grpActions);
            this.panelMain.Controls.Add(this.grpDetails);
            this.panelMain.Controls.Add(this.lstFlightPassengers);
            this.panelMain.Controls.Add(this.btnClose);
            this.panelMain.Controls.Add(this.btnRefresh);
            this.panelMain.Controls.Add(this.lblStatus);
            this.panelMain.Controls.Add(this.lblWelcome);
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Location = new Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new Size(1200, 800);
            this.panelMain.TabIndex = 0;
            
            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.Blue;
            this.lblWelcome.Location = new Point(20, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new Size(300, 24);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "FlightPassenger Management";
            
            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new Font("Microsoft Sans Serif", 10F);
            this.lblStatus.Location = new Point(20, 60);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(100, 17);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Loading...";
            
            // btnRefresh
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(25, 118, 210);
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new Point(1000, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(80, 30);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);
            
            // btnClose
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new Point(1090, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(80, 30);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            
            // lstFlightPassengers
            this.lstFlightPassengers.Columns.AddRange(new ColumnHeader[] {
                this.colBookingReference,
                this.colFlightNumber,
                this.colPassengerName,
                this.colPassport,
                this.colCheckedIn,
                this.colDepartureDate
            });
            this.lstFlightPassengers.FullRowSelect = true;
            this.lstFlightPassengers.GridLines = true;
            this.lstFlightPassengers.HideSelection = false;
            this.lstFlightPassengers.Location = new Point(20, 100);
            this.lstFlightPassengers.MultiSelect = false;
            this.lstFlightPassengers.Name = "lstFlightPassengers";
            this.lstFlightPassengers.Size = new Size(1150, 300);
            this.lstFlightPassengers.TabIndex = 4;
            this.lstFlightPassengers.UseCompatibleStateImageBehavior = false;
            this.lstFlightPassengers.View = View.Details;
            this.lstFlightPassengers.SelectedIndexChanged += new EventHandler(this.lstFlightPassengers_SelectedIndexChanged);
            
            // colBookingReference
            this.colBookingReference.Text = "Booking Reference";
            this.colBookingReference.Width = 150;
            
            // colFlightNumber
            this.colFlightNumber.Text = "Flight Number";
            this.colFlightNumber.Width = 120;
            
            // colPassengerName
            this.colPassengerName.Text = "Passenger Name";
            this.colPassengerName.Width = 200;
            
            // colPassport
            this.colPassport.Text = "Passport";
            this.colPassport.Width = 120;
            
            // colCheckedIn
            this.colCheckedIn.Text = "Checked In";
            this.colCheckedIn.Width = 100;
            
            // colDepartureDate
            this.colDepartureDate.Text = "Departure Date";
            this.colDepartureDate.Width = 120;
            
            // grpDetails
            this.grpDetails.Controls.Add(this.txtDetails);
            this.grpDetails.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.grpDetails.Location = new Point(20, 420);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new Size(600, 300);
            this.grpDetails.TabIndex = 5;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Flight Passenger Details";
            
            // txtDetails
            this.txtDetails.Font = new Font("Microsoft Sans Serif", 9F);
            this.txtDetails.Location = new Point(15, 25);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.ReadOnly = true;
            this.txtDetails.ScrollBars = ScrollBars.Vertical;
            this.txtDetails.Size = new Size(570, 260);
            this.txtDetails.TabIndex = 0;
            
            // grpActions
            this.grpActions.Controls.Add(this.btnCancel);
            this.grpActions.Controls.Add(this.btnDelete);
            this.grpActions.Controls.Add(this.btnUpdate);
            this.grpActions.Controls.Add(this.btnCreate);
            this.grpActions.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.grpActions.Location = new Point(650, 420);
            this.grpActions.Name = "grpActions";
            this.grpActions.Size = new Size(520, 300);
            this.grpActions.TabIndex = 6;
            this.grpActions.TabStop = false;
            this.grpActions.Text = "Actions";
            
            // btnCreate
            this.btnCreate.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnCreate.FlatAppearance.BorderSize = 0;
            this.btnCreate.FlatStyle = FlatStyle.Flat;
            this.btnCreate.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Location = new Point(50, 50);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new Size(200, 50);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Create New Booking";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new EventHandler(this.btnCreate_Click);
            
            // btnUpdate
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(255, 152, 0);
            this.btnUpdate.Enabled = false;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = FlatStyle.Flat;
            this.btnUpdate.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new Point(270, 50);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new Size(200, 50);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update Booking";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
            
            // btnDelete
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(244, 67, 54);
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new Point(50, 130);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(200, 50);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete Booking";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            
            // btnCancel
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(156, 39, 176);
            this.btnCancel.Enabled = false;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new Point(270, 130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(200, 50);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel Check-in";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            
            // FlightPassengerManagementForm
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 800);
            this.Controls.Add(this.panelMain);
            this.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Name = "FlightPassengerManagementForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "FlightPassenger Management";
            this.WindowState = FormWindowState.Maximized;
            
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.grpActions.ResumeLayout(false);
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
