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
    public partial class LoginForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _serverUrl = "https://localhost:7261";
        
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public EmployeeDto? LoggedInEmployee { get; private set; }
        
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool IsLoginSuccessful { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri(_serverUrl) };
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
                btnLogin.Enabled = false;
                btnLogin.Text = "Нэвтрэж байна...";

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
                    LoggedInEmployee = result.Data.Employee;
                    IsLoginSuccessful = true;
                    
                    if (chkSaveCredentials.Checked)
                    {
                        SaveCredentials(employeeCode, password);
                    }
                    
                    MessageBox.Show($"Тавтай морил, {LoggedInEmployee?.FullName ?? "Ажилтан"}!", "Амжилт", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    DialogResult = DialogResult.OK;
                    Close();
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
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Нэвтрэх";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SaveCredentials(string employeeCode, string password)
        {
            try
            {
                // Simple credential saving (in production, use secure storage)
                Properties.Settings.Default.EmployeeCode = employeeCode;
                Properties.Settings.Default.Password = password;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                // Log error but don't show to user
                System.Diagnostics.Debug.WriteLine($"Error saving credentials: {ex.Message}");
            }
        }

        private void LoadSavedCredentials()
        {
            try
            {
                if (!string.IsNullOrEmpty(Properties.Settings.Default.EmployeeCode))
                {
                    txtEmployeeCode.Text = Properties.Settings.Default.EmployeeCode;
                    chkSaveCredentials.Checked = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading credentials: {ex.Message}");
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadSavedCredentials();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _httpClient?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
