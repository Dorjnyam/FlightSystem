namespace FlightSystem.CheckinApp
{
    partial class CheckinMainSystemForm
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStrip;
        private StatusStrip statusStrip;
        private TabControl tabControl;
        private TabPage tabDashboard;
        private TabPage tabFlights;
        private TabPage tabPassengers;
        private TabPage tabCheckin;
        private TabPage tabBookings;
        
        // Profile Section
        private Panel pnlProfile;
        private PictureBox picProfile;
        private Label lblEmployeeName;
        private Label lblEmployeeRole;
        private Label lblEmployeeId;
        
        // Dashboard
        private Panel pnlDashboard;
        private GroupBox grpStatistics;
        private Label lblTotalFlights;
        private Label lblTotalPassengers;
        private Label lblTotalBookings;
        private Label lblCheckedIn;
        private Label lblTotalFlightsLabel;
        private Label lblTotalPassengersLabel;
        private Label lblTotalBookingsLabel;
        private Label lblCheckedInLabel;
        private GroupBox grpRecentFlights;
        private ListView lstRecentFlights;
        private ColumnHeader colRecentFlight;
        private ColumnHeader colRecentDeparture;
        private ColumnHeader colRecentArrival;
        private ColumnHeader colRecentTime;
        private ColumnHeader colRecentStatus;
        
        // Flights Tab
        private Panel pnlFlights;
        private ListView lstFlights;
        private ColumnHeader colFlightNumber;
        private ColumnHeader colDeparture;
        private ColumnHeader colArrival;
        private ColumnHeader colDepartureTime;
        private ColumnHeader colStatus;
        private GroupBox grpFlightActions;
        private Button btnAddFlight;
        private Button btnManageFlight;
        private Button btnSelectFlight;
        private Button btnChangeFlightStatus;
        private GroupBox grpFlightDetails;
        private TextBox txtFlightDetails;
        
        // Passengers Tab
        private Panel pnlPassengers;
        private ListView lstPassengers;
        private ColumnHeader colFirstName;
        private ColumnHeader colLastName;
        private ColumnHeader colPassport;
        private ColumnHeader colNationality;
        private GroupBox grpPassengerActions;
        private Button btnAddPassenger;
        private Button btnManagePassenger;
        
        // Check-in Tab
        private Panel pnlCheckin;
        private GroupBox grpFlightSelection;
        private ComboBox cmbCheckinFlight;
        private Label lblCheckinFlight;
        private GroupBox grpPassengerSearch;
        private TextBox txtPassportSearch;
        private Label lblPassportSearch;
        private Button btnSearchPassenger;
        private GroupBox grpPassengerInfo;
        private TextBox txtPassengerInfo;
        private GroupBox grpSeatMap;
        private Panel pnlSeatMap;
        private Button btnLoadSeatMap;
        private GroupBox grpCheckinActions;
        private Button btnCheckin;
        private Button btnPrintBoardingPass;
        
        // Flight Status Change Controls
        private GroupBox grpFlightStatusChange;
        private Label lblStatusFlight;
        private ComboBox cmbStatusFlight;
        private Label lblCurrentStatus;
        private Label lblCurrentStatusValue;
        private Label lblNewStatus;
        private ComboBox cmbNewStatus;
        private Button btnChangeStatus;
        
        // Bookings Tab
        private Panel pnlBookings;
        private ListView lstFlightPassengers;
        private ColumnHeader colBookingRef;
        private ColumnHeader colBookingFlight;
        private ColumnHeader colBookingPassenger;
        private ColumnHeader colBookingPassport;
        private ColumnHeader colBookingCheckedIn;
        private GroupBox grpBookingActions;
        private Button btnAddBooking;
        private Button btnManageBooking;
        private Button btnCancelBooking;
        
        // Common Controls
        private System.Windows.Forms.Timer timerTimeUpdate;

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
            components = new System.ComponentModel.Container();
            timerTimeUpdate = new System.Windows.Forms.Timer(components);
            menuStrip = new MenuStrip();
            statusStrip = new StatusStrip();
            tabControl = new TabControl();
            tabDashboard = new TabPage();
            tabFlights = new TabPage();
            tabPassengers = new TabPage();
            tabCheckin = new TabPage();
            tabBookings = new TabPage();
            pnlProfile = new Panel();
            picProfile = new PictureBox();
            lblEmployeeName = new Label();
            lblEmployeeRole = new Label();
            lblEmployeeId = new Label();
            pnlProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picProfile).BeginInit();
            SuspendLayout();
            // 
            // timerTimeUpdate
            // 
            timerTimeUpdate.Interval = 1000;
            timerTimeUpdate.Tick += timerTimeUpdate_Tick;
            // 
            // menuStrip
            // 
            menuStrip.BackColor = Color.FromArgb(25, 118, 210);
            menuStrip.Font = new Font("Segoe UI", 10F);
            menuStrip.ForeColor = Color.White;
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(7, 2, 0, 2);
            menuStrip.Size = new Size(1633, 24);
            menuStrip.TabIndex = 0;
            // 
            // statusStrip
            // 
            statusStrip.BackColor = Color.FromArgb(25, 118, 210);
            statusStrip.ForeColor = Color.White;
            statusStrip.Location = new Point(0, 1009);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(1633, 22);
            statusStrip.TabIndex = 1;
            // 
            // tabControl
            // 
            tabControl.Font = new Font("Segoe UI", 10F);
            tabControl.Location = new Point(12, 138);
            tabControl.Margin = new Padding(4, 3, 4, 3);
            tabControl.Name = "tabControl";
            tabControl.Padding = new Point(15, 8);
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1610, 865);
            tabControl.TabIndex = 3;
            // 
            // tabDashboard
            // 
            tabDashboard.Location = new Point(0, 0);
            tabDashboard.Name = "tabDashboard";
            tabDashboard.Size = new Size(200, 100);
            tabDashboard.TabIndex = 0;
            // 
            // tabFlights
            // 
            tabFlights.Location = new Point(0, 0);
            tabFlights.Name = "tabFlights";
            tabFlights.Size = new Size(200, 100);
            tabFlights.TabIndex = 0;
            // 
            // tabPassengers
            // 
            tabPassengers.Location = new Point(0, 0);
            tabPassengers.Name = "tabPassengers";
            tabPassengers.Size = new Size(200, 100);
            tabPassengers.TabIndex = 0;
            // 
            // tabCheckin
            // 
            tabCheckin.Location = new Point(0, 0);
            tabCheckin.Name = "tabCheckin";
            tabCheckin.Size = new Size(200, 100);
            tabCheckin.TabIndex = 0;
            // 
            // tabBookings
            // 
            tabBookings.Location = new Point(0, 0);
            tabBookings.Name = "tabBookings";
            tabBookings.Size = new Size(200, 100);
            tabBookings.TabIndex = 0;
            // 
            // pnlProfile
            // 
            pnlProfile.BackColor = Color.White;
            pnlProfile.BorderStyle = BorderStyle.FixedSingle;
            pnlProfile.Controls.Add(picProfile);
            pnlProfile.Controls.Add(lblEmployeeName);
            pnlProfile.Controls.Add(lblEmployeeRole);
            pnlProfile.Controls.Add(lblEmployeeId);
            pnlProfile.Location = new Point(12, 35);
            pnlProfile.Margin = new Padding(4, 3, 4, 3);
            pnlProfile.Name = "pnlProfile";
            pnlProfile.Size = new Size(350, 92);
            pnlProfile.TabIndex = 2;
            // 
            // picProfile
            // 
            picProfile.BackgroundImageLayout = ImageLayout.Stretch;
            picProfile.Location = new Point(12, 12);
            picProfile.Margin = new Padding(4, 3, 4, 3);
            picProfile.Name = "picProfile";
            picProfile.Size = new Size(70, 69);
            picProfile.SizeMode = PictureBoxSizeMode.StretchImage;
            picProfile.TabIndex = 0;
            picProfile.TabStop = false;
            // 
            // lblEmployeeName
            // 
            lblEmployeeName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblEmployeeName.ForeColor = Color.FromArgb(25, 118, 210);
            lblEmployeeName.Location = new Point(93, 17);
            lblEmployeeName.Margin = new Padding(4, 0, 4, 0);
            lblEmployeeName.Name = "lblEmployeeName";
            lblEmployeeName.Size = new Size(233, 23);
            lblEmployeeName.TabIndex = 1;
            lblEmployeeName.Text = "Employee Name";
            // 
            // lblEmployeeRole
            // 
            lblEmployeeRole.Font = new Font("Segoe UI", 10F);
            lblEmployeeRole.ForeColor = Color.Gray;
            lblEmployeeRole.Location = new Point(93, 40);
            lblEmployeeRole.Margin = new Padding(4, 0, 4, 0);
            lblEmployeeRole.Name = "lblEmployeeRole";
            lblEmployeeRole.Size = new Size(233, 23);
            lblEmployeeRole.TabIndex = 2;
            lblEmployeeRole.Text = "Check-in Agent";
            // 
            // lblEmployeeId
            // 
            lblEmployeeId.Font = new Font("Segoe UI", 9F);
            lblEmployeeId.ForeColor = Color.Gray;
            lblEmployeeId.Location = new Point(93, 63);
            lblEmployeeId.Margin = new Padding(4, 0, 4, 0);
            lblEmployeeId.Name = "lblEmployeeId";
            lblEmployeeId.Size = new Size(233, 23);
            lblEmployeeId.TabIndex = 3;
            lblEmployeeId.Text = "ID: EMP001";
            // 
            // CheckinMainSystemForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 248, 255);
            ClientSize = new Size(1633, 1031);
            Controls.Add(menuStrip);
            Controls.Add(statusStrip);
            Controls.Add(pnlProfile);
            Controls.Add(tabControl);
            MainMenuStrip = menuStrip;
            Margin = new Padding(4, 3, 4, 3);
            Name = "CheckinMainSystemForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Flight System - Check-in Management";
            WindowState = FormWindowState.Maximized;
            pnlProfile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picProfile).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void SetupDashboardTab()
        {
            this.tabDashboard.Text = "Dashboard";
            this.tabDashboard.BackColor = Color.FromArgb(240, 248, 255);
            
            this.pnlDashboard = new Panel();
            this.pnlDashboard.Dock = DockStyle.Fill;
            this.tabDashboard.Controls.Add(this.pnlDashboard);
            
            // Statistics Group
            this.grpStatistics = new GroupBox();
            this.grpStatistics.Text = "Statistics";
            this.grpStatistics.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpStatistics.Location = new Point(20, 20);
            this.grpStatistics.Size = new Size(600, 150);
            
            this.lblTotalFlightsLabel = new Label();
            this.lblTotalFlightsLabel.Text = "Total Flights:";
            this.lblTotalFlightsLabel.Font = new Font("Segoe UI", 10F);
            this.lblTotalFlightsLabel.Location = new Point(20, 30);
            this.lblTotalFlightsLabel.Size = new Size(100, 20);
            
            this.lblTotalFlights = new Label();
            this.lblTotalFlights.Text = "0";
            this.lblTotalFlights.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTotalFlights.ForeColor = Color.FromArgb(25, 118, 210);
            this.lblTotalFlights.Location = new Point(20, 55);
            this.lblTotalFlights.Size = new Size(100, 25);
            
            this.lblTotalPassengersLabel = new Label();
            this.lblTotalPassengersLabel.Text = "Total Passengers:";
            this.lblTotalPassengersLabel.Font = new Font("Segoe UI", 10F);
            this.lblTotalPassengersLabel.Location = new Point(150, 30);
            this.lblTotalPassengersLabel.Size = new Size(120, 20);
            
            this.lblTotalPassengers = new Label();
            this.lblTotalPassengers.Text = "0";
            this.lblTotalPassengers.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTotalPassengers.ForeColor = Color.FromArgb(25, 118, 210);
            this.lblTotalPassengers.Location = new Point(150, 55);
            this.lblTotalPassengers.Size = new Size(100, 25);
            
            this.lblTotalBookingsLabel = new Label();
            this.lblTotalBookingsLabel.Text = "Total Bookings:";
            this.lblTotalBookingsLabel.Font = new Font("Segoe UI", 10F);
            this.lblTotalBookingsLabel.Location = new Point(280, 30);
            this.lblTotalBookingsLabel.Size = new Size(120, 20);
            
            this.lblTotalBookings = new Label();
            this.lblTotalBookings.Text = "0";
            this.lblTotalBookings.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTotalBookings.ForeColor = Color.FromArgb(25, 118, 210);
            this.lblTotalBookings.Location = new Point(280, 55);
            this.lblTotalBookings.Size = new Size(100, 25);
            
            this.lblCheckedInLabel = new Label();
            this.lblCheckedInLabel.Text = "Checked In:";
            this.lblCheckedInLabel.Font = new Font("Segoe UI", 10F);
            this.lblCheckedInLabel.Location = new Point(410, 30);
            this.lblCheckedInLabel.Size = new Size(100, 20);
            
            this.lblCheckedIn = new Label();
            this.lblCheckedIn.Text = "0";
            this.lblCheckedIn.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblCheckedIn.ForeColor = Color.FromArgb(76, 175, 80);
            this.lblCheckedIn.Location = new Point(410, 55);
            this.lblCheckedIn.Size = new Size(100, 25);
            
            this.grpStatistics.Controls.AddRange(new Control[] {
                this.lblTotalFlightsLabel, this.lblTotalFlights,
                this.lblTotalPassengersLabel, this.lblTotalPassengers,
                this.lblTotalBookingsLabel, this.lblTotalBookings,
                this.lblCheckedInLabel, this.lblCheckedIn
            });
            
            // Recent Flights Group
            this.grpRecentFlights = new GroupBox();
            this.grpRecentFlights.Text = "Recent Flights";
            this.grpRecentFlights.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpRecentFlights.Location = new Point(20, 190);
            this.grpRecentFlights.Size = new Size(800, 300);
            
            this.lstRecentFlights = new ListView();
            this.lstRecentFlights.Dock = DockStyle.Fill;
            this.lstRecentFlights.FullRowSelect = true;
            this.lstRecentFlights.GridLines = true;
            this.lstRecentFlights.View = View.Details;
            this.lstRecentFlights.Font = new Font("Segoe UI", 9F);
            
            this.colRecentFlight = new ColumnHeader();
            this.colRecentFlight.Text = "Flight";
            this.colRecentFlight.Width = 100;
            
            this.colRecentDeparture = new ColumnHeader();
            this.colRecentDeparture.Text = "Departure";
            this.colRecentDeparture.Width = 100;
            
            this.colRecentArrival = new ColumnHeader();
            this.colRecentArrival.Text = "Arrival";
            this.colRecentArrival.Width = 100;
            
            this.colRecentTime = new ColumnHeader();
            this.colRecentTime.Text = "Time";
            this.colRecentTime.Width = 80;
            
            this.colRecentStatus = new ColumnHeader();
            this.colRecentStatus.Text = "Status";
            this.colRecentStatus.Width = 100;
            
            this.lstRecentFlights.Columns.AddRange(new ColumnHeader[] {
                this.colRecentFlight, this.colRecentDeparture, this.colRecentArrival,
                this.colRecentTime, this.colRecentStatus
            });
            
            this.grpRecentFlights.Controls.Add(this.lstRecentFlights);
            
            this.pnlDashboard.Controls.AddRange(new Control[] { 
                this.grpStatistics, 
                this.grpRecentFlights 
            });
            
            if (!this.tabControl.TabPages.Contains(this.tabDashboard))
                this.tabControl.TabPages.Add(this.tabDashboard);
        }

        private void SetupFlightsTab()
        {
            this.tabFlights.Text = "Flights";
            this.tabFlights.BackColor = Color.FromArgb(240, 248, 255);
            
            this.pnlFlights = new Panel();
            this.pnlFlights.Dock = DockStyle.Fill;
            this.tabFlights.Controls.Add(this.pnlFlights);
            
            // Flights List
            this.lstFlights = new ListView();
            this.lstFlights.Location = new Point(20, 20);
            this.lstFlights.Size = new Size(800, 400);
            this.lstFlights.FullRowSelect = true;
            this.lstFlights.GridLines = true;
            this.lstFlights.View = View.Details;
            this.lstFlights.SelectedIndexChanged += new EventHandler(this.lstFlights_SelectedIndexChanged);
            
            this.colFlightNumber = new ColumnHeader();
            this.colFlightNumber.Text = "Flight Number";
            this.colFlightNumber.Width = 120;
            
            this.colDeparture = new ColumnHeader();
            this.colDeparture.Text = "Departure";
            this.colDeparture.Width = 100;
            
            this.colArrival = new ColumnHeader();
            this.colArrival.Text = "Arrival";
            this.colArrival.Width = 100;
            
            this.colDepartureTime = new ColumnHeader();
            this.colDepartureTime.Text = "Departure Time";
            this.colDepartureTime.Width = 150;
            
            this.colStatus = new ColumnHeader();
            this.colStatus.Text = "Status";
            this.colStatus.Width = 100;
            
            this.lstFlights.Columns.AddRange(new ColumnHeader[] {
                this.colFlightNumber, this.colDeparture, this.colArrival,
                this.colDepartureTime, this.colStatus
            });
            
            // Flight Actions
            this.grpFlightActions = new GroupBox();
            this.grpFlightActions.Text = "Actions";
            this.grpFlightActions.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpFlightActions.Location = new Point(840, 20);
            this.grpFlightActions.Size = new Size(200, 200);
            
            this.btnAddFlight = new Button();
            this.btnAddFlight.Text = "Add Flight";
            this.btnAddFlight.BackColor = Color.FromArgb(76, 175, 80);
            this.btnAddFlight.FlatAppearance.BorderSize = 0;
            this.btnAddFlight.FlatStyle = FlatStyle.Flat;
            this.btnAddFlight.ForeColor = Color.White;
            this.btnAddFlight.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnAddFlight.Location = new Point(20, 30);
            this.btnAddFlight.Size = new Size(150, 35);
            this.btnAddFlight.Click += new EventHandler(this.btnAddFlight_Click);
            
            this.btnManageFlight = new Button();
            this.btnManageFlight.Text = "Manage Flight";
            this.btnManageFlight.BackColor = Color.FromArgb(255, 152, 0);
            this.btnManageFlight.FlatAppearance.BorderSize = 0;
            this.btnManageFlight.FlatStyle = FlatStyle.Flat;
            this.btnManageFlight.ForeColor = Color.White;
            this.btnManageFlight.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnManageFlight.Location = new Point(20, 75);
            this.btnManageFlight.Size = new Size(150, 35);
            this.btnManageFlight.Enabled = false;
            this.btnManageFlight.Click += new EventHandler(this.btnManageFlight_Click);
            
            this.btnSelectFlight = new Button();
            this.btnSelectFlight.Text = "Select for Check-in";
            this.btnSelectFlight.BackColor = Color.FromArgb(25, 118, 210);
            this.btnSelectFlight.FlatAppearance.BorderSize = 0;
            this.btnSelectFlight.FlatStyle = FlatStyle.Flat;
            this.btnSelectFlight.ForeColor = Color.White;
            this.btnSelectFlight.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSelectFlight.Location = new Point(20, 120);
            this.btnSelectFlight.Size = new Size(150, 35);
            this.btnSelectFlight.Enabled = false;
            this.btnSelectFlight.Click += new EventHandler(this.btnSelectFlight_Click);
            
            this.btnChangeFlightStatus = new Button();
            this.btnChangeFlightStatus.Text = "Change Status";
            this.btnChangeFlightStatus.BackColor = Color.FromArgb(255, 152, 0);
            this.btnChangeFlightStatus.FlatAppearance.BorderSize = 0;
            this.btnChangeFlightStatus.FlatStyle = FlatStyle.Flat;
            this.btnChangeFlightStatus.ForeColor = Color.White;
            this.btnChangeFlightStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnChangeFlightStatus.Location = new Point(180, 120);
            this.btnChangeFlightStatus.Size = new Size(150, 35);
            this.btnChangeFlightStatus.Enabled = false;
            this.btnChangeFlightStatus.Click += new EventHandler(this.btnChangeFlightStatus_Click);
            
            this.grpFlightActions.Controls.AddRange(new Control[] {
                this.btnAddFlight, this.btnManageFlight, this.btnSelectFlight, this.btnChangeFlightStatus
            });
            
            // Flight Details
            this.grpFlightDetails = new GroupBox();
            this.grpFlightDetails.Text = "Flight Details";
            this.grpFlightDetails.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpFlightDetails.Location = new Point(20, 440);
            this.grpFlightDetails.Size = new Size(800, 150);
            
            this.txtFlightDetails = new TextBox();
            this.txtFlightDetails.Multiline = true;
            this.txtFlightDetails.ReadOnly = true;
            this.txtFlightDetails.ScrollBars = ScrollBars.Vertical;
            this.txtFlightDetails.Font = new Font("Segoe UI", 9F);
            this.txtFlightDetails.Location = new Point(20, 30);
            this.txtFlightDetails.Size = new Size(750, 100);
            
            this.grpFlightDetails.Controls.Add(this.txtFlightDetails);
            
            // Flight Status Change Section
            this.grpFlightStatusChange = new GroupBox();
            this.grpFlightStatusChange.Text = "Flight Status Management";
            this.grpFlightStatusChange.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpFlightStatusChange.Location = new Point(20, 400);
            this.grpFlightStatusChange.Size = new Size(450, 120);
            
            this.lblStatusFlight = new Label();
            this.lblStatusFlight.Text = "Select Flight:";
            this.lblStatusFlight.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblStatusFlight.Location = new Point(15, 30);
            this.lblStatusFlight.Size = new Size(80, 20);
            
            this.cmbStatusFlight = new ComboBox();
            this.cmbStatusFlight.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbStatusFlight.Font = new Font("Segoe UI", 9F);
            this.cmbStatusFlight.Location = new Point(100, 28);
            this.cmbStatusFlight.Size = new Size(180, 23);
            this.cmbStatusFlight.SelectedIndexChanged += new EventHandler(this.cmbStatusFlight_SelectedIndexChanged);
            
            this.lblCurrentStatus = new Label();
            this.lblCurrentStatus.Text = "Current Status:";
            this.lblCurrentStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCurrentStatus.Location = new Point(15, 60);
            this.lblCurrentStatus.Size = new Size(80, 20);
            
            this.lblCurrentStatusValue = new Label();
            this.lblCurrentStatusValue.Text = "Not Selected";
            this.lblCurrentStatusValue.Font = new Font("Segoe UI", 9F);
            this.lblCurrentStatusValue.Location = new Point(100, 60);
            this.lblCurrentStatusValue.Size = new Size(100, 20);
            this.lblCurrentStatusValue.ForeColor = Color.FromArgb(244, 67, 54);
            
            this.lblNewStatus = new Label();
            this.lblNewStatus.Text = "New Status:";
            this.lblNewStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblNewStatus.Location = new Point(15, 85);
            this.lblNewStatus.Size = new Size(70, 20);
            
            this.cmbNewStatus = new ComboBox();
            this.cmbNewStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbNewStatus.Font = new Font("Segoe UI", 9F);
            this.cmbNewStatus.Location = new Point(90, 83);
            this.cmbNewStatus.Size = new Size(120, 23);
            this.cmbNewStatus.Items.AddRange(new object[] { "Бүртгэж байна", "Онгоцонд сууж байна", "Ниссэн", "Хойшилсон", "Цуцалсан" });
            
            this.btnChangeStatus = new Button();
            this.btnChangeStatus.Text = "Change Status";
            this.btnChangeStatus.BackColor = Color.FromArgb(255, 152, 0);
            this.btnChangeStatus.FlatAppearance.BorderSize = 0;
            this.btnChangeStatus.FlatStyle = FlatStyle.Flat;
            this.btnChangeStatus.ForeColor = Color.White;
            this.btnChangeStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnChangeStatus.Location = new Point(220, 82);
            this.btnChangeStatus.Size = new Size(120, 25);
            this.btnChangeStatus.Click += new EventHandler(this.btnChangeStatus_Click);
            
            this.grpFlightStatusChange.Controls.AddRange(new Control[] {
                this.lblStatusFlight, this.cmbStatusFlight, this.lblCurrentStatus,
                this.lblCurrentStatusValue, this.lblNewStatus, this.cmbNewStatus,
                this.btnChangeStatus
            });

            this.pnlFlights.Controls.AddRange(new Control[] {
                this.lstFlights, this.grpFlightActions, this.grpFlightDetails, this.grpFlightStatusChange
            });
            
            if (!this.tabControl.TabPages.Contains(this.tabFlights))
                this.tabControl.TabPages.Add(this.tabFlights);
        }

        private void SetupPassengersTab()
        {
            this.tabPassengers.Text = "Passengers";
            this.tabPassengers.BackColor = Color.FromArgb(240, 248, 255);
            
            this.pnlPassengers = new Panel();
            this.pnlPassengers.Dock = DockStyle.Fill;
            this.tabPassengers.Controls.Add(this.pnlPassengers);
            
            // Passengers List
            this.lstPassengers = new ListView();
            this.lstPassengers.Location = new Point(20, 20);
            this.lstPassengers.Size = new Size(800, 500);
            this.lstPassengers.FullRowSelect = true;
            this.lstPassengers.GridLines = true;
            this.lstPassengers.View = View.Details;
            this.lstPassengers.SelectedIndexChanged += new EventHandler(this.lstPassengers_SelectedIndexChanged);
            
            this.colFirstName = new ColumnHeader();
            this.colFirstName.Text = "First Name";
            this.colFirstName.Width = 150;
            
            this.colLastName = new ColumnHeader();
            this.colLastName.Text = "Last Name";
            this.colLastName.Width = 150;
            
            this.colPassport = new ColumnHeader();
            this.colPassport.Text = "Passport";
            this.colPassport.Width = 120;
            
            this.colNationality = new ColumnHeader();
            this.colNationality.Text = "Nationality";
            this.colNationality.Width = 100;
            
            this.lstPassengers.Columns.AddRange(new ColumnHeader[] {
                this.colFirstName, this.colLastName, this.colPassport, this.colNationality
            });
            
            // Passenger Actions
            this.grpPassengerActions = new GroupBox();
            this.grpPassengerActions.Text = "Actions";
            this.grpPassengerActions.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpPassengerActions.Location = new Point(840, 20);
            this.grpPassengerActions.Size = new Size(200, 150);
            
            this.btnAddPassenger = new Button();
            this.btnAddPassenger.Text = "Add Passenger";
            this.btnAddPassenger.BackColor = Color.FromArgb(76, 175, 80);
            this.btnAddPassenger.FlatAppearance.BorderSize = 0;
            this.btnAddPassenger.FlatStyle = FlatStyle.Flat;
            this.btnAddPassenger.ForeColor = Color.White;
            this.btnAddPassenger.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnAddPassenger.Location = new Point(20, 30);
            this.btnAddPassenger.Size = new Size(150, 35);
            this.btnAddPassenger.Click += new EventHandler(this.btnAddPassenger_Click);
            
            this.btnManagePassenger = new Button();
            this.btnManagePassenger.Text = "Manage Passenger";
            this.btnManagePassenger.BackColor = Color.FromArgb(255, 152, 0);
            this.btnManagePassenger.FlatAppearance.BorderSize = 0;
            this.btnManagePassenger.FlatStyle = FlatStyle.Flat;
            this.btnManagePassenger.ForeColor = Color.White;
            this.btnManagePassenger.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnManagePassenger.Location = new Point(20, 75);
            this.btnManagePassenger.Size = new Size(150, 35);
            this.btnManagePassenger.Enabled = false;
            this.btnManagePassenger.Click += new EventHandler(this.btnManagePassenger_Click);
            
            this.grpPassengerActions.Controls.AddRange(new Control[] {
                this.btnAddPassenger, this.btnManagePassenger
            });
            
            this.pnlPassengers.Controls.AddRange(new Control[] {
                this.lstPassengers, this.grpPassengerActions
            });
            
            if (!this.tabControl.TabPages.Contains(this.tabPassengers))
                this.tabControl.TabPages.Add(this.tabPassengers);
        }

        private void SetupCheckinTab()
        {
            this.tabCheckin.Text = "Check-in";
            this.tabCheckin.BackColor = Color.FromArgb(240, 248, 255);
            
            this.pnlCheckin = new Panel();
            this.pnlCheckin.Dock = DockStyle.Fill;
            this.tabCheckin.Controls.Add(this.pnlCheckin);
            
            // Flight Selection
            this.grpFlightSelection = new GroupBox();
            this.grpFlightSelection.Text = "Flight Selection";
            this.grpFlightSelection.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpFlightSelection.Location = new Point(20, 20);
            this.grpFlightSelection.Size = new Size(400, 80);
            
            this.lblCheckinFlight = new Label();
            this.lblCheckinFlight.Text = "Select Flight:";
            this.lblCheckinFlight.Location = new Point(20, 30);
            this.lblCheckinFlight.Size = new Size(100, 20);
            
            this.cmbCheckinFlight = new ComboBox();
            this.cmbCheckinFlight.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCheckinFlight.Location = new Point(130, 28);
            this.cmbCheckinFlight.Size = new Size(250, 25);
            
            this.grpFlightSelection.Controls.AddRange(new Control[] {
                this.lblCheckinFlight, this.cmbCheckinFlight
            });
            
            // Passenger Search
            this.grpPassengerSearch = new GroupBox();
            this.grpPassengerSearch.Text = "Passenger Search";
            this.grpPassengerSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpPassengerSearch.Location = new Point(20, 120);
            this.grpPassengerSearch.Size = new Size(400, 80);
            
            this.lblPassportSearch = new Label();
            this.lblPassportSearch.Text = "Passport Number:";
            this.lblPassportSearch.Location = new Point(20, 30);
            this.lblPassportSearch.Size = new Size(120, 20);
            
            this.txtPassportSearch = new TextBox();
            this.txtPassportSearch.Location = new Point(150, 28);
            this.txtPassportSearch.Size = new Size(150, 25);
            
            this.btnSearchPassenger = new Button();
            this.btnSearchPassenger.Text = "Search";
            this.btnSearchPassenger.BackColor = Color.FromArgb(25, 118, 210);
            this.btnSearchPassenger.FlatAppearance.BorderSize = 0;
            this.btnSearchPassenger.FlatStyle = FlatStyle.Flat;
            this.btnSearchPassenger.ForeColor = Color.White;
            this.btnSearchPassenger.Location = new Point(310, 27);
            this.btnSearchPassenger.Size = new Size(70, 27);
            this.btnSearchPassenger.Click += new EventHandler(this.btnSearchPassenger_Click);
            
            this.grpPassengerSearch.Controls.AddRange(new Control[] {
                this.lblPassportSearch, this.txtPassportSearch, this.btnSearchPassenger
            });
            
            // Passenger Info
            this.grpPassengerInfo = new GroupBox();
            this.grpPassengerInfo.Text = "Passenger Information";
            this.grpPassengerInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpPassengerInfo.Location = new Point(20, 220);
            this.grpPassengerInfo.Size = new Size(400, 150);
            
            this.txtPassengerInfo = new TextBox();
            this.txtPassengerInfo.Multiline = true;
            this.txtPassengerInfo.ReadOnly = true;
            this.txtPassengerInfo.ScrollBars = ScrollBars.Vertical;
            this.txtPassengerInfo.Location = new Point(20, 30);
            this.txtPassengerInfo.Size = new Size(350, 100);
            
            this.grpPassengerInfo.Controls.Add(this.txtPassengerInfo);
            
            // Seat Map
            this.grpSeatMap = new GroupBox();
            this.grpSeatMap.Text = "Seat Map";
            this.grpSeatMap.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpSeatMap.Location = new Point(440, 20);
            this.grpSeatMap.Size = new Size(600, 400);
            
            this.pnlSeatMap = new Panel();
            this.pnlSeatMap.Location = new Point(20, 30);
            this.pnlSeatMap.Size = new Size(560, 300);
            this.pnlSeatMap.BackColor = Color.White;
            this.pnlSeatMap.BorderStyle = BorderStyle.FixedSingle;
            
            this.btnLoadSeatMap = new Button();
            this.btnLoadSeatMap.Text = "Load Seat Map";
            this.btnLoadSeatMap.BackColor = Color.FromArgb(25, 118, 210);
            this.btnLoadSeatMap.FlatAppearance.BorderSize = 0;
            this.btnLoadSeatMap.FlatStyle = FlatStyle.Flat;
            this.btnLoadSeatMap.ForeColor = Color.White;
            this.btnLoadSeatMap.Location = new Point(20, 340);
            this.btnLoadSeatMap.Size = new Size(120, 35);
            
            this.grpSeatMap.Controls.AddRange(new Control[] {
                this.pnlSeatMap, this.btnLoadSeatMap
            });
            
            // Check-in Actions
            this.grpCheckinActions = new GroupBox();
            this.grpCheckinActions.Text = "Check-in Actions";
            this.grpCheckinActions.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpCheckinActions.Location = new Point(20, 390);
            this.grpCheckinActions.Size = new Size(400, 100);
            
            this.btnCheckin = new Button();
            this.btnCheckin.Text = "Check-in Passenger";
            this.btnCheckin.BackColor = Color.FromArgb(76, 175, 80);
            this.btnCheckin.FlatAppearance.BorderSize = 0;
            this.btnCheckin.FlatStyle = FlatStyle.Flat;
            this.btnCheckin.ForeColor = Color.White;
            this.btnCheckin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCheckin.Location = new Point(20, 30);
            this.btnCheckin.Size = new Size(150, 35);
            this.btnCheckin.Click += new EventHandler(this.btnCheckin_Click);
            
            this.btnPrintBoardingPass = new Button();
            this.btnPrintBoardingPass.Text = "Print Boarding Pass";
            this.btnPrintBoardingPass.BackColor = Color.FromArgb(255, 152, 0);
            this.btnPrintBoardingPass.FlatAppearance.BorderSize = 0;
            this.btnPrintBoardingPass.FlatStyle = FlatStyle.Flat;
            this.btnPrintBoardingPass.ForeColor = Color.White;
            this.btnPrintBoardingPass.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnPrintBoardingPass.Location = new Point(180, 30);
            this.btnPrintBoardingPass.Size = new Size(150, 35);
            this.btnPrintBoardingPass.Click += new EventHandler(this.btnPrintBoardingPass_Click);
            
            this.grpCheckinActions.Controls.AddRange(new Control[] {
                this.btnCheckin, this.btnPrintBoardingPass
            });
            
            this.pnlCheckin.Controls.AddRange(new Control[] {
                this.grpFlightSelection, this.grpPassengerSearch, this.grpPassengerInfo,
                this.grpSeatMap, this.grpCheckinActions
            });
            
            if (!this.tabControl.TabPages.Contains(this.tabCheckin))
                this.tabControl.TabPages.Add(this.tabCheckin);
        }

        private void SetupBookingsTab()
        {
            this.tabBookings.Text = "Bookings";
            this.tabBookings.BackColor = Color.FromArgb(240, 248, 255);
            
            this.pnlBookings = new Panel();
            this.pnlBookings.Dock = DockStyle.Fill;
            this.tabBookings.Controls.Add(this.pnlBookings);
            
            // Flight Passengers List
            this.lstFlightPassengers = new ListView();
            this.lstFlightPassengers.Location = new Point(20, 20);
            this.lstFlightPassengers.Size = new Size(800, 500);
            this.lstFlightPassengers.FullRowSelect = true;
            this.lstFlightPassengers.GridLines = true;
            this.lstFlightPassengers.View = View.Details;
            this.lstFlightPassengers.SelectedIndexChanged += new EventHandler(this.lstFlightPassengers_SelectedIndexChanged);
            
            this.colBookingRef = new ColumnHeader();
            this.colBookingRef.Text = "Booking Reference";
            this.colBookingRef.Width = 150;
            
            this.colBookingFlight = new ColumnHeader();
            this.colBookingFlight.Text = "Flight";
            this.colBookingFlight.Width = 100;
            
            this.colBookingPassenger = new ColumnHeader();
            this.colBookingPassenger.Text = "Passenger";
            this.colBookingPassenger.Width = 200;
            
            this.colBookingPassport = new ColumnHeader();
            this.colBookingPassport.Text = "Passport";
            this.colBookingPassport.Width = 120;
            
            this.colBookingCheckedIn = new ColumnHeader();
            this.colBookingCheckedIn.Text = "Checked In";
            this.colBookingCheckedIn.Width = 100;
            
            this.lstFlightPassengers.Columns.AddRange(new ColumnHeader[] {
                this.colBookingRef, this.colBookingFlight, this.colBookingPassenger,
                this.colBookingPassport, this.colBookingCheckedIn
            });
            
            // Booking Actions
            this.grpBookingActions = new GroupBox();
            this.grpBookingActions.Text = "Actions";
            this.grpBookingActions.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpBookingActions.Location = new Point(840, 20);
            this.grpBookingActions.Size = new Size(200, 200);
            
            this.btnAddBooking = new Button();
            this.btnAddBooking.Text = "Add Booking";
            this.btnAddBooking.BackColor = Color.FromArgb(76, 175, 80);
            this.btnAddBooking.FlatAppearance.BorderSize = 0;
            this.btnAddBooking.FlatStyle = FlatStyle.Flat;
            this.btnAddBooking.ForeColor = Color.White;
            this.btnAddBooking.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnAddBooking.Location = new Point(20, 30);
            this.btnAddBooking.Size = new Size(150, 35);
            this.btnAddBooking.Click += new EventHandler(this.btnAddBooking_Click);
            
            this.btnManageBooking = new Button();
            this.btnManageBooking.Text = "Manage Booking";
            this.btnManageBooking.BackColor = Color.FromArgb(255, 152, 0);
            this.btnManageBooking.FlatAppearance.BorderSize = 0;
            this.btnManageBooking.FlatStyle = FlatStyle.Flat;
            this.btnManageBooking.ForeColor = Color.White;
            this.btnManageBooking.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnManageBooking.Location = new Point(20, 75);
            this.btnManageBooking.Size = new Size(150, 35);
            this.btnManageBooking.Enabled = false;
            this.btnManageBooking.Click += new EventHandler(this.btnManageBooking_Click);
            
            this.btnCancelBooking = new Button();
            this.btnCancelBooking.Text = "Cancel Booking";
            this.btnCancelBooking.BackColor = Color.FromArgb(244, 67, 54);
            this.btnCancelBooking.FlatAppearance.BorderSize = 0;
            this.btnCancelBooking.FlatStyle = FlatStyle.Flat;
            this.btnCancelBooking.ForeColor = Color.White;
            this.btnCancelBooking.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancelBooking.Location = new Point(20, 120);
            this.btnCancelBooking.Size = new Size(150, 35);
            this.btnCancelBooking.Enabled = false;
            this.btnCancelBooking.Click += new EventHandler(this.btnCancelBooking_Click);
            
            this.grpBookingActions.Controls.AddRange(new Control[] {
                this.btnAddBooking, this.btnManageBooking, this.btnCancelBooking
            });
            
            this.pnlBookings.Controls.AddRange(new Control[] {
                this.lstFlightPassengers, this.grpBookingActions
            });
            
            if (!this.tabControl.TabPages.Contains(this.tabBookings))
                this.tabControl.TabPages.Add(this.tabBookings);
        }
    }
}