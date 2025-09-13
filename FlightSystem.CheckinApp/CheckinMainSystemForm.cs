using System;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.DTOs.Common;
using Microsoft.AspNetCore.SignalR.Client;

namespace FlightSystem.CheckinApp
{
    public partial class CheckinMainSystemForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _serverUrl = "https://localhost:7261";
        private readonly EmployeeDto _currentEmployee;
        private HubConnection? _hubConnection;

        // Current data
        private FlightInfoDto? _selectedFlight;
        private PassengerDto? _selectedPassenger;
        private FlightPassengerDto? _selectedFlightPassenger;
        private List<FlightInfoDto> _flights = new();
        private List<PassengerDto> _passengers = new();
        private List<FlightPassengerDto> _flightPassengers = new();

        public CheckinMainSystemForm(EmployeeDto employee)
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri(_serverUrl) };
            _currentEmployee = employee;
            
            InitializeForm();
        }

        private async void InitializeForm()
        {
            // Setup UI components that don't require Designer controls
            SetupMenuStrip();
            SetupStatusBar();
            
            // Load initial data
            await LoadInitialData();
            
            // Setup SignalR
            await SetupSignalR();
            
            // Start timer for time display
            timerTimeUpdate.Start();
            
            // Setup profile section after controls are initialized
            SetupProfileSection();
            
            // Update UI
            UpdateUI();
        }

        private void SetupProfileSection()
        {
            if (lblEmployeeName != null)
                lblEmployeeName.Text = _currentEmployee.FullName;
            
            if (lblEmployeeRole != null)
                lblEmployeeRole.Text = _currentEmployee.Role;
            
            if (lblEmployeeId != null)
                lblEmployeeId.Text = $"ID: {_currentEmployee.EmployeeCode}";
            
            // Set profile picture (you can add actual image)
            if (picProfile != null && !string.IsNullOrEmpty(_currentEmployee.FirstName))
                picProfile.BackgroundImage = CreateProfileImage(_currentEmployee.FirstName[0]);
        }

        private void SetupMenuStrip()
        {
            // File Menu
            var fileMenu = new ToolStripMenuItem("&File");
            fileMenu.DropDownItems.Add("&New Flight", null, async (s, e) => await ShowAddFlightDialog());
            fileMenu.DropDownItems.Add("&New Passenger", null, async (s, e) => await ShowAddPassengerDialog());
            fileMenu.DropDownItems.Add("&New Booking", null, async (s, e) => await ShowAddBookingDialog());
            fileMenu.DropDownItems.Add("-");
            fileMenu.DropDownItems.Add("&Exit", null, (s, e) => Close());
            menuStrip.Items.Add(fileMenu);

            // Flight Menu
            var flightMenu = new ToolStripMenuItem("&Flight");
            flightMenu.DropDownItems.Add("&Refresh Flights", null, async (s, e) => await LoadFlights());
            flightMenu.DropDownItems.Add("&Manage Flights", null, (s, e) => NavigateToTab(tabFlights));
            flightMenu.DropDownItems.Add("&Add Flight", null, async (s, e) => await ShowAddFlightDialog());
            menuStrip.Items.Add(flightMenu);

            // Passenger Menu
            var passengerMenu = new ToolStripMenuItem("&Passenger");
            passengerMenu.DropDownItems.Add("&Refresh Passengers", null, async (s, e) => await LoadPassengers());
            passengerMenu.DropDownItems.Add("&Manage Passengers", null, (s, e) => NavigateToTab(tabPassengers));
            passengerMenu.DropDownItems.Add("&Add Passenger", null, async (s, e) => await ShowAddPassengerDialog());
            menuStrip.Items.Add(passengerMenu);

            // Check-in Menu
            var checkinMenu = new ToolStripMenuItem("&Check-in");
            checkinMenu.DropDownItems.Add("&New Check-in", null, (s, e) => NavigateToTab(tabCheckin));
            checkinMenu.DropDownItems.Add("&Manage Bookings", null, (s, e) => NavigateToTab(tabBookings));
            checkinMenu.DropDownItems.Add("&Add Booking", null, async (s, e) => await ShowAddBookingDialog());
            menuStrip.Items.Add(checkinMenu);

            // View Menu
            var viewMenu = new ToolStripMenuItem("&View");
            viewMenu.DropDownItems.Add("&Dashboard", null, (s, e) => NavigateToTab(tabDashboard));
            viewMenu.DropDownItems.Add("&Refresh All", null, async (s, e) => await LoadInitialData());
            viewMenu.DropDownItems.Add("-");
            viewMenu.DropDownItems.Add("&Status Bar", null, (s, e) => statusStrip.Visible = !statusStrip.Visible);
            menuStrip.Items.Add(viewMenu);

            // Help Menu
            var helpMenu = new ToolStripMenuItem("&Help");
            helpMenu.DropDownItems.Add("&About", null, (s, e) => ShowAboutDialog());
            menuStrip.Items.Add(helpMenu);
        }

        private void SetupStatusBar()
        {
            statusStrip.Items.Clear();
            
            var lblStatus = new ToolStripStatusLabel("Ready");
            var lblTime = new ToolStripStatusLabel();
            var lblUser = new ToolStripStatusLabel($"User: {_currentEmployee.FullName}");
            var lblConnection = new ToolStripStatusLabel("Connecting...");
            
            lblTime.Name = "lblTime";
            lblConnection.Name = "lblConnection";
            
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus, new ToolStripSeparator(), lblTime, new ToolStripSeparator(), lblUser, new ToolStripSeparator(), lblConnection });
        }

        private async Task LoadInitialData()
        {
            UpdateStatus("Loading data...");
            
            try
            {
                await Task.WhenAll(
                    LoadFlights(),
                    LoadPassengers(),
                    LoadFlightPassengers(),
                    LoadCheckinFlights()
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading initial data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateStatus("Ready");
            }
        }

        private async Task LoadFlights()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/flight/active");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightInfoDto[]>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true && result.Data != null)
                {
                    _flights = result.Data.ToList();
                    UpdateFlightsList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading flights: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadPassengers()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/passenger");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<PassengerDto[]>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true && result.Data != null)
                {
                    _passengers = result.Data.ToList();
                    UpdatePassengersList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading passengers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadFlightPassengers()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/flightpassenger");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightPassengerDto[]>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true && result.Data != null)
                {
                    _flightPassengers = result.Data.ToList();
                    UpdateFlightPassengersList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading flight passengers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateFlightsList()
        {
            if (lstFlights == null) return;
            
            lstFlights.Items.Clear();
            foreach (var flight in _flights.OrderBy(f => f.ScheduledDeparture))
            {
                var item = new ListViewItem(flight.FlightNumber);
                item.SubItems.Add(flight.DepartureAirport);
                item.SubItems.Add(flight.ArrivalAirport);
                item.SubItems.Add(flight.ScheduledDeparture.ToString("yyyy-MM-dd HH:mm"));
                item.SubItems.Add(flight.Status);
                item.Tag = flight;
                lstFlights.Items.Add(item);
            }
        }

        private void UpdatePassengersList()
        {
            if (lstPassengers == null) return;
            
            lstPassengers.Items.Clear();
            foreach (var passenger in _passengers.OrderBy(p => p.LastName))
            {
                var item = new ListViewItem(passenger.FirstName);
                item.SubItems.Add(passenger.LastName);
                item.SubItems.Add(passenger.PassportNumber);
                item.SubItems.Add(passenger.Nationality);
                item.Tag = passenger;
                lstPassengers.Items.Add(item);
            }
        }

        private void UpdateFlightPassengersList()
        {
            if (lstFlightPassengers == null) return;
            
            lstFlightPassengers.Items.Clear();
            foreach (var fp in _flightPassengers.OrderBy(f => f.Flight.FlightNumber))
            {
                var item = new ListViewItem(fp.BookingReference);
                item.SubItems.Add(fp.Flight.FlightNumber);
                item.SubItems.Add($"{fp.Passenger.FirstName} {fp.Passenger.LastName}");
                item.SubItems.Add(fp.Passenger.PassportNumber);
                item.SubItems.Add(fp.IsCheckedIn ? "Yes" : "No");
                item.Tag = fp;
                lstFlightPassengers.Items.Add(item);
            }
        }

        private async Task SetupSignalR()
        {
            try
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl($"{_serverUrl}/flightHub")
                    .Build();

                _hubConnection.On<FlightInfoDto>("FlightUpdated", OnFlightUpdated);
                _hubConnection.On<string>("SystemMessage", OnSystemMessage);

                await _hubConnection.StartAsync();
                
                var lblConnection = statusStrip.Items["lblConnection"] as ToolStripStatusLabel;
                if (lblConnection != null)
                    lblConnection.Text = "Connected";
            }
            catch (Exception ex)
            {
                var lblConnection = statusStrip.Items["lblConnection"] as ToolStripStatusLabel;
                if (lblConnection != null)
                    lblConnection.Text = "Disconnected";
                
                MessageBox.Show($"SignalR connection failed: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void OnFlightUpdated(FlightInfoDto flight)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<FlightInfoDto>(OnFlightUpdated), flight);
                return;
            }

            var existingFlight = _flights.FirstOrDefault(f => f.Id == flight.Id);
            if (existingFlight != null)
            {
                var index = _flights.IndexOf(existingFlight);
                _flights[index] = flight;
                UpdateFlightsList();
            }

            if (_selectedFlight?.Id == flight.Id)
            {
                _selectedFlight = flight;
                UpdateFlightDetails();
            }
        }

        private void OnSystemMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(OnSystemMessage), message);
                return;
            }

            UpdateStatus($"System: {message}");
        }

        private void UpdateUI()
        {
            // Update time display
            var lblTime = statusStrip.Items["lblTime"] as ToolStripStatusLabel;
            if (lblTime != null)
                lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Update dashboard with statistics
            UpdateDashboard();
        }

        private void UpdateDashboard()
        {
            if (lblTotalFlights != null)
                lblTotalFlights.Text = _flights.Count.ToString();
            
            if (lblTotalPassengers != null)
                lblTotalPassengers.Text = _passengers.Count.ToString();
            
            if (lblTotalBookings != null)
                lblTotalBookings.Text = _flightPassengers.Count.ToString();
            
            if (lblCheckedIn != null)
                lblCheckedIn.Text = _flightPassengers.Count(fp => fp.IsCheckedIn).ToString();
            
            // Recent flights
            if (lstRecentFlights != null)
            {
                var recentFlights = _flights.OrderByDescending(f => f.ScheduledDeparture).Take(5);
                lstRecentFlights.Items.Clear();
                foreach (var flight in recentFlights)
                {
                    var item = new ListViewItem(flight.FlightNumber);
                    item.SubItems.Add(flight.DepartureAirport);
                    item.SubItems.Add(flight.ArrivalAirport);
                    item.SubItems.Add(flight.ScheduledDeparture.ToString("HH:mm"));
                    item.SubItems.Add(flight.Status);
                    lstRecentFlights.Items.Add(item);
                }
            }
        }

        private void UpdateStatus(string message)
        {
            if (statusStrip.Items.Count > 0)
            {
                var statusLabel = statusStrip.Items[0] as ToolStripStatusLabel;
                if (statusLabel != null)
                    statusLabel.Text = message;
            }
        }

        private void UpdateFlightDetails()
        {
            if (txtFlightDetails == null) return;
            
            if (_selectedFlight == null)
            {
                txtFlightDetails.Clear();
                return;
            }

            var details = $"Flight: {_selectedFlight.FlightNumber}\n" +
                         $"Route: {_selectedFlight.DepartureAirport} → {_selectedFlight.ArrivalAirport}\n" +
                         $"Departure: {_selectedFlight.ScheduledDeparture:yyyy-MM-dd HH:mm}\n" +
                         $"Arrival: {_selectedFlight.ScheduledArrival:yyyy-MM-dd HH:mm}\n" +
                         $"Status: {_selectedFlight.Status}\n" +
                         $"Gate: {_selectedFlight.GateNumber}\n" +
                         $"Aircraft: {_selectedFlight.AircraftType}";

            txtFlightDetails.Text = details;
        }

        // Event Handlers
        private void lstFlights_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFlights?.SelectedItems.Count > 0)
            {
                _selectedFlight = lstFlights.SelectedItems[0].Tag as FlightInfoDto;
                UpdateFlightDetails();
                if (btnManageFlight != null) btnManageFlight.Enabled = true;
                if (btnSelectFlight != null) btnSelectFlight.Enabled = true;
            }
            else
            {
                _selectedFlight = null;
                if (btnManageFlight != null) btnManageFlight.Enabled = false;
                if (btnSelectFlight != null) btnSelectFlight.Enabled = false;
            }
        }

        private void lstPassengers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPassengers?.SelectedItems.Count > 0)
            {
                _selectedPassenger = lstPassengers.SelectedItems[0].Tag as PassengerDto;
                if (btnManagePassenger != null) btnManagePassenger.Enabled = true;
            }
            else
            {
                _selectedPassenger = null;
                if (btnManagePassenger != null) btnManagePassenger.Enabled = false;
            }
        }

        private void lstFlightPassengers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFlightPassengers?.SelectedItems.Count > 0)
            {
                _selectedFlightPassenger = lstFlightPassengers.SelectedItems[0].Tag as FlightPassengerDto;
                if (btnManageBooking != null) btnManageBooking.Enabled = true;
                if (btnCancelBooking != null) btnCancelBooking.Enabled = true;
            }
            else
            {
                _selectedFlightPassenger = null;
                if (btnManageBooking != null) btnManageBooking.Enabled = false;
                if (btnCancelBooking != null) btnCancelBooking.Enabled = false;
            }
        }

        private void timerTimeUpdate_Tick(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadInitialData();
        }

        private async void btnAddFlight_Click(object sender, EventArgs e)
        {
            await ShowAddFlightDialog();
        }

        private async void btnAddPassenger_Click(object sender, EventArgs e)
        {
            await ShowAddPassengerDialog();
        }

        private async void btnAddBooking_Click(object sender, EventArgs e)
        {
            await ShowAddBookingDialog();
        }

        private void btnManageFlight_Click(object sender, EventArgs e)
        {
            ManageFlight();
        }

        private void btnSelectFlight_Click(object sender, EventArgs e)
        {
            if (_selectedFlight != null)
            {
                NavigateToTab(tabCheckin);
                // You can also set the selected flight in the check-in combo box here
            }
        }

        private void btnManagePassenger_Click(object sender, EventArgs e)
        {
            ManagePassenger();
        }

        private void btnManageBooking_Click(object sender, EventArgs e)
        {
            ManageBooking();
        }

        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            CancelBooking();
        }

        private async Task ShowAddFlightDialog()
        {
            var addFlightForm = new AddFlightForm(_currentEmployee);
            if (addFlightForm.ShowDialog() == DialogResult.OK)
            {
                await LoadFlights();
            }
        }

        private async Task ShowAddPassengerDialog()
        {
            var addPassengerForm = new AddPassengerForm(_currentEmployee);
            if (addPassengerForm.ShowDialog() == DialogResult.OK)
            {
                await LoadPassengers();
            }
        }

        private async Task ShowAddBookingDialog()
        {
            var addBookingForm = new AddBookingForm(_currentEmployee);
            if (addBookingForm.ShowDialog() == DialogResult.OK)
            {
                await LoadFlightPassengers();
            }
        }

        private void ShowAboutDialog()
        {
            MessageBox.Show("Flight System Check-in Application\nVersion 1.0\n\nProfessional Check-in Management System", 
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void NavigateToTab(TabPage tab)
        {
            if (tab != null && tabControl != null)
            {
                tabControl.SelectedTab = tab;
            }
        }

        private void ManageFlight()
        {
            if (_selectedFlight == null)
            {
                MessageBox.Show("Please select a flight to manage.", "No Flight Selected", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // For now, show flight details. You can create a dedicated ManageFlightForm later
            MessageBox.Show($"Managing Flight: {_selectedFlight.FlightNumber}\n" +
                          $"Route: {_selectedFlight.DepartureAirport} → {_selectedFlight.ArrivalAirport}\n" +
                          $"Status: {_selectedFlight.Status}\n" +
                          $"Departure: {_selectedFlight.ScheduledDeparture:yyyy-MM-dd HH:mm}", 
                "Manage Flight", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ManagePassenger()
        {
            if (_selectedPassenger == null)
            {
                MessageBox.Show("Please select a passenger to manage.", "No Passenger Selected", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // For now, show passenger details. You can create a dedicated ManagePassengerForm later
            MessageBox.Show($"Managing Passenger: {_selectedPassenger.FirstName} {_selectedPassenger.LastName}\n" +
                          $"Passport: {_selectedPassenger.PassportNumber}\n" +
                          $"Nationality: {_selectedPassenger.Nationality}", 
                "Manage Passenger", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ManageBooking()
        {
            if (_selectedFlightPassenger == null)
            {
                MessageBox.Show("Please select a booking to manage.", "No Booking Selected", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // For now, show booking details. You can create a dedicated ManageBookingForm later
            MessageBox.Show($"Managing Booking: {_selectedFlightPassenger.BookingReference}\n" +
                          $"Flight: {_selectedFlightPassenger.Flight.FlightNumber}\n" +
                          $"Passenger: {_selectedFlightPassenger.Passenger.FirstName} {_selectedFlightPassenger.Passenger.LastName}\n" +
                          $"Checked In: {( _selectedFlightPassenger.IsCheckedIn ? "Yes" : "No")}", 
                "Manage Booking", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CancelBooking()
        {
            if (_selectedFlightPassenger == null)
            {
                MessageBox.Show("Please select a booking to cancel.", "No Booking Selected", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to cancel booking {_selectedFlightPassenger.BookingReference}?\n" +
                                       $"Flight: {_selectedFlightPassenger.Flight.FlightNumber}\n" +
                                       $"Passenger: {_selectedFlightPassenger.Passenger.FirstName} {_selectedFlightPassenger.Passenger.LastName}", 
                "Cancel Booking", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // For now, just show a message. You can implement actual cancellation later
                MessageBox.Show("Booking cancellation would be implemented here.", "Booking Cancelled", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task LoadCheckinFlights()
        {
            if (cmbCheckinFlight == null) return;

            try
            {
                var response = await _httpClient.GetAsync("/api/flight/active");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightInfoDto[]>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true && result.Data != null)
                {
                    cmbCheckinFlight.Items.Clear();
                    foreach (var flight in result.Data.OrderBy(f => f.ScheduledDeparture))
                    {
                        cmbCheckinFlight.Items.Add(new FlightComboBoxItem
                        {
                            Flight = flight,
                            DisplayText = $"{flight.FlightNumber} - {flight.DepartureAirport} → {flight.ArrivalAirport} ({flight.ScheduledDeparture:yyyy-MM-dd HH:mm})"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading flights for check-in: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class FlightComboBoxItem
        {
            public FlightInfoDto Flight { get; set; } = new();
            public string DisplayText { get; set; } = string.Empty;
            public override string ToString() => DisplayText;
        }

        private Image CreateProfileImage(char initial)
        {
            var bitmap = new Bitmap(40, 40);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                
                // Background circle
                using (var brush = new SolidBrush(Color.FromArgb(25, 118, 210)))
                {
                    g.FillEllipse(brush, 0, 0, 40, 40);
                }
                
                // Initial text
                using (var brush = new SolidBrush(Color.White))
                {
                    var font = new Font("Arial", 16, FontStyle.Bold);
                    var textSize = g.MeasureString(initial.ToString(), font);
                    var x = (40 - textSize.Width) / 2;
                    var y = (40 - textSize.Height) / 2;
                    g.DrawString(initial.ToString(), font, brush, x, y);
                }
            }
            return bitmap;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timerTimeUpdate?.Stop();
            _hubConnection?.StopAsync();
            _httpClient?.Dispose();
            base.OnFormClosed(e);
        }
    }
}