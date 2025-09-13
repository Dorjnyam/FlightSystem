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
    public partial class AddPassengerForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _serverUrl = "https://localhost:7261";
        private readonly EmployeeDto _currentEmployee;

        public AddPassengerForm(EmployeeDto employee)
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri(_serverUrl) };
            _currentEmployee = employee;
            
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set default values
            dtpDateOfBirth.Value = DateTime.Now.AddYears(-30);
            
            // Load passenger types
            LoadPassengerTypes();
        }

        private void LoadPassengerTypes()
        {
            cmbPassengerType.Items.Clear();
            foreach (PassengerType type in Enum.GetValues(typeof(PassengerType)))
            {
                cmbPassengerType.Items.Add(type);
            }
            cmbPassengerType.SelectedItem = PassengerType.Adult;
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                await CreatePassenger();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter first name", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter last name", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassportNumber.Text))
            {
                MessageBox.Show("Please enter passport number", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNationality.Text))
            {
                MessageBox.Show("Please enter nationality", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbPassengerType.SelectedItem == null)
            {
                MessageBox.Show("Please select passenger type", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private async Task CreatePassenger()
        {
            try
            {
                btnCreate.Enabled = false;
                btnCreate.Text = "Creating...";

                var createDto = new CreatePassengerDto
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    PassportNumber = txtPassportNumber.Text.Trim().ToUpper(),
                    Nationality = txtNationality.Text.Trim(),
                    DateOfBirth = dtpDateOfBirth.Value.Date,
                    Type = (PassengerType)cmbPassengerType.SelectedItem!,
                    Phone = txtPhoneNumber.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };

                var json = JsonSerializer.Serialize(createDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("/api/passenger", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<PassengerDto>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true)
                {
                    MessageBox.Show($"Passenger {result.Data?.FirstName} {result.Data?.LastName} created successfully!", 
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(result?.Message ?? "Failed to create passenger", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating passenger: {ex.Message}", "Error", 
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _httpClient?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
