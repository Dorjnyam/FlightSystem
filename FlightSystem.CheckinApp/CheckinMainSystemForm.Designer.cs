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
        private Button btnRefresh;
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
            this.components = new System.ComponentModel.Container();
            this.timerTimeUpdate = new System.Windows.Forms.Timer(this.components);
            this.menuStrip = new MenuStrip();
            this.statusStrip = new StatusStrip();
            this.tabControl = new TabControl();
            this.tabDashboard = new TabPage();
            this.tabFlights = new TabPage();
            this.tabPassengers = new TabPage();
            this.tabCheckin = new TabPage();
            this.tabBookings = new TabPage();
            
            this.SuspendLayout();
            
            // CheckinMainSystemForm
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1400, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Flight System - Check-in Management";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(240, 248, 255);
            
            // MenuStrip
            this.menuStrip.BackColor = Color.FromArgb(25, 118, 210);
            this.menuStrip.ForeColor = Color.White;
            this.menuStrip.Font = new Font("Segoe UI", 10F);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            
            // StatusStrip
            this.statusStrip.BackColor = Color.FromArgb(25, 118, 210);
            this.statusStrip.ForeColor = Color.White;
            this.Controls.Add(this.statusStrip);
            
            // Profile Panel
            this.pnlProfile = new Panel();
            this.pnlProfile.BackColor = Color.White;
            this.pnlProfile.BorderStyle = BorderStyle.FixedSingle;
            this.pnlProfile.Location = new Point(10, 30);
            this.pnlProfile.Size = new Size(300, 80);
            
            this.picProfile = new PictureBox();
            this.picProfile.Location = new Point(10, 10);
            this.picProfile.Size = new Size(60, 60);
            this.picProfile.SizeMode = PictureBoxSizeMode.StretchImage;
            this.picProfile.BackgroundImageLayout = ImageLayout.Stretch;
            
            this.lblEmployeeName = new Label();
            this.lblEmployeeName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblEmployeeName.ForeColor = Color.FromArgb(25, 118, 210);
            this.lblEmployeeName.Location = new Point(80, 15);
            this.lblEmployeeName.Size = new Size(200, 20);
            this.lblEmployeeName.Text = "Employee Name";
            
            this.lblEmployeeRole = new Label();
            this.lblEmployeeRole.Font = new Font("Segoe UI", 10F);
            this.lblEmployeeRole.ForeColor = Color.Gray;
            this.lblEmployeeRole.Location = new Point(80, 35);
            this.lblEmployeeRole.Size = new Size(200, 20);
            this.lblEmployeeRole.Text = "Check-in Agent";
            
            this.lblEmployeeId = new Label();
            this.lblEmployeeId.Font = new Font("Segoe UI", 9F);
            this.lblEmployeeId.ForeColor = Color.Gray;
            this.lblEmployeeId.Location = new Point(80, 50);
            this.lblEmployeeId.Size = new Size(200, 20);
            this.lblEmployeeId.Text = "ID: EMP001";
            
            this.pnlProfile.Controls.AddRange(new Control[] { 
                this.picProfile, 
                this.lblEmployeeName, 
                this.lblEmployeeRole, 
                this.lblEmployeeId 
            });
            
            this.Controls.Add(this.pnlProfile);
            
            // TabControl
            this.tabControl.Location = new Point(10, 120);
            this.tabControl.Size = new Size(1380, 750);
            this.tabControl.Font = new Font("Segoe UI", 10F);
            this.tabControl.Padding = new Point(15, 8);
            this.Controls.Add(this.tabControl);
            
            // Setup Tab Pages
            this.SetupDashboardTab();
            this.SetupFlightsTab();
            this.SetupPassengersTab();
            this.SetupCheckinTab();
            this.SetupBookingsTab();
            
            // Timer
            this.timerTimeUpdate.Interval = 1000;
            this.timerTimeUpdate.Tick += new EventHandler(this.timerTimeUpdate_Tick);
            
            this.ResumeLayout(false);
            this.PerformLayout();
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
            
            this.grpFlightActions.Controls.AddRange(new Control[] {
                this.btnAddFlight, this.btnManageFlight, this.btnSelectFlight
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
            
            this.pnlFlights.Controls.AddRange(new Control[] {
                this.lstFlights, this.grpFlightActions, this.grpFlightDetails
            });
            
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
            
            this.btnPrintBoardingPass = new Button();
            this.btnPrintBoardingPass.Text = "Print Boarding Pass";
            this.btnPrintBoardingPass.BackColor = Color.FromArgb(255, 152, 0);
            this.btnPrintBoardingPass.FlatAppearance.BorderSize = 0;
            this.btnPrintBoardingPass.FlatStyle = FlatStyle.Flat;
            this.btnPrintBoardingPass.ForeColor = Color.White;
            this.btnPrintBoardingPass.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnPrintBoardingPass.Location = new Point(180, 30);
            this.btnPrintBoardingPass.Size = new Size(150, 35);
            
            this.grpCheckinActions.Controls.AddRange(new Control[] {
                this.btnCheckin, this.btnPrintBoardingPass
            });
            
            this.pnlCheckin.Controls.AddRange(new Control[] {
                this.grpFlightSelection, this.grpPassengerSearch, this.grpPassengerInfo,
                this.grpSeatMap, this.grpCheckinActions
            });
            
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
            
            this.tabControl.TabPages.Add(this.tabBookings);
        }
    }
}