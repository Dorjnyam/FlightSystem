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
    public partial class CreateFlightPassengerForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _serverUrl = "https://localhost:7261";
        private readonly EmployeeDto _currentEmployee;

        public CreateFlightPassengerForm(EmployeeDto employee)
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri(_serverUrl) };
            _currentEmployee = employee;
            
            InitializeForm();
        }

        private async void InitializeForm()
        {
            await LoadFlights();
            await LoadPassengers();
        }

        private async Task LoadFlights()
        {
            try
            {
                cmbFlight.Items.Clear();
                var response = await _httpClient.GetAsync("/api/flight/active");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightInfoDto[]>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true && result.Data != null)
                {
                    foreach (var flight in result.Data.OrderBy(f => f.ScheduledDeparture))
                    {
                        cmbFlight.Items.Add(new FlightComboBoxItem
                        {
                            Flight = flight,
                            DisplayText = $"{flight.FlightNumber} - {flight.DepartureAirport} → {flight.ArrivalAirport} ({flight.ScheduledDeparture:yyyy-MM-dd HH:mm})"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading flights: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadPassengers()
        {
            try
            {
                cmbPassenger.Items.Clear();
                var response = await _httpClient.GetAsync("/api/passenger");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<PassengerDto[]>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true && result.Data != null)
                {
                    foreach (var passenger in result.Data.OrderBy(p => p.LastName))
                    {
                        cmbPassenger.Items.Add(new PassengerComboBoxItem
                        {
                            Passenger = passenger,
                            DisplayText = $"{passenger.FirstName} {passenger.LastName} ({passenger.PassportNumber})"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading passengers: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                CreateFlightPassenger();
            }
        }

        private bool ValidateInput()
        {
            if (cmbFlight.SelectedItem == null)
            {
                MessageBox.Show("Please select a flight", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbPassenger.SelectedItem == null)
            {
                MessageBox.Show("Please select a passenger", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBookingReference.Text))
            {
                MessageBox.Show("Please enter a booking reference", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private async void CreateFlightPassenger()
        {
            try
            {
                btnCreate.Enabled = false;
                btnCreate.Text = "Creating...";

                var flightItem = (FlightComboBoxItem?)cmbFlight.SelectedItem;
                var passengerItem = (PassengerComboBoxItem?)cmbPassenger.SelectedItem;

                var createDto = new CreateFlightPassengerDto
                {
                    FlightId = flightItem!.Flight.Id,
                    PassengerId = passengerItem!.Passenger.Id,
                    BookingReference = txtBookingReference.Text.Trim(),
                    SpecialRequests = string.IsNullOrWhiteSpace(txtSpecialRequests.Text) ? null : txtSpecialRequests.Text.Trim(),
                    BaggageInfo = string.IsNullOrWhiteSpace(txtBaggageInfo.Text) ? null : txtBaggageInfo.Text.Trim(),
                    CreatedByEmployeeId = _currentEmployee.Id
                };

                var json = JsonSerializer.Serialize(createDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("/api/flightpassenger", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightPassengerDto>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true)
                {
                    MessageBox.Show($"Flight passenger booking created successfully!\nBooking Reference: {result.Data?.BookingReference}", 
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(result?.Message ?? "Failed to create booking", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating booking: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCreate.Enabled = true;
                btnCreate.Text = "Create Booking";
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

        private class FlightComboBoxItem
        {
            public FlightInfoDto Flight { get; set; } = new();
            public string DisplayText { get; set; } = string.Empty;
            public override string ToString() => DisplayText;
        }

        private class PassengerComboBoxItem
        {
            public PassengerDto Passenger { get; set; } = new();
            public string DisplayText { get; set; } = string.Empty;
            public override string ToString() => DisplayText;
        }
    }
}
