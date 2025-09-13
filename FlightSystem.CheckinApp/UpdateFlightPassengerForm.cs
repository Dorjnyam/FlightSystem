using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightSystem.Shared.DTOs.Request;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.DTOs.Common;

namespace FlightSystem.CheckinApp
{
    public partial class UpdateFlightPassengerForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _serverUrl = "https://localhost:7261";
        private readonly FlightPassengerDto _flightPassenger;
        private readonly EmployeeDto _currentEmployee;

        public UpdateFlightPassengerForm(FlightPassengerDto flightPassenger, EmployeeDto employee)
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri(_serverUrl) };
            _flightPassenger = flightPassenger;
            _currentEmployee = employee;
            
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Display current flight and passenger info (read-only)
            lblFlightInfo.Text = $"{_flightPassenger.Flight.FlightNumber} - {_flightPassenger.Flight.DepartureAirport} → {_flightPassenger.Flight.ArrivalAirport}";
            lblPassengerInfo.Text = $"{_flightPassenger.Passenger.FirstName} {_flightPassenger.Passenger.LastName} ({_flightPassenger.Passenger.PassportNumber})";
            lblBookingReference.Text = _flightPassenger.BookingReference;
            lblCheckinStatus.Text = _flightPassenger.IsCheckedIn ? "Checked In" : "Not Checked In";
            lblCheckinStatus.ForeColor = _flightPassenger.IsCheckedIn ? Color.Green : Color.Red;
            
            // Load current values
            txtSpecialRequests.Text = _flightPassenger.SpecialRequests ?? "";
            txtBaggageInfo.Text = _flightPassenger.BaggageInfo ?? "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateFlightPassenger();
        }

        private async void UpdateFlightPassenger()
        {
            try
            {
                btnUpdate.Enabled = false;
                btnUpdate.Text = "Updating...";

                var updateDto = new UpdateFlightPassengerDto
                {
                    SpecialRequests = string.IsNullOrWhiteSpace(txtSpecialRequests.Text) ? null : txtSpecialRequests.Text.Trim(),
                    BaggageInfo = string.IsNullOrWhiteSpace(txtBaggageInfo.Text) ? null : txtBaggageInfo.Text.Trim(),
                    UpdatedByEmployeeId = _currentEmployee.Id
                };

                var json = JsonSerializer.Serialize(updateDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync($"/api/flightpassenger/{_flightPassenger.Id}", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightPassengerDto>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true)
                {
                    MessageBox.Show("Flight passenger booking updated successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(result?.Message ?? "Failed to update booking", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating booking: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnUpdate.Enabled = true;
                btnUpdate.Text = "Update Booking";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _httpClient?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
