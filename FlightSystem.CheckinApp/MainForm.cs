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
using FlightSystem.Core.Enums;
using Microsoft.AspNetCore.SignalR.Client;

namespace FlightSystem.CheckinApp
{
    public partial class MainForm : Form
    {
        private readonly HttpClient _httpClient;
        private HubConnection? _hubConnection;
        private EmployeeDto? _currentEmployee;
        private FlightInfoDto? _selectedFlight;
        private readonly string _serverUrl = "https://localhost:7261";

        public MainForm()
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri(_serverUrl) };
            SetupSignalR();
        }

        private async void SetupSignalR()
        {
            try
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl($"{_serverUrl}/flightHub")
                    .Build();

                _hubConnection.On<FlightInfoDto>("FlightUpdated", OnFlightUpdated);
                _hubConnection.On<string>("SystemMessage", OnSystemMessage);
                
                await _hubConnection.StartAsync();
                LogMessage("SignalR холболт амжилттай үүслээ");
            }
            catch (Exception ex)
            {
                LogMessage($"SignalR холболт амжилтгүй: {ex.Message}");
            }
        }

        private void OnFlightUpdated(FlightInfoDto flight)
        {
            if (InvokeRequired)
            {
                Invoke(() => OnFlightUpdated(flight));
                return;
            }

            LogMessage($"Нислэг {flight.FlightNumber} төлөв өөрчлөгдлөө: {flight.Status}");
            if (_selectedFlight?.Id == flight.Id)
            {
                _selectedFlight = flight;
                UpdateFlightInfo();
            }
        }

        private void OnSystemMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(() => OnSystemMessage(message));
                return;
            }

            LogMessage($"Систем: {message}");
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var employeeCode = txtEmployeeCode.Text.Trim();
            var password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(employeeCode) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Ажилтны код болон нууц үгээ оруулна уу", "Алдаа", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var loginDto = new EmployeeLoginDto
                {
                    EmployeeCode = employeeCode,
                    Password = password
                };

                var json = JsonSerializer.Serialize(loginDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("/api/employee/login", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<EmployeeLoginResultDto>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true && result.Data?.IsSuccess == true)
                {
                    _currentEmployee = result.Data.Employee;
                    panelLogin.Visible = false;
                    panelMain.Visible = true;
                    lblWelcome.Text = $"Тавтай морил, {_currentEmployee?.FullName ?? "Ажилтан"}";
                    
                    await LoadActiveFlights();
                    LogMessage($"Амжилттай нэвтэрлээ: {_currentEmployee?.FullName ?? "Ажилтан"}");
                }
                else
                {
                    MessageBox.Show(result?.Message ?? "Нэвтрэх амжилтгүй", "Алдаа", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Нэвтрэхэд алдаа гарлаа: {ex.Message}", "Алдаа", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadActiveFlights()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/flight/active");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightInfoDto[]>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true && result.Data != null)
                {
                    cmbFlights.Items.Clear();
                    foreach (var flight in result.Data)
                    {
                        cmbFlights.Items.Add(flight);
                    }
                    cmbFlights.DisplayMember = "FlightNumber";
                    LogMessage($"{result.Data.Length} нислэг ачаалалаа");
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Нислэгүүд ачаалахад алдаа: {ex.Message}");
            }
        }

        private void cmbFlights_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedFlight = cmbFlights.SelectedItem as FlightInfoDto;
            if (_selectedFlight != null)
            {
                UpdateFlightInfo();
                _ = Task.Run(() => JoinFlightGroup(_selectedFlight.Id));
            }
        }

        private async Task JoinFlightGroup(int flightId)
        {
            try
            {
                if (_hubConnection?.State == HubConnectionState.Connected)
                {
                    await _hubConnection.InvokeAsync("JoinFlightGroup", flightId);
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Нислэгийн бүлэгт нэгдэхэд алдаа: {ex.Message}");
            }
        }

        private void UpdateFlightInfo()
        {
            if (_selectedFlight == null) return;

            lblFlightInfo.Text = $"Нислэг: {_selectedFlight.FlightNumber}\n" +
                               $"Чиглэл: {_selectedFlight.DepartureAirport} → {_selectedFlight.ArrivalAirport}\n" +
                               $"Төлөв: {_selectedFlight.Status}\n" +
                               $"Хөөрөх: {_selectedFlight.ScheduledDeparture:HH:mm}\n" +
                               $"Хаалга: {_selectedFlight.GateNumber ?? "Тодорхойгүй"}";
        }

        private async void btnSearchPassenger_Click(object sender, EventArgs e)
        {
            var passport = txtPassportNumber.Text.Trim();
            if (string.IsNullOrEmpty(passport))
            {
                MessageBox.Show("Пасспортын дугаар оруулна уу", "Алдаа", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var response = await _httpClient.GetAsync($"/api/passenger/passport/{passport}");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<PassengerDto>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true && result.Data != null)
                {
                    var passenger = result.Data;
                    lblPassengerInfo.Text = $"Зорчигч: {passenger.FullName}\n" +
                                          $"Пасспорт: {passenger.PassportNumber}\n" +
                                          $"Төрсөн огноо: {passenger.DateOfBirth:yyyy-MM-dd}\n" +
                                          $"Үндэс: {passenger.Nationality}";
                    
                    await LoadSeatMap();
                    LogMessage($"Зорчигч олдлоо: {passenger.FullName}");
                }
                else
                {
                    MessageBox.Show("Зорчигч олдсонгүй", "Мэдээлэл", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblPassengerInfo.Text = "Зорчигч олдсонгүй";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Зорчигч хайхад алдаа гарлаа: {ex.Message}", "Алдаа", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadSeatMap()
        {
            if (_selectedFlight == null) return;

            try
            {
                panelSeats.Controls.Clear();
                
                var response = await _httpClient.GetAsync($"/api/checkin/seatmap/{_selectedFlight.Id}");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<SeatMapDto>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true && result.Data != null)
                {
                    CreateSeatButtons(result.Data);
                    LogMessage($"Суудлын зураглал : {result.Data.AvailableSeats}/{result.Data.TotalSeats} чөлөөтэй");
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Суудлын зураглал ачаалахад алдаа: {ex.Message}");
            }
        }

        private void CreateSeatButtons(SeatMapDto seatMap)
        {
            const int seatWidth = 25;
            const int seatHeight = 20;
            const int seatSpacing = 2;
            const int aisleSpacing = 15;
            const int startX = 30;
            const int startY = 5;
            
            // Clear all seat panels
            panelSeats.Controls.Clear();
            grpBusinessClass.Controls.Clear();
            grpPremiumEconomyClass.Controls.Clear();
            grpEconomyClass.Controls.Clear();

            // Create realistic Boeing 737 layout
            CreateBoeing737Layout(seatMap);
        }

        private void CreateBoeing737Layout(SeatMapDto seatMap)
        {
            const int seatWidth = 25;
            const int seatHeight = 20;
            const int seatSpacing = 2;
            const int aisleSpacing = 15;
            const int startX = 30;
            const int startY = 5;

            // Boeing 737 typical configuration: 3-3 seating (A-B-C aisle D-E-F)
            string[] seatColumns = { "A", "B", "C", "D", "E", "F" };
            
            int currentY = startY;
            Control currentContainer = panelSeats;

            foreach (var row in seatMap.SeatRows)
            {
                var seatClass = DetermineSeatClassFromRow(row.Row);
                
                // Switch to appropriate container
                switch (seatClass)
                {
                    case SeatClass.First:
                        currentContainer = panelSeats;
                        break;
                    case SeatClass.Business:
                        currentContainer = grpBusinessClass;
                        break;
                    case SeatClass.PremiumEconomy:
                        currentContainer = grpPremiumEconomyClass;
                        break;
                    case SeatClass.Economy:
                        currentContainer = grpEconomyClass;
                        break;
                }

                // Add row number label
                var lblRow = new Label
                {
                    Text = row.Row,
                    Location = new Point(5, currentY + 2),
                    Size = new Size(20, 16),
                    Font = new Font("Arial", 7, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(240, 240, 240)
                };
                currentContainer.Controls.Add(lblRow);

                int currentX = startX;
                
                // Create seats in Boeing 737 configuration (A-B-C aisle D-E-F)
                foreach (var column in seatColumns)
                {
                    var seat = row.Seats.FirstOrDefault(s => s.Column == column);
                    if (seat == null) continue;

                    var btnSeat = new Button
                    {
                        Text = $"{row.Row}{column}",
                        Location = new Point(currentX, currentY),
                        Size = new Size(seatWidth, seatHeight),
                        Font = new Font("Arial", 6, FontStyle.Bold),
                        Tag = seat,
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderSize = 1 }
                    };

                    // Apply seat styling based on status and type
                    ApplySeatStyling(btnSeat, seat, seatClass);

                    btnSeat.Click += BtnSeat_Click;
                    currentContainer.Controls.Add(btnSeat);

                    currentX += seatWidth + seatSpacing;
                    
                    // Add aisle spacing after column C
                    if (column == "C")
                    {
                        currentX += aisleSpacing;
                    }
                }

                currentY += seatHeight + seatSpacing;
                
                // Reset Y position for new seat class
                if (currentY > 60) // If we exceed container height, reset
                {
                    currentY = startY;
                }
            }

            // Add aisle labels
            AddAisleLabels(currentContainer);
        }

        private void ApplySeatStyling(Button btnSeat, SeatInfoDto seat, SeatClass seatClass)
        {
            // Base colors for each seat class
            Color baseColor = seatClass switch
            {
                SeatClass.First => Color.FromArgb(139, 69, 19),      // Gold/Brown
                SeatClass.Business => Color.FromArgb(25, 118, 210),  // Blue
                SeatClass.PremiumEconomy => Color.FromArgb(156, 39, 176), // Purple
                SeatClass.Economy => Color.FromArgb(76, 175, 80),    // Green
                _ => Color.FromArgb(200, 200, 200)                   // Gray
            };

            if (!seat.IsAvailable)
            {
                btnSeat.BackColor = Color.FromArgb(244, 67, 54);     // Red
                btnSeat.ForeColor = Color.White;
                btnSeat.Enabled = false;
                btnSeat.Text = "✕";
                btnSeat.FlatAppearance.BorderColor = Color.FromArgb(183, 28, 28);
            }
            else if (seat.IsEmergencyExit)
            {
                btnSeat.BackColor = Color.FromArgb(255, 152, 0);     // Orange
                btnSeat.ForeColor = Color.White;
                btnSeat.FlatAppearance.BorderColor = Color.FromArgb(230, 81, 0);
                btnSeat.Text = $"{btnSeat.Text}⚠";
            }
            else if (seat.IsWindowSeat)
            {
                btnSeat.BackColor = Color.FromArgb(33, 150, 243);    // Blue
                btnSeat.ForeColor = Color.White;
                btnSeat.FlatAppearance.BorderColor = Color.FromArgb(21, 101, 192);
            }
            else if (seat.IsAisleSeat)
            {
                btnSeat.BackColor = Color.FromArgb(76, 175, 80);     // Green
                btnSeat.ForeColor = Color.White;
                btnSeat.FlatAppearance.BorderColor = Color.FromArgb(56, 142, 60);
            }
            else
            {
                btnSeat.BackColor = Color.FromArgb(250, 250, 250);  // Light Gray
                btnSeat.ForeColor = Color.Black;
                btnSeat.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            }
        }

        private void AddAisleLabels(Control container)
        {
            // Add aisle label
            var lblAisle = new Label
            {
                Text = "AISLE",
                Location = new Point(120, 25),
                Size = new Size(30, 15),
                Font = new Font("Arial", 5, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Gray,
                BackColor = Color.Transparent
            };
            container.Controls.Add(lblAisle);
        }

        private SeatClass DetermineSeatClassFromRow(string rowNumber)
        {
            if (int.TryParse(rowNumber, out int row))
            {
                if (row <= 2) return SeatClass.First;
                if (row <= 6) return SeatClass.Business;
                if (row <= 12) return SeatClass.PremiumEconomy;
                return SeatClass.Economy;
            }
            return SeatClass.Economy;
        }

        private async void BtnSeat_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btnSeat || btnSeat.Tag is not SeatInfoDto seat) return;
            if (string.IsNullOrEmpty(txtPassportNumber.Text.Trim()) || _selectedFlight == null)
            {
                MessageBox.Show("Эхлээд зорчигчийг хайна уу", "Алдаа", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var checkinRequest = new CheckinRequestDto
                {
                    FlightNumber = _selectedFlight.FlightNumber,
                    PassportNumber = txtPassportNumber.Text.Trim(),
                    EmployeeId = _currentEmployee!.Id,
                    PreferredSeatId = seat.Id
                };

                var json = JsonSerializer.Serialize(checkinRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("/api/checkin/passenger", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<CheckinResultDto>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true && result.Data?.IsSuccess == true)
                {
                    MessageBox.Show($"Check-in амжилттай!\nСуудал: {seat.SeatNumber}", "Амжилт", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    btnSeat.BackColor = Color.Red;
                    btnSeat.Text = "X";
                    btnSeat.Enabled = false;
                    
                    LogMessage($"Check-in амжилттай: {txtPassportNumber.Text} - {seat.SeatNumber}");
                    
                    // Boarding pass хэвлэх
                    if (result.Data.BoardingPass != null)
                    {
                        await PrintBoardingPass(result.Data.BoardingPass);
                    }
                }
                else
                {
                    MessageBox.Show(result?.Message ?? "Check-in амжилтгүй", "Алдаа", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Check-in хийхэд алдаа гарлаа: {ex.Message}", "Алдаа", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Task PrintBoardingPass(BoardingPassDto boardingPass)
        {
            try
            {
                // Энд boarding pass хэвлэх логик бичнэ
                // Одоогоор зөвхөн мэдээллийг харуулж байна
                var printInfo = $"=== BOARDING PASS ===\n" +
                              $"Зорчигч: {boardingPass.Passenger.FullName}\n" +
                              $"Нислэг: {boardingPass.Flight.FlightNumber}\n" +
                              $"Суудал: {boardingPass.Seat.SeatNumber}\n" +
                              $"Хаалга: {boardingPass.Gate}\n" +
                              $"Хөөрөх: {boardingPass.Flight.ScheduledDeparture:yyyy-MM-dd HH:mm}\n" +
                              $"Код: {boardingPass.BoardingPassCode}";

                MessageBox.Show(printInfo, "Boarding Pass", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                LogMessage($"Boarding pass хэвлэгдлээ: {boardingPass.BoardingPassCode}");
            }
            catch (Exception ex)
            {
                LogMessage($"Boarding pass хэвлэхэд алдаа: {ex.Message}");
            }
            
            return Task.CompletedTask;
        }

        private async void btnUpdateFlightStatus_Click(object sender, EventArgs e)
        {
            if (_selectedFlight == null)
            {
                MessageBox.Show("Нислэг сонгоно уу", "Алдаа", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var statusForm = new FlightStatusForm(_selectedFlight);
            if (statusForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var updateDto = new UpdateFlightStatusDto
                    {
                        Status = statusForm.SelectedStatus,
                        UpdatedByEmployeeId = _currentEmployee!.Id
                    };

                    var json = JsonSerializer.Serialize(updateDto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    
                    var response = await _httpClient.PutAsync($"/api/flight/{_selectedFlight.Id}/status", content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    
                    var result = JsonSerializer.Deserialize<ApiResponseDto<FlightInfoDto>>(
                        responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (result?.Success == true && result.Data != null)
                    {
                        _selectedFlight = result.Data;
                        UpdateFlightInfo();
                        LogMessage($"Нислэгийн төлөв амжилттай өөрчлөгдлөө: {result.Data.Status}");
                        
                        MessageBox.Show("Нислэгийн төлөв амжилттай өөрчлөгдлөө", "Амжилт", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(result?.Message ?? "Төлөв өөрчлөх амжилтгүй", "Алдаа", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Төлөв өөрчлөхөд алдаа гарлаа: {ex.Message}", "Алдаа", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LogMessage(string message)
        {
            var logEntry = $"[{DateTime.Now:HH:mm:ss}] {message}";
            
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(() =>
                {
                    txtLog.AppendText(logEntry + Environment.NewLine);
                    txtLog.ScrollToCaret();
                });
            }
            else
            {
                txtLog.AppendText(logEntry + Environment.NewLine);
                txtLog.ScrollToCaret();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _hubConnection?.DisposeAsync();
            _httpClient?.Dispose();
            base.OnFormClosed(e);
        }
    }
}