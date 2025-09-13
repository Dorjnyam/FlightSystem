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
    public partial class AddBookingForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _serverUrl = "https://localhost:7261";
        private readonly EmployeeDto _currentEmployee;

        public AddBookingForm(EmployeeDto employee)
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri(_serverUrl) };
            _currentEmployee = employee;
            
            InitializeForm();
        }

        private async void InitializeForm()
        {
            // Load flights and passengers
            await LoadFlights();
            await LoadPassengers();
            
            // Set default values
            txtBookingReference.Text = GenerateBookingReference();
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
                    cmbFlight.Items.Clear();
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
                var response = await _httpClient.GetAsync("/api/passenger");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<PassengerDto[]>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true && result.Data != null)
                {
                    cmbPassenger.Items.Clear();
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

        private string GenerateBookingReference()
        {
            var random = new Random();
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var numbers = "0123456789";
            
            var reference = "";
            for (int i = 0; i < 6; i++)
            {
                if (i < 2)
                    reference += letters[random.Next(letters.Length)];
                else
                    reference += numbers[random.Next(numbers.Length)];
            }
            
            return reference;
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                await CreateBooking();
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
                MessageBox.Show("Please enter booking reference", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private async Task CreateBooking()
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
                    BookingReference = txtBookingReference.Text.Trim().ToUpper(),
                    SpecialRequests = txtSpecialRequests.Text.Trim(),
                    BaggageInfo = $"Weight: {txtBaggageWeight.Text}kg, Count: {txtBaggageCount.Text}",
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
                    MessageBox.Show($"Booking {result.Data?.BookingReference} created successfully!", 
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
                btnCreate.Text = "Create";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnGenerateReference_Click(object sender, EventArgs e)
        {
            txtBookingReference.Text = GenerateBookingReference();
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
