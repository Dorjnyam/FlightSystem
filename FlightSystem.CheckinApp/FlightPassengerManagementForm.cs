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

namespace FlightSystem.CheckinApp
{
    public partial class FlightPassengerManagementForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _serverUrl = "https://localhost:7261";
        private readonly EmployeeDto _currentEmployee;
        private FlightPassengerDto? _selectedFlightPassenger;

        public FlightPassengerManagementForm(EmployeeDto employee)
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri(_serverUrl) };
            _currentEmployee = employee;
            
            InitializeForm();
        }

        private void InitializeForm()
        {
            lblWelcome.Text = $"FlightPassenger Management - {_currentEmployee.FullName}";
            LoadFlightPassengers();
        }

        private async void LoadFlightPassengers()
        {
            try
            {
                btnRefresh.Enabled = false;
                btnRefresh.Text = "Loading...";
                lstFlightPassengers.Items.Clear();

                var response = await _httpClient.GetAsync("/api/flightpassenger");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightPassengerDto[]>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true && result.Data != null)
                {
                    foreach (var fp in result.Data.OrderBy(f => f.Flight.FlightNumber).ThenBy(f => f.Passenger.LastName))
                    {
                        var item = new ListViewItem(fp.BookingReference);
                        item.SubItems.Add(fp.Flight.FlightNumber);
                        item.SubItems.Add($"{fp.Passenger.FirstName} {fp.Passenger.LastName}");
                        item.SubItems.Add(fp.Passenger.PassportNumber);
                        item.SubItems.Add(fp.IsCheckedIn ? "Yes" : "No");
                        item.SubItems.Add(fp.Flight.ScheduledDeparture.ToString("yyyy-MM-dd"));
                        item.Tag = fp;
                        lstFlightPassengers.Items.Add(item);
                    }
                    
                    lblStatus.Text = $"{result.Data.Length} flight passengers loaded";
                    lblStatus.ForeColor = Color.Green;
                }
                else
                {
                    lblStatus.Text = "No flight passengers found";
                    lblStatus.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show($"Error loading flight passengers: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRefresh.Enabled = true;
                btnRefresh.Text = "Refresh";
            }
        }

        private void lstFlightPassengers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFlightPassengers.SelectedItems.Count > 0)
            {
                _selectedFlightPassenger = lstFlightPassengers.SelectedItems[0].Tag as FlightPassengerDto;
                UpdateFlightPassengerDetails();
                EnableCrudButtons();
            }
            else
            {
                _selectedFlightPassenger = null;
                DisableCrudButtons();
            }
        }

        private void UpdateFlightPassengerDetails()
        {
            if (_selectedFlightPassenger == null)
            {
                txtDetails.Clear();
                return;
            }

            var details = $"Booking Reference: {_selectedFlightPassenger.BookingReference}\n" +
                         $"Flight: {_selectedFlightPassenger.Flight.FlightNumber}\n" +
                         $"Route: {_selectedFlightPassenger.Flight.DepartureAirport} → {_selectedFlightPassenger.Flight.ArrivalAirport}\n" +
                         $"Departure: {_selectedFlightPassenger.Flight.ScheduledDeparture:yyyy-MM-dd HH:mm}\n" +
                         $"Passenger: {_selectedFlightPassenger.Passenger.FirstName} {_selectedFlightPassenger.Passenger.LastName}\n" +
                         $"Passport: {_selectedFlightPassenger.Passenger.PassportNumber}\n" +
                         $"Checked In: {( _selectedFlightPassenger.IsCheckedIn ? "Yes" : "No")}\n" +
                         $"Check-in Time: {(_selectedFlightPassenger.CheckinTime?.ToString("yyyy-MM-dd HH:mm") ?? "Not checked in")}\n" +
                         $"Special Requests: {_selectedFlightPassenger.SpecialRequests ?? "None"}\n" +
                         $"Baggage Info: {_selectedFlightPassenger.BaggageInfo ?? "None"}";

            txtDetails.Text = details;
        }

        private void EnableCrudButtons()
        {
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnCancel.Enabled = _selectedFlightPassenger?.IsCheckedIn == true;
        }

        private void DisableCrudButtons()
        {
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var createForm = new CreateFlightPassengerForm(_currentEmployee);
            if (createForm.ShowDialog() == DialogResult.OK)
            {
                LoadFlightPassengers();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedFlightPassenger == null) return;

            var updateForm = new UpdateFlightPassengerForm(_selectedFlightPassenger, _currentEmployee);
            if (updateForm.ShowDialog() == DialogResult.OK)
            {
                LoadFlightPassengers();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedFlightPassenger == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete the booking for {_selectedFlightPassenger.Passenger.FirstName} {_selectedFlightPassenger.Passenger.LastName} on flight {_selectedFlightPassenger.Flight.FlightNumber}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    btnDelete.Enabled = false;
                    btnDelete.Text = "Deleting...";

                    var response = await _httpClient.DeleteAsync($"/api/flightpassenger/{_selectedFlightPassenger.Id}");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    
                    var result_dto = JsonSerializer.Deserialize<ApiResponseDto<bool>>(
                        responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (result_dto?.Success == true)
                    {
                        MessageBox.Show("Flight passenger booking deleted successfully", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadFlightPassengers();
                    }
                    else
                    {
                        MessageBox.Show(result_dto?.Message ?? "Failed to delete booking", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting booking: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnDelete.Enabled = true;
                    btnDelete.Text = "Delete";
                }
            }
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            if (_selectedFlightPassenger == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to cancel the check-in for {_selectedFlightPassenger.Passenger.FirstName} {_selectedFlightPassenger.Passenger.LastName} on flight {_selectedFlightPassenger.Flight.FlightNumber}?",
                "Confirm Cancel",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    btnCancel.Enabled = false;
                    btnCancel.Text = "Cancelling...";

                    var json = JsonSerializer.Serialize(_currentEmployee.Id);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    
                    var response = await _httpClient.PostAsync($"/api/flightpassenger/{_selectedFlightPassenger.Id}/cancel", content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    
                    var result_dto = JsonSerializer.Deserialize<ApiResponseDto<bool>>(
                        responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (result_dto?.Success == true)
                    {
                        MessageBox.Show("Check-in cancelled successfully", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadFlightPassengers();
                    }
                    else
                    {
                        MessageBox.Show(result_dto?.Message ?? "Failed to cancel check-in", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error cancelling check-in: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnCancel.Enabled = true;
                    btnCancel.Text = "Cancel Check-in";
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadFlightPassengers();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _httpClient?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
