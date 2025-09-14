using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightSystem.Shared.DTOs.Common;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.CheckinApp.Controllers;
using FlightSystem.CheckinApp.Services;
using FlightSystem.Core.Enums;
using Microsoft.AspNetCore.SignalR.Client;

namespace FlightSystem.CheckinApp
{
    public partial class CheckinMainSystemForm : Form
    {
        private readonly EmployeeDto _currentEmployee;
        private readonly FormController _formController;
        private readonly CheckinService _checkinService;
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
            
            _currentEmployee = employee ?? throw new ArgumentNullException(nameof(employee));
            _formController = new FormController(_currentEmployee);
            _checkinService = new CheckinService();
            
            _formController.SetMainForm(this);
            
            // Subscribe to form controller events
            _formController.DataChanged += OnDataChanged;
            
            _ = InitializeFormAsync();
        }

        #region Initialization

        private async Task InitializeFormAsync()
        {
            try
            {
                // Setup UI components
            SetupMenuStrip();
            SetupStatusBar();
                SetupTabs();
                SetupProfileSection();
            
            // Load initial data
                await LoadAllDataAsync();
            
            // Setup SignalR
                await SetupSignalRAsync();
            
                // Start timer
            timerTimeUpdate.Start();
            
                UpdateStatus("System ready");
            }
            catch (Exception ex)
            {
                UpdateStatus($"Initialization error: {ex.Message}");
                MessageBox.Show($"Error initializing application: {ex.Message}", 
                    "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupTabs()
        {
            SetupDashboardTab();
            SetupFlightsTab();
            SetupPassengersTab();
            SetupCheckinTab();
            SetupBookingsTab();
            SetupConcurrentTestTab();
        }

        private void SetupProfileSection()
        {
            if (lblEmployeeName != null)
                lblEmployeeName.Text = _currentEmployee.FullName;
            
            if (lblEmployeeRole != null)
                lblEmployeeRole.Text = _currentEmployee.Role;
            
            if (lblEmployeeId != null)
                lblEmployeeId.Text = $"ID: {_currentEmployee.EmployeeCode}";
        }

        #endregion

        #region Data Loading Methods

        /// <summary>
        /// Load all data from server
        /// </summary>
        public async Task LoadAllDataAsync()
        {
            UpdateStatus("Loading data...");
            
            try
            {
                var tasks = new List<Task>
                {
                    LoadFlightsAsync(),
                    LoadPassengersAsync(),
                    LoadFlightPassengersAsync(),
                    LoadTestFlightsAsync()
                };

                await Task.WhenAll(tasks);
                
                UpdateUI();
                UpdateStatus($"Data loaded successfully - {_flights.Count} flights, {_passengers.Count} passengers, {_flightPassengers.Count} bookings");
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error loading data: {ex.Message}");
                MessageBox.Show($"Error loading data: {ex.Message}", "Data Loading Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load flights from server
        /// </summary>
        public async Task LoadFlightsAsync()
        {
            var result = await _checkinService.GetActiveFlightsAsync();
            
            if (result.IsSuccess && result.Data != null)
            {
                _flights = result.Data;
                UpdateFlightsList();
                await PopulateFlightComboBoxesAsync();
            }
            else
            {
                UpdateStatus($"Error loading flights: {result.ErrorMessage}");
            }
        }

        /// <summary>
        /// Load passengers from server
        /// </summary>
        public async Task LoadPassengersAsync()
        {
            var result = await _checkinService.GetPassengersAsync();
            
            if (result.IsSuccess && result.Data != null)
            {
                _passengers = result.Data;
                UpdatePassengersList();
            }
            else
            {
                UpdateStatus($"Error loading passengers: {result.ErrorMessage}");
            }
        }

        /// <summary>
        /// Load flight passengers from server
        /// </summary>
        public async Task LoadFlightPassengersAsync()
        {
            var result = await _checkinService.GetFlightPassengersAsync();
            
            if (result.IsSuccess && result.Data != null)
            {
                _flightPassengers = result.Data;
                UpdateFlightPassengersList();
            }
            else
            {
                UpdateStatus($"Error loading bookings: {result.ErrorMessage}");
            }
        }

        /// <summary>
        /// Populate flight combo boxes
        /// </summary>
        private async Task PopulateFlightComboBoxesAsync()
        {
            await Task.Run(() =>
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => PopulateFlightComboBoxes()));
                }
                else
                {
                    PopulateFlightComboBoxes();
                }
            });
        }

        private void PopulateFlightComboBoxes()
        {
            // Populate check-in flight combo
            if (cmbCheckinFlight != null)
            {
                cmbCheckinFlight.Items.Clear();
                foreach (var flight in _flights.OrderBy(f => f.ScheduledDeparture))
                {
                    cmbCheckinFlight.Items.Add(new FlightComboBoxItem(flight));
                }
            }

            // Populate status change flight combo
            if (cmbStatusFlight != null)
            {
                cmbStatusFlight.Items.Clear();
                foreach (var flight in _flights.OrderBy(f => f.ScheduledDeparture))
                {
                    cmbStatusFlight.Items.Add(new FlightComboBoxItem(flight));
                }
            }
        }

        #endregion

        #region UI Update Methods

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
                
                // Color code based on status
                item.BackColor = GetStatusBackColor(flight.Status);
                
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
                
                // Color code checked-in passengers
                if (fp.IsCheckedIn)
                {
                    item.BackColor = Color.LightGreen;
                }
                
                lstFlightPassengers.Items.Add(item);
            }
        }

        private void UpdateUI()
        {
            UpdateTimeDisplay();
            UpdateDashboard();
        }

        private void UpdateTimeDisplay()
        {
            var lblTime = statusStrip?.Items["lblTime"] as ToolStripStatusLabel;
            if (lblTime != null)
                lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
            
            UpdateRecentFlights();
        }

        private void UpdateRecentFlights()
        {
            if (lstRecentFlights == null) return;
            
            var recentFlights = _flights
                .OrderByDescending(f => f.ScheduledDeparture)
                .Take(5);
                
                lstRecentFlights.Items.Clear();
                foreach (var flight in recentFlights)
                {
                    var item = new ListViewItem(flight.FlightNumber);
                    item.SubItems.Add(flight.DepartureAirport);
                    item.SubItems.Add(flight.ArrivalAirport);
                    item.SubItems.Add(flight.ScheduledDeparture.ToString("HH:mm"));
                    item.SubItems.Add(flight.Status);
                item.BackColor = GetStatusBackColor(flight.Status);
                    lstRecentFlights.Items.Add(item);
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
                         $"Gate: {_selectedFlight.GateNumber ?? "TBD"}\n" +
                         $"Aircraft: {_selectedFlight.AircraftType ?? "TBD"}";

            txtFlightDetails.Text = details;
        }

        #endregion

        #region Flight Status Management (FIXED)

        /// <summary>
        /// Handle flight status change from button
        /// </summary>
        private async void btnChangeFlightStatus_Click(object sender, EventArgs e)
        {
            if (_selectedFlight == null)
            {
                MessageBox.Show("Please select a flight first.", "No Flight Selected", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await ShowFlightStatusDialog(_selectedFlight);
        }

        /// <summary>
        /// Handle status change from combo box section
        /// </summary>
        private async void btnChangeStatus_Click(object sender, EventArgs e)
        {
            var selectedFlight = cmbStatusFlight?.SelectedItem as FlightComboBoxItem;
            var newStatus = cmbNewStatus?.SelectedItem?.ToString();

            if (selectedFlight == null)
            {
                MessageBox.Show("Please select a flight first.", "Flight Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(newStatus))
            {
                MessageBox.Show("Please select a new status.", "Status Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = _formController.ShowConfirmationDialog(
                $"Are you sure you want to change flight {selectedFlight.Flight.FlightNumber} status from '{selectedFlight.Flight.Status}' to '{newStatus}'?",
                "Confirm Status Change");

            if (result == DialogResult.Yes)
            {
                await UpdateFlightStatusAsync(selectedFlight.Flight.Id, newStatus);
            }
        }

        /// <summary>
        /// Show flight status dialog
        /// </summary>
        private async Task ShowFlightStatusDialog(FlightInfoDto flight)
        {
            var (result, newStatus) = await _formController.ShowFlightStatusDialogAsync(flight);
            
            if (result == DialogResult.OK && !string.IsNullOrEmpty(newStatus))
            {
                await UpdateFlightStatusAsync(flight.Id, newStatus);
            }
        }

        /// <summary>
        /// Update flight status on server
        /// </summary>
        private async Task UpdateFlightStatusAsync(int flightId, string newStatusString)
        {
            try
            {
                UpdateStatus("Updating flight status...");

                // Convert string to enum
                var statusEnum = ConvertStringToFlightStatus(newStatusString);
                
                var result = await _checkinService.UpdateFlightStatusAsync(flightId, statusEnum, _currentEmployee.Id);
                
                if (result.IsSuccess && result.Data != null)
                {
                    _formController.ShowSuccessMessage($"Flight status changed successfully to '{newStatusString}'!");
                    
                    // Update local data
                    var existingFlight = _flights.FirstOrDefault(f => f.Id == flightId);
                    if (existingFlight != null)
                    {
                        var index = _flights.IndexOf(existingFlight);
                        _flights[index] = result.Data;
                        
                        if (_selectedFlight?.Id == flightId)
                        {
                            _selectedFlight = result.Data;
                        }
                    }
                    
                    // Refresh UI
                    UpdateFlightsList();
                    UpdateFlightDetails();
                    await PopulateFlightComboBoxesAsync();
                    
                    UpdateStatus($"Flight {result.Data.FlightNumber} status changed to {newStatusString}");
                }
                else
                {
                    MessageBox.Show($"Failed to change flight status: {result.ErrorMessage}", 
                        "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatus($"Status update failed: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing flight status: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus($"Error updating status: {ex.Message}");
            }
        }

        /// <summary>
        /// Handle status flight selection change
        /// </summary>
        private void cmbStatusFlight_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = cmbStatusFlight?.SelectedItem as FlightComboBoxItem;
            if (selectedItem != null && lblCurrentStatusValue != null)
            {
                lblCurrentStatusValue.Text = selectedItem.Flight.Status;
                lblCurrentStatusValue.ForeColor = GetStatusColor(selectedItem.Flight.Status);
            }
        }

        /// <summary>
        /// Convert status string to FlightStatus enum
        /// </summary>
        private FlightStatus ConvertStringToFlightStatus(string statusString)
        {
            return statusString switch
            {
                "Төлөвлөсөн" or "Scheduled" => FlightStatus.Scheduled,
                "Бүртгэж байна" or "CheckinOpen" => FlightStatus.CheckinOpen,
                "Бүртгэл хаагдсан" or "CheckinClosed" => FlightStatus.CheckinClosed,
                "Онгоцонд сууж байна" or "Boarding" => FlightStatus.Boarding,
                "Сүүлчийн дуудлага" or "LastCall" => FlightStatus.LastCall,
                "Хаалга хаагдсан" or "GateClosed" => FlightStatus.GateClosed,
                "Ниссэн" or "Departed" => FlightStatus.Departed,
                "Хойшилсон" or "Delayed" => FlightStatus.Delayed,
                "Цуцалсан" or "Cancelled" => FlightStatus.Cancelled,
                _ => FlightStatus.Scheduled
            };
        }

        #endregion

        #region Event Handlers

        private void lstFlights_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFlights?.SelectedItems.Count > 0)
            {
                _selectedFlight = lstFlights.SelectedItems[0].Tag as FlightInfoDto;
                UpdateFlightDetails();
                
                // Enable buttons
                if (btnManageFlight != null) btnManageFlight.Enabled = true;
                if (btnSelectFlight != null) btnSelectFlight.Enabled = true;
                if (btnChangeFlightStatus != null) btnChangeFlightStatus.Enabled = true;
            }
            else
            {
                _selectedFlight = null;
                UpdateFlightDetails();
                
                // Disable buttons
                if (btnManageFlight != null) btnManageFlight.Enabled = false;
                if (btnSelectFlight != null) btnSelectFlight.Enabled = false;
                if (btnChangeFlightStatus != null) btnChangeFlightStatus.Enabled = false;
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

        #endregion

        #region Button Event Handlers

        private async void btnAddFlight_Click(object sender, EventArgs e)
        {
            var result = await _formController.ShowAddFlightDialogAsync();
            if (result == DialogResult.OK)
            {
                UpdateStatus("Flight added successfully");
            }
        }

        private async void btnAddPassenger_Click(object sender, EventArgs e)
        {
            var result = await _formController.ShowAddPassengerDialogAsync();
            if (result == DialogResult.OK)
            {
                UpdateStatus("Passenger added successfully");
            }
        }

        private async void btnAddBooking_Click(object sender, EventArgs e)
        {
            var result = await _formController.ShowAddBookingDialogAsync();
            if (result == DialogResult.OK)
            {
                UpdateStatus("Booking added successfully");
            }
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
                LoadSelectedFlightForCheckin(_selectedFlight);
            }
        }

        private void btnManagePassenger_Click(object sender, EventArgs e)
        {
            ManagePassenger();
        }

        private async void btnManageBooking_Click(object sender, EventArgs e)
        {
            if (_selectedFlightPassenger != null)
            {
                var result = await _formController.ShowUpdateFlightPassengerDialogAsync(_selectedFlightPassenger);
                if (result == DialogResult.OK)
                {
                    UpdateStatus("Booking updated successfully");
                }
            }
            else
            {
                var result = await _formController.ShowFlightPassengerManagementDialogAsync();
                if (result == DialogResult.OK)
                {
                    UpdateStatus("FlightPassenger management completed");
                }
            }
        }

        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            CancelBooking();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadAllDataAsync();
        }

        #endregion

        #region Check-in Event Handlers

        private async void btnSearchPassenger_Click(object sender, EventArgs e)
        {
            await SearchPassenger();
        }

        private async void btnCheckin_Click(object sender, EventArgs e)
        {
            await PerformCheckin();
        }

        private async void btnPrintBoardingPass_Click(object sender, EventArgs e)
        {
            await PrintBoardingPass();
        }

        private async void btnLoadSeatMap_Click(object sender, EventArgs e)
        {
            await LoadSeatMapAsync();
        }

        private async void btnOpenCheckin_Click(object sender, EventArgs e)
        {
            await OpenCheckinForFlight();
        }

        private async Task LoadSeatMapAsync()
        {
            if (cmbCheckinFlight?.SelectedItem is not FlightComboBoxItem selectedFlight)
            {
                MessageBox.Show("Please select a flight first.", "Flight Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassportSearch?.Text))
            {
                MessageBox.Show("Please search for a passenger first.", "Passenger Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if flight status allows check-in
            var status = selectedFlight.Flight.Status;
            var isValidCheckinStatus = status == "Бүртгэж байна" || status == "CheckinOpen";
            
            if (!isValidCheckinStatus)
            {
                var result = MessageBox.Show($"Flight {selectedFlight.Flight.FlightNumber} is not in check-in status.\nCurrent status: {status}\n\nDo you want to open check-in for this flight?", 
                    "Flight Status Check", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    await UpdateFlightStatusAsync(selectedFlight.Flight.Id, "CheckinOpen");
                    // Refresh the flight data
                    await LoadFlightsAsync();
                    
                    // Update the combo box selection
                    cmbCheckinFlight.SelectedItem = cmbCheckinFlight.Items.Cast<FlightComboBoxItem>()
                        .FirstOrDefault(item => item.Flight.Id == selectedFlight.Flight.Id);
                }
                else
                {
                    return;
                }
            }

            try
            {
                btnLoadSeatMap.Enabled = false;
                btnLoadSeatMap.Text = "Loading...";

                var result = await _checkinService.GetSeatMapAsync(selectedFlight.Flight.Id);

                if (result.IsSuccess && result.Data != null)
                {
                    CreateSeatMap(result.Data);
                    MessageBox.Show($"Seat map loaded successfully!\nAvailable seats: {result.Data.AvailableSeats}/{result.Data.TotalSeats}", 
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Failed to load seat map: {result.ErrorMessage}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading seat map: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLoadSeatMap.Enabled = true;
                btnLoadSeatMap.Text = "Load Seat Map";
            }
        }

        private void CreateSeatMap(SeatMapDto seatMap)
        {
            if (pnlSeatMap == null) return;

            // Clear existing seat buttons
            pnlSeatMap.Controls.Clear();

            const int seatWidth = 40;
            const int seatHeight = 35;
            const int seatSpacing = 8;
            const int rowSpacing = 45;
            const int startX = 50;
            const int startY = 30;

            // Add header labels for seat columns
            var columnLabels = new[] { "A", "B", "C", "D", "E", "F" };
            for (int i = 0; i < columnLabels.Length; i++)
            {
                var columnLabel = new Label
                {
                    Text = columnLabels[i],
                    Location = new Point(startX + i * (seatWidth + seatSpacing) + seatWidth/2 - 5, startY - 20),
                    Size = new Size(20, 20),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.DarkBlue
                };
                pnlSeatMap.Controls.Add(columnLabel);
            }

            // Create seat buttons for each row
            int rowIndex = 0;
            foreach (var seatRow in seatMap.SeatRows)
            {
                int currentX = startX;
                
                // Add row number label
                var rowLabel = new Label
                {
                    Text = seatRow.Row,
                    Location = new Point(10, startY + rowIndex * rowSpacing + seatHeight/2 - 10),
                    Size = new Size(25, 20),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.DarkBlue
                };
                pnlSeatMap.Controls.Add(rowLabel);

                // Create seat buttons for this row
                foreach (var seat in seatRow.Seats)
                {
                    var seatButton = new Button
                    {
                        Text = seat.SeatNumber,
                        Location = new Point(currentX, startY + rowIndex * rowSpacing),
                        Size = new Size(seatWidth, seatHeight),
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        Tag = seat,
                        Enabled = seat.IsAvailable,
                        FlatStyle = FlatStyle.Flat
                    };

                    // Set seat button colors based on availability and class
                    if (!seat.IsAvailable)
                    {
                        seatButton.BackColor = Color.Red;
                        seatButton.ForeColor = Color.White;
                        seatButton.Text = "X";
                        seatButton.Enabled = false;
                        seatButton.FlatAppearance.BorderColor = Color.DarkRed;
                    }
                    else
                    {
                        // Color based on seat class
                        switch (seat.SeatClass.ToUpper())
                        {
                            case "FIRST":
                                seatButton.BackColor = Color.Gold;
                                seatButton.ForeColor = Color.Black;
                                break;
                            case "BUSINESS":
                                seatButton.BackColor = Color.Purple;
                                seatButton.ForeColor = Color.White;
                                break;
                            case "PREMIUMECONOMY":
                                seatButton.BackColor = Color.Orange;
                                seatButton.ForeColor = Color.White;
                                break;
                            default: // ECONOMY
                                seatButton.BackColor = Color.Green;
                                seatButton.ForeColor = Color.White;
                                break;
                        }
                        seatButton.Click += (s, e) => SelectSeat(seat);
                        seatButton.FlatAppearance.BorderColor = Color.DarkGreen;
                    }

                    seatButton.FlatAppearance.BorderSize = 2;
                    pnlSeatMap.Controls.Add(seatButton);
                    currentX += seatWidth + seatSpacing;
                }
                
                rowIndex++;
            }

            // Add aisle indicator
            var aisleLabel = new Label
            {
                Text = "AISLE",
                Location = new Point(startX + (seatWidth + seatSpacing) * 3 - 15, startY - 35),
                Size = new Size(60, 15),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.DarkGray,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlSeatMap.Controls.Add(aisleLabel);

            // Add seat class legend
            var legendY = startY + rowIndex * rowSpacing + 20;
            var legend = new Label
            {
                Text = "LEGEND: 🟡 First | 🟣 Business | 🟠 Premium | 🟢 Economy | 🔴 Occupied",
                Location = new Point(10, legendY),
                Size = new Size(700, 20),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };
            pnlSeatMap.Controls.Add(legend);
        }

        private void SelectSeat(SeatInfoDto seat)
        {
            // Show seat selection confirmation
            var result = MessageBox.Show($"Do you want to select seat {seat.SeatNumber}?", 
                "Seat Selection", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                // Store the selected seat
                _selectedSeat = seat;

                // Reset all seat colors first
                foreach (Control control in pnlSeatMap.Controls)
                {
                    if (control is Button button && button.Tag is SeatInfoDto seatInfo)
                    {
                        if (!seatInfo.IsAvailable)
                        {
                            button.BackColor = Color.Red;
                            button.ForeColor = Color.White;
                            button.Text = "X";
                            button.Enabled = false;
                }
                else
                {
                            // Reset to original seat class colors
                            switch (seatInfo.SeatClass.ToUpper())
                            {
                                case "FIRST":
                                    button.BackColor = Color.Gold;
                                    button.ForeColor = Color.Black;
                                    break;
                                case "BUSINESS":
                                    button.BackColor = Color.Purple;
                                    button.ForeColor = Color.White;
                                    break;
                                case "PREMIUMECONOMY":
                                    button.BackColor = Color.Orange;
                                    button.ForeColor = Color.White;
                                    break;
                                default: // ECONOMY
                                    button.BackColor = Color.Green;
                                    button.ForeColor = Color.White;
                                    break;
                            }
                            button.Text = seatInfo.SeatNumber;
                            button.Enabled = true;
                        }
                    }
                }

                // Highlight selected seat
                foreach (Control control in pnlSeatMap.Controls)
                {
                    if (control is Button button && button.Tag is SeatInfoDto seatInfo)
                    {
                        if (seatInfo.Id == seat.Id)
                        {
                            button.BackColor = Color.Blue;
                            button.ForeColor = Color.White;
                            button.Text = "✓";
                            button.Enabled = false;
                        }
                    }
                }

                // Enable check-in button
                if (btnCheckin != null)
                {
                    btnCheckin.Enabled = true;
                }

                MessageBox.Show($"Seat {seat.SeatNumber} selected successfully!\nYou can now proceed with check-in.", "Seat Selected", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task OpenCheckinForFlight()
        {
            if (cmbCheckinFlight?.SelectedItem is not FlightComboBoxItem selectedFlight)
            {
                MessageBox.Show("Please select a flight first.", "Flight Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnOpenCheckin.Enabled = false;
                btnOpenCheckin.Text = "Opening...";

                await UpdateFlightStatusAsync(selectedFlight.Flight.Id, "CheckinOpen");
                
                // Refresh the flight data
                await LoadFlightsAsync();
                
                // Update the combo box selection
                cmbCheckinFlight.SelectedItem = cmbCheckinFlight.Items.Cast<FlightComboBoxItem>()
                    .FirstOrDefault(item => item.Flight.Id == selectedFlight.Flight.Id);

                MessageBox.Show($"Check-in opened successfully for flight {selectedFlight.Flight.FlightNumber}!\n\n" +
                               $"Note: If you encounter time validation issues, it may be due to timezone differences when the flight was created.\n" +
                               $"You can proceed with check-in by clicking 'Yes' when prompted.", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening check-in: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            finally
            {
                btnOpenCheckin.Enabled = true;
                btnOpenCheckin.Text = "Open Check-in";
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Load selected flight into check-in tab
        /// </summary>
        /// <param name="flight">Selected flight</param>
        private void LoadSelectedFlightForCheckin(FlightInfoDto flight)
        {
            if (cmbCheckinFlight != null)
            {
                // Find and select the flight in the combo box
                for (int i = 0; i < cmbCheckinFlight.Items.Count; i++)
                {
                    if (cmbCheckinFlight.Items[i] is FlightComboBoxItem item && 
                        item.Flight.Id == flight.Id)
                    {
                        cmbCheckinFlight.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private async Task LoadTestFlightsAsync()
        {
            try
            {
                // Use the existing flights data that's already loaded
                cmbTestFlight.Items.Clear();
                foreach (var flight in _flights)
                {
                    // Create a display item that shows flight number and route
                    var displayItem = new FlightDisplayItem(flight);
                    cmbTestFlight.Items.Add(displayItem);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading test flights: {ex.Message}");
            }
        }

        private async void btnRefreshTestFlights_Click(object sender, EventArgs e)
        {
            try
            {
                await LoadTestFlightsAsync();
                MessageBox.Show($"Refreshed {cmbTestFlight.Items.Count} flights for testing.", "Refresh Complete", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing flights: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTestFlight_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optional: Add any logic when flight selection changes
            if (cmbTestFlight.SelectedItem is FlightDisplayItem selectedItem)
            {
                // You could auto-populate seat suggestions or other helpful info here
                System.Diagnostics.Debug.WriteLine($"Selected flight for testing: {selectedItem.Flight.FlightNumber}");
            }
        }

        private async void btnRunConcurrentTest_Click(object sender, EventArgs e)
        {
            try
            {
                // Оролтыг шалгах
                if (cmbTestFlight.SelectedItem == null)
                {
                    MessageBox.Show("Нислэг сонгоно уу.", "Шалгах алдаа",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTestSeatNumber.Text))
                {
                    MessageBox.Show("Суудлын дугаарыг оруулна уу.", "Шалгах алдаа",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTestPassenger1.Text))
                {
                    MessageBox.Show("Зорчигч 1-ийн паспортын дугаарыг оруулна уу.", "Шалгах алдаа",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTestPassenger2.Text))
                {
                    MessageBox.Show("Зорчигч 2-ийн паспортын дугаарыг оруулна уу.", "Шалгах алдаа",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Сонгосон нислэг авах
                var selectedItem = cmbTestFlight.SelectedItem as FlightDisplayItem;
                if (selectedItem == null)
                {
                    MessageBox.Show("Буруу нислэг сонгогдсон байна.", "Алдаа",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var selectedFlight = selectedItem.Flight;

                // Туршилт эхлэхэд товчийг идэвхгүй болгох
                btnRunConcurrentTest.Enabled = false;
                btnRunConcurrentTest.Text = "🔄 Туршилт ажиллаж байна...";

                // Өмнөх үр дүнг цэвэрлэх
                txtTestResults.Clear();

                // Туршилтын эхлэл бичих
                txtTestResults.AppendText($"=== ЗЭРЭГЦЭЭ СУУДАЛ ОНООХ ТУРШИЛТ ===\r\n");
                txtTestResults.AppendText($"Туршилт эхэлсэн: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}\r\n");
                txtTestResults.AppendText($"Нислэг: {selectedFlight.FlightNumber}\r\n");
                txtTestResults.AppendText($"Суудал: {txtTestSeatNumber.Text}\r\n");
                txtTestResults.AppendText($"Зорчигч 1: {txtTestPassenger1.Text}\r\n");
                txtTestResults.AppendText($"Зорчигч 2: {txtTestPassenger2.Text}\r\n");
                txtTestResults.AppendText($"\r\n");

                // Туршилтыг ажиллуулах
                var testResult = await _checkinService.TestConcurrentSeatAssignmentAsync(
                    selectedFlight.Id,
                    txtTestSeatNumber.Text,
                    txtTestPassenger1.Text,
                    txtTestPassenger2.Text
                );

                if (testResult.IsSuccess && testResult.Data != null)
                {
                    var result = testResult.Data;

                    txtTestResults.AppendText($"✅ ТУРШИЛТ АМЖИЛТТАЙ ДУУСЛАА\r\n");
                    txtTestResults.AppendText($"Туршилтын ID: {result.TestId}\r\n");
                    txtTestResults.AppendText($"Нийт хугацаа: {(result.TestEndTime - result.TestStartTime).TotalMilliseconds:F2}мс\r\n");
                    txtTestResults.AppendText($"\r\n");

                    txtTestResults.AppendText($"=== ЗОРЧИГЧ 1-ИЙН ҮР ДҮН ===\r\n");
                    txtTestResults.AppendText($"Паспорт: {result.Passenger1Result.PassportNumber}\r\n");
                    txtTestResults.AppendText($"Амжилт: {(result.Passenger1Result.Success ? "✅ ТИЙМ" : "❌ ҮГҮЙ")}\r\n");
                    txtTestResults.AppendText($"Боловсруулсан хугацаа: {result.Passenger1Result.ProcessingTimeMs}мс\r\n");
                    txtTestResults.AppendText($"Эхэлсэн цаг: {result.Passenger1Result.RequestStartTime:HH:mm:ss.fff}\r\n");
                    txtTestResults.AppendText($"Дууссан цаг: {result.Passenger1Result.RequestEndTime:HH:mm:ss.fff}\r\n");
                    if (!string.IsNullOrEmpty(result.Passenger1Result.ErrorMessage))
                    {
                        txtTestResults.AppendText($"Алдаа: {result.Passenger1Result.ErrorMessage}\r\n");
                    }
                    if (!string.IsNullOrEmpty(result.Passenger1Result.SeatAssignmentId))
                    {
                        txtTestResults.AppendText($"Суудал оноолтын ID: {result.Passenger1Result.SeatAssignmentId}\r\n");
                    }
                    txtTestResults.AppendText($"\r\n");

                    txtTestResults.AppendText($"=== ЗОРЧИГЧ 2-ИЙН ҮР ДҮН ===\r\n");
                    txtTestResults.AppendText($"Паспорт: {result.Passenger2Result.PassportNumber}\r\n");
                    txtTestResults.AppendText($"Амжилт: {(result.Passenger2Result.Success ? "✅ ТИЙМ" : "❌ ҮГҮЙ")}\r\n");
                    txtTestResults.AppendText($"Боловсруулсан хугацаа: {result.Passenger2Result.ProcessingTimeMs}мс\r\n");
                    txtTestResults.AppendText($"Эхэлсэн цаг: {result.Passenger2Result.RequestStartTime:HH:mm:ss.fff}\r\n");
                    txtTestResults.AppendText($"Дууссан цаг: {result.Passenger2Result.RequestEndTime:HH:mm:ss.fff}\r\n");
                    if (!string.IsNullOrEmpty(result.Passenger2Result.ErrorMessage))
                    {
                        txtTestResults.AppendText($"Алдаа: {result.Passenger2Result.ErrorMessage}\r\n");
                    }
                    if (!string.IsNullOrEmpty(result.Passenger2Result.SeatAssignmentId))
                    {
                        txtTestResults.AppendText($"Суудал оноолтын ID: {result.Passenger2Result.SeatAssignmentId}\r\n");
                    }
                    txtTestResults.AppendText($"\r\n");

                    txtTestResults.AppendText($"=== ДҮГНЭЛТ ===\r\n");
                    txtTestResults.AppendText($"Ялагч зорчигч: {result.WinnerPassenger}\r\n");
                    txtTestResults.AppendText($"Товч тайлбар: {result.Summary}\r\n");
                    txtTestResults.AppendText($"\r\n");

                    txtTestResults.AppendText($"Туршилт дууссан: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}\r\n");
                }
                else
                {
                    txtTestResults.AppendText($"❌ ТУРШИЛТ АМЖИЛТГҮЙ БОЛЛОО\r\n");
                    txtTestResults.AppendText($"Алдаа: {testResult.ErrorMessage}\r\n");
                }
            }
            catch (Exception ex)
            {
                txtTestResults.AppendText($"❌ ТУРШИЛТЫН АЛДАА\r\n");
                txtTestResults.AppendText($"Exception: {ex.Message}\r\n");
                txtTestResults.AppendText($"Stack Trace: {ex.StackTrace}\r\n");
            }
            finally
            {
                // Товчийг дахин идэвхжүүлэх
                btnRunConcurrentTest.Enabled = true;
                btnRunConcurrentTest.Text = "🚀 Зэрэгцээ туршилт ажиллуулах";
            }
        }

        private void NavigateToTab(TabPage? tab)
        {
            if (tab != null && tabControl != null)
            {
                tabControl.SelectedTab = tab;
            }
        }

        /// <summary>
        /// Update status bar message
        /// </summary>
        /// <param name="message">Status message</param>
        private void UpdateStatus(string message)
        {
            if (statusStrip?.Items.Count > 0)
            {
                var statusLabel = statusStrip.Items[0] as ToolStripStatusLabel;
                if (statusLabel != null)
                {
                    statusLabel.Text = message;
                }
            }
        }

        /// <summary>
        /// Get background color for flight status
        /// </summary>
        /// <param name="status">Flight status</param>
        /// <returns>Background color</returns>
        private Color GetStatusBackColor(string status)
        {
            return status switch
            {
                "Бүртгэж байна" => Color.LightBlue,
                "Онгоцонд сууж байна" => Color.LightGoldenrodYellow,
                "Ниссэн" => Color.LightGreen,
                "Хойшилсон" => Color.LightPink,
                "Цуцалсан" => Color.LightCoral,
                _ => Color.White
            };
        }

        /// <summary>
        /// Get text color for flight status
        /// </summary>
        /// <param name="status">Flight status</param>
        /// <returns>Text color</returns>
        private Color GetStatusColor(string status)
        {
            return status switch
            {
                "Бүртгэж байна" => Color.Blue,
                "Онгоцонд сууж байна" => Color.Orange,
                "Ниссэн" => Color.Green,
                "Хойшилсон" => Color.Red,
                "Цуцалсан" => Color.DarkRed,
                _ => Color.Black
            };
        }

        #endregion

        #region Management Methods

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

        #endregion

        #region Check-in Methods

        private async Task SearchPassenger()
        {
            if (string.IsNullOrWhiteSpace(txtPassportSearch?.Text))
            {
                MessageBox.Show("Please enter a passport number.", "Input Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCheckinFlight?.SelectedItem is not FlightComboBoxItem selectedFlight)
            {
                MessageBox.Show("Please select a flight first.", "Flight Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var result = await _checkinService.ValidateFlightPassengerAsync(
                    selectedFlight.Flight.Id, txtPassportSearch.Text);

                // Debug: Show what we got from the API
                System.Diagnostics.Debug.WriteLine($"API Result - Success: {result.IsSuccess}");
                if (result.Data != null)
                {
                    System.Diagnostics.Debug.WriteLine($"API Result - IsValid: {result.Data.IsValid}");
                    System.Diagnostics.Debug.WriteLine($"API Result - IsBooked: {result.Data.IsBooked}");
                    System.Diagnostics.Debug.WriteLine($"API Result - Message: {result.Data.Message}");
                    System.Diagnostics.Debug.WriteLine($"API Result - Passenger: {result.Data.Passenger?.FirstName} {result.Data.Passenger?.LastName}");
                    System.Diagnostics.Debug.WriteLine($"API Result - Flight: {result.Data.Flight?.FlightNumber}");
                }

                if (result.IsSuccess && result.Data != null && result.Data.IsValid && result.Data.IsBooked)
                    {
                        if (txtPassengerInfo != null)
                        {
                        txtPassengerInfo.Text = $"Name: {result.Data.Passenger.FirstName} {result.Data.Passenger.LastName}\n" +
                                              $"Passport: {result.Data.Passenger.PassportNumber}\n" +
                                              $"Nationality: {result.Data.Passenger.Nationality}\n" +
                                              $"Booking Ref: {result.Data.BookingReference}\n" +
                                              $"Checked In: {(result.Data.IsCheckedIn ? "Yes" : "No")}";
                    }

                    // Enable seat map loading if passenger is found and not checked in
                    if (btnLoadSeatMap != null && !result.Data.IsCheckedIn)
                    {
                        btnLoadSeatMap.Enabled = true;
                        btnLoadSeatMap.Text = "Load Seat Map";
                    }
                    else if (btnLoadSeatMap != null && result.Data.IsCheckedIn)
                    {
                        btnLoadSeatMap.Enabled = false;
                        btnLoadSeatMap.Text = "Already Checked In";
                    }

                    // Reset check-in button state
                    if (btnCheckin != null)
                    {
                        btnCheckin.Enabled = false;
                        btnCheckin.Text = "Check-in Passenger";
                    }

                    // Clear any previously selected seat
                    _selectedSeat = null;
                }
                else
                {
                    var errorMessage = result.IsSuccess ? 
                        $"API returned success but data is invalid. Message: {result.Data?.Message}" :
                        $"API call failed: {result.ErrorMessage}";
                    
                    MessageBox.Show($"Passenger search failed for passport {txtPassportSearch.Text} on flight {selectedFlight.Flight.FlightNumber}.\n\nDetails: {errorMessage}", "Search Failed", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    if (txtPassengerInfo != null)
                        txtPassengerInfo.Clear();
                    
                    // Disable seat map loading if passenger not found
                    if (btnLoadSeatMap != null)
                    {
                        btnLoadSeatMap.Enabled = false;
                        btnLoadSeatMap.Text = "Load Seat Map";
                    }

                    // Disable check-in button if passenger not found
                    if (btnCheckin != null)
                    {
                        btnCheckin.Enabled = false;
                        btnCheckin.Text = "Check-in Passenger";
                    }

                    // Clear any previously selected seat
                    _selectedSeat = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching passenger: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Disable buttons on error
                if (btnLoadSeatMap != null)
                {
                    btnLoadSeatMap.Enabled = false;
                    btnLoadSeatMap.Text = "Load Seat Map";
                }
                if (btnCheckin != null)
                {
                    btnCheckin.Enabled = false;
                    btnCheckin.Text = "Check-in Passenger";
                }
                _selectedSeat = null;
            }
        }

        private SeatInfoDto? _selectedSeat = null;

        private async Task PerformCheckin()
        {
            if (string.IsNullOrWhiteSpace(txtPassportSearch?.Text))
            {
                MessageBox.Show("Please search for a passenger first.", "Passenger Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCheckinFlight?.SelectedItem is not FlightComboBoxItem selectedFlight)
            {
                MessageBox.Show("Please select a flight first.", "Flight Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_selectedSeat == null)
            {
                MessageBox.Show("Please select a seat first.", "Seat Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if flight status allows check-in
            var status = selectedFlight.Flight.Status;
            var isValidCheckinStatus = status == "Бүртгэж байна" || status == "CheckinOpen";
            
            if (!isValidCheckinStatus)
            {
                MessageBox.Show($"Cannot check in passenger. Flight {selectedFlight.Flight.FlightNumber} is not in check-in status.\nCurrent status: {status}\n\nPlease change the flight status to 'CheckinOpen' first.", 
                    "Flight Status Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if check-in time has started
            var now = DateTime.UtcNow;
            var localNow = DateTime.Now;
            var checkinOpenTime = selectedFlight.Flight.CheckinOpenTime;
            
            // Convert check-in open time to local time for comparison
            var checkinOpenLocal = checkinOpenTime.ToLocalTime();
            
            if (checkinOpenTime > now)
            {
                // Show times in both UTC and local timezone for clarity
                var message = $"Check-in time validation failed for flight {selectedFlight.Flight.FlightNumber}.\n\n" +
                             $"Check-in opens at: {checkinOpenTime:yyyy-MM-dd HH:mm} UTC ({checkinOpenLocal:yyyy-MM-dd HH:mm} Local)\n" +
                             $"Current time: {now:yyyy-MM-dd HH:mm} UTC ({localNow:yyyy-MM-dd HH:mm} Local)\n\n" +
                             $"This might be due to timezone issues when the flight was created.\n" +
                             $"Since the flight status is 'CheckinOpen', do you want to proceed with check-in anyway?";
                
                var result = MessageBox.Show(message, "Check-in Time Validation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.No)
                {
                    return;
                }
                
                // Log the override for debugging
                System.Diagnostics.Debug.WriteLine($"WARNING: Check-in time validation overridden by user. CheckinOpenTime: {checkinOpenTime}, Current Time: {now}");
            }

            try
            {
                btnCheckin.Enabled = false;
                btnCheckin.Text = "Checking in...";

                var request = new CheckinRequestDto
                {
                    FlightNumber = selectedFlight.Flight.FlightNumber,
                    PassportNumber = txtPassportSearch.Text,
                    EmployeeId = _currentEmployee.Id,
                    PreferredSeatId = _selectedSeat.Id
                };

                // Debug: Show request details
                var debugUtcNow = DateTime.UtcNow;
                var debugLocalNow = DateTime.Now;
                var debugCheckinOpenTime = selectedFlight.Flight.CheckinOpenTime;
                var debugCheckinCloseTime = selectedFlight.Flight.CheckinCloseTime;
                
                System.Diagnostics.Debug.WriteLine($"Checkin Request Details:");
                System.Diagnostics.Debug.WriteLine($"  Flight Number: {request.FlightNumber}");
                System.Diagnostics.Debug.WriteLine($"  Passport: {request.PassportNumber}");
                System.Diagnostics.Debug.WriteLine($"  Employee ID: {request.EmployeeId}");
                System.Diagnostics.Debug.WriteLine($"  Preferred Seat ID: {request.PreferredSeatId}");
                System.Diagnostics.Debug.WriteLine($"  Flight Status: {selectedFlight.Flight.Status}");
                System.Diagnostics.Debug.WriteLine($"  Current Time UTC: {debugUtcNow:yyyy-MM-dd HH:mm:ss}");
                System.Diagnostics.Debug.WriteLine($"  Current Time Local: {debugLocalNow:yyyy-MM-dd HH:mm:ss}");
                System.Diagnostics.Debug.WriteLine($"  CheckinOpenTime UTC: {debugCheckinOpenTime:yyyy-MM-dd HH:mm:ss}");
                System.Diagnostics.Debug.WriteLine($"  CheckinOpenTime Local: {debugCheckinOpenTime.ToLocalTime():yyyy-MM-dd HH:mm:ss}");
                System.Diagnostics.Debug.WriteLine($"  CheckinCloseTime UTC: {debugCheckinCloseTime:yyyy-MM-dd HH:mm:ss}");
                System.Diagnostics.Debug.WriteLine($"  CheckinCloseTime Local: {debugCheckinCloseTime.ToLocalTime():yyyy-MM-dd HH:mm:ss}");
                System.Diagnostics.Debug.WriteLine($"  Time Difference (minutes): {(debugUtcNow - debugCheckinOpenTime).TotalMinutes:F1}");

                var result = await _checkinService.CheckinPassengerAsync(request);

                if (result.IsSuccess)
                {
                    MessageBox.Show($"Passenger checked in successfully!\nSeat: {_selectedSeat.SeatNumber}", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Refresh passenger info
                    await SearchPassenger();
                    
                    // Clear selected seat
                    _selectedSeat = null;
                }
                else
                {
                    MessageBox.Show($"Failed to check in passenger: {result.ErrorMessage}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking in passenger: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCheckin.Enabled = true;
                btnCheckin.Text = "Check-in Passenger";
            }
        }

        private async Task PrintBoardingPass()
        {
            if (string.IsNullOrWhiteSpace(txtPassportSearch?.Text))
            {
                MessageBox.Show("Please search for a passenger first.", "Passenger Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCheckinFlight?.SelectedItem is not FlightComboBoxItem selectedFlight)
            {
                MessageBox.Show("Please select a flight first.", "Flight Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Debug: Show API call details
                System.Diagnostics.Debug.WriteLine($"Boarding Pass API Call:");
                System.Diagnostics.Debug.WriteLine($"  Flight ID: {selectedFlight.Flight.Id}");
                System.Diagnostics.Debug.WriteLine($"  Passport: {txtPassportSearch.Text}");
                System.Diagnostics.Debug.WriteLine($"  API Endpoint: /api/Checkin/boarding-pass/{selectedFlight.Flight.Id}/{txtPassportSearch.Text}");

                // First try to get the boarding pass from the API
                var result = await _checkinService.GetBoardingPassAsync(
                    selectedFlight.Flight.Id, txtPassportSearch.Text);

                System.Diagnostics.Debug.WriteLine($"Boarding Pass API Result:");
                System.Diagnostics.Debug.WriteLine($"  Success: {result.IsSuccess}");
                System.Diagnostics.Debug.WriteLine($"  Error: {result.ErrorMessage}");
                System.Diagnostics.Debug.WriteLine($"  Data: {(result.Data != null ? "Available" : "Null")}");

                if (result.IsSuccess && result.Data != null)
                {
                    // Show boarding pass in a proper form with print functionality
                    var boardingPassForm = new BoardingPassForm(result.Data);
                    boardingPassForm.ShowDialog();
                }
                else
                {
                    // If API fails, create boarding pass from validation data
                    MessageBox.Show($"Boarding pass API failed: {result.ErrorMessage}\n\nCreating boarding pass from check-in data...", 
                        "API Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    await CreateBoardingPassFromCheckinData(selectedFlight.Flight.Id, txtPassportSearch.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing boarding pass: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CreateBoardingPassFromCheckinData(int flightId, string passportNumber)
        {
            try
            {
                // Get passenger validation data which contains all the information we need
                var validationResult = await _checkinService.ValidateFlightPassengerAsync(flightId, passportNumber);
                
                if (validationResult.IsSuccess && validationResult.Data != null)
                {
                    // Create a boarding pass DTO from the validation data
                    var boardingPass = new BoardingPassDto
                    {
                        BoardingPassCode = validationResult.Data.BookingReference ?? $"BP{DateTime.Now:yyyyMMddHHmmss}",
                        IssuedAt = DateTime.UtcNow,
                        IssuedByEmployee = _currentEmployee?.FirstName + " " + _currentEmployee?.LastName,
                        BoardingTime = validationResult.Data.Flight?.ScheduledDeparture.AddMinutes(-30) ?? DateTime.UtcNow.AddHours(1),
                        IsBoardingComplete = false,
                        Gate = validationResult.Data.Flight?.GateNumber ?? "TBA",
                        Flight = validationResult.Data.Flight,
                        Passenger = validationResult.Data.Passenger,
                        Seat = validationResult.Data.FlightPassenger?.AssignedSeat
                    };

                    // Show boarding pass in a proper form with print functionality
                    var boardingPassForm = new BoardingPassForm(boardingPass);
                    boardingPassForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Cannot create boarding pass. Passenger not found or not checked in.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating boarding pass: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Menu Setup

        private void SetupMenuStrip()
        {
            // File Menu
            var fileMenu = new ToolStripMenuItem("&File");
            fileMenu.DropDownItems.Add("&New Flight", null, async (s, e) => await _formController.ShowAddFlightDialogAsync());
            fileMenu.DropDownItems.Add("&New Passenger", null, async (s, e) => await _formController.ShowAddPassengerDialogAsync());
            fileMenu.DropDownItems.Add("&New Booking", null, async (s, e) => await _formController.ShowAddBookingDialogAsync());
            fileMenu.DropDownItems.Add("-");
            fileMenu.DropDownItems.Add("&Exit", null, (s, e) => Close());
            menuStrip.Items.Add(fileMenu);

            // Flight Menu
            var flightMenu = new ToolStripMenuItem("&Flight");
            flightMenu.DropDownItems.Add("&Refresh Flights", null, async (s, e) => await LoadFlightsAsync());
            flightMenu.DropDownItems.Add("&Manage Flights", null, (s, e) => NavigateToTab(tabFlights));
            flightMenu.DropDownItems.Add("&Add Flight", null, async (s, e) => await _formController.ShowAddFlightDialogAsync());
            menuStrip.Items.Add(flightMenu);

            // Passenger Menu
            var passengerMenu = new ToolStripMenuItem("&Passenger");
            passengerMenu.DropDownItems.Add("&Refresh Passengers", null, async (s, e) => await LoadPassengersAsync());
            passengerMenu.DropDownItems.Add("&Manage Passengers", null, (s, e) => NavigateToTab(tabPassengers));
            passengerMenu.DropDownItems.Add("&Add Passenger", null, async (s, e) => await _formController.ShowAddPassengerDialogAsync());
            menuStrip.Items.Add(passengerMenu);

            // Check-in Menu
            var checkinMenu = new ToolStripMenuItem("&Check-in");
            checkinMenu.DropDownItems.Add("&New Check-in", null, (s, e) => NavigateToTab(tabCheckin));
            checkinMenu.DropDownItems.Add("&Manage Bookings", null, (s, e) => NavigateToTab(tabBookings));
            checkinMenu.DropDownItems.Add("&Add Booking", null, async (s, e) => await _formController.ShowAddBookingDialogAsync());
            menuStrip.Items.Add(checkinMenu);

            // View Menu
            var viewMenu = new ToolStripMenuItem("&View");
            viewMenu.DropDownItems.Add("&Dashboard", null, (s, e) => NavigateToTab(tabDashboard));
            viewMenu.DropDownItems.Add("&Refresh All", null, async (s, e) => await LoadAllDataAsync());
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

        #endregion

        #region SignalR

        private async Task SetupSignalRAsync()
            {
                try
                {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:7261/flightHub")
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

        private void OnFlightUpdated(FlightInfoDto flight)
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

        #endregion

        #region Public Refresh Methods (for FormController)

        public async Task RefreshFlightsAsync()
        {
            await LoadFlightsAsync();
        }

        public async Task RefreshPassengersAsync()
        {
            await LoadPassengersAsync();
        }

        public async Task RefreshFlightPassengersAsync()
        {
            await LoadFlightPassengersAsync();
        }

        public async Task RefreshAllDataAsync()
        {
            await LoadAllDataAsync();
        }

        #endregion

        #region Event Handlers

        private void OnDataChanged(object? sender, string dataType)
        {
            // Handle data change events from FormController
            _ = Task.Run(async () =>
            {
                switch (dataType.ToLower())
                {
                    case "flight":
                        await LoadFlightsAsync();
                        break;
                    case "passenger":
                        await LoadPassengersAsync();
                        break;
                    case "booking":
                    case "flightpassenger":
                        await LoadFlightPassengersAsync();
                        break;
                    case "flightstatus":
                        await LoadFlightsAsync();
                        break;
                    default:
                        await LoadAllDataAsync();
                        break;
                }
            });
        }

        private void ShowAboutDialog()
        {
            MessageBox.Show("Flight System Check-in Application\nVersion 2.0\n\nProfessional Check-in Management System", 
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region FlightComboBoxItem Helper Class

        private class FlightComboBoxItem
        {
            public FlightInfoDto Flight { get; set; }
            public string DisplayText { get; set; }

            public FlightComboBoxItem(FlightInfoDto flight)
            {
                Flight = flight;
                DisplayText = $"{flight.FlightNumber} - {flight.DepartureAirport} → {flight.ArrivalAirport} ({flight.ScheduledDeparture:yyyy-MM-dd HH:mm})";
            }

            public override string ToString() => DisplayText;
        }

        #endregion

        #region Cleanup

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timerTimeUpdate?.Stop();
            _hubConnection?.StopAsync();
            _checkinService?.Dispose();
            base.OnFormClosed(e);
        }

        #endregion

        #region FlightDisplayItem Helper Class

        private class FlightDisplayItem
        {
            public FlightInfoDto Flight { get; set; }
            public string DisplayText { get; set; }

            public FlightDisplayItem(FlightInfoDto flight)
            {
                Flight = flight;
                DisplayText = $"{flight.FlightNumber} - {flight.DepartureAirport} → {flight.ArrivalAirport} ({flight.ScheduledDeparture:yyyy-MM-dd HH:mm})";
            }

            public override string ToString() => DisplayText;
        }

        #endregion
    }
}
