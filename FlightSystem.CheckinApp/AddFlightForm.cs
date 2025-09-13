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
using FlightSystem.Core.Enums;

namespace FlightSystem.CheckinApp
{
    public partial class AddFlightForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _serverUrl = "https://localhost:7261";
        private readonly EmployeeDto _currentEmployee;

        public AddFlightForm(EmployeeDto employee)
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri(_serverUrl) };
            _currentEmployee = employee;
            
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set default values
            dtpScheduledDeparture.Value = DateTime.Now.AddHours(2);
            dtpScheduledArrival.Value = DateTime.Now.AddHours(4);
            dtpCheckinOpen.Value = DateTime.Now;
            dtpCheckinClose.Value = DateTime.Now.AddHours(1.5);
            
            // Load aircraft types
            LoadAircraftTypes();
        }

        private async void LoadAircraftTypes()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/aircraft");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<AircraftDto[]>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true && result.Data != null)
                {
                    cmbAircraft.Items.Clear();
                    foreach (var aircraft in result.Data)
                    {
                        cmbAircraft.Items.Add(new AircraftComboBoxItem
                        {
                            Aircraft = aircraft,
                            DisplayText = $"{aircraft.AircraftType} - {aircraft.AircraftCode}"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading aircraft: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                await CreateFlight();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFlightNumber.Text))
            {
                MessageBox.Show("Please enter flight number", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDepartureAirport.Text))
            {
                MessageBox.Show("Please enter departure airport", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtArrivalAirport.Text))
            {
                MessageBox.Show("Please enter arrival airport", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbAircraft.SelectedItem == null)
            {
                MessageBox.Show("Please select an aircraft", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpScheduledArrival.Value <= dtpScheduledDeparture.Value)
            {
                MessageBox.Show("Arrival time must be after departure time", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private async Task CreateFlight()
        {
            try
            {
                btnCreate.Enabled = false;
                btnCreate.Text = "Creating...";

                var aircraftItem = (AircraftComboBoxItem?)cmbAircraft.SelectedItem;

                var createDto = new CreateFlightDto
                {
                    FlightNumber = txtFlightNumber.Text.Trim().ToUpper(),
                    DepartureAirport = txtDepartureAirport.Text.Trim().ToUpper(),
                    ArrivalAirport = txtArrivalAirport.Text.Trim().ToUpper(),
                    ScheduledDeparture = dtpScheduledDeparture.Value,
                    ScheduledArrival = dtpScheduledArrival.Value,
                    AircraftId = aircraftItem!.Aircraft.Id,
                    CheckinOpenTime = dtpCheckinOpen.Value,
                    CheckinCloseTime = dtpCheckinClose.Value,
                    BoardingTime = dtpScheduledDeparture.Value.AddMinutes(-30),
                    GateNumber = txtGateNumber.Text.Trim().ToUpper(),
                    CreatedByEmployeeId = _currentEmployee.Id
                };

                var json = JsonSerializer.Serialize(createDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("/api/flight", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightInfoDto>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true)
                {
                    MessageBox.Show($"Flight {result.Data?.FlightNumber} created successfully!", 
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(result?.Message ?? "Failed to create flight", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating flight: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCreate.Enabled = true;
                btnCreate.Text = "Create Flight";
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

        private class AircraftComboBoxItem
        {
            public AircraftDto Aircraft { get; set; } = new();
            public string DisplayText { get; set; } = string.Empty;
            public override string ToString() => DisplayText;
        }
    }
}
