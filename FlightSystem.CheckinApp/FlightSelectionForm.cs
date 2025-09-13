using System;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Shared.DTOs.Common;

namespace FlightSystem.CheckinApp
{
    public partial class FlightSelectionForm : Form
    {
        private readonly HttpClient _httpClient;
        private readonly string _serverUrl = "https://localhost:7261";
        
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public FlightInfoDto? SelectedFlight { get; private set; }

        public FlightSelectionForm()
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri(_serverUrl) };
            LoadFlights();
        }

        private async void LoadFlights()
        {
            try
            {
                btnRefresh.Enabled = false;
                btnRefresh.Text = "Ачаалж байна...";
                lstFlights.Items.Clear();

                var response = await _httpClient.GetAsync("/api/flight/active");
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var result = JsonSerializer.Deserialize<ApiResponseDto<FlightInfoDto[]>>(
                    responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true && result.Data != null)
                {
                    foreach (var flight in result.Data)
                    {
                        var item = new ListViewItem(flight.FlightNumber);
                        item.SubItems.Add(flight.DepartureAirport);
                        item.SubItems.Add(flight.ArrivalAirport);
                        item.SubItems.Add(flight.ScheduledDeparture.ToString("HH:mm"));
                        item.SubItems.Add(flight.Status);
                        item.SubItems.Add(flight.GateNumber ?? "Тодорхойгүй");
                        item.Tag = flight;
                        lstFlights.Items.Add(item);
                    }
                    
                    lblStatus.Text = $"{result.Data.Length} нислэг олдлоо";
                    lblStatus.ForeColor = Color.Green;
                }
                else
                {
                    lblStatus.Text = "Нислэг олдсонгүй";
                    lblStatus.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Алдаа: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show($"Нислэгүүд ачаалахад алдаа гарлаа: {ex.Message}", "Алдаа", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRefresh.Enabled = true;
                btnRefresh.Text = "Шинэчлэх";
            }
        }

        private void lstFlights_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFlights.SelectedItems.Count > 0)
            {
                SelectedFlight = lstFlights.SelectedItems[0].Tag as FlightInfoDto;
                UpdateFlightDetails();
                btnSelect.Enabled = true;
            }
            else
            {
                SelectedFlight = null;
                btnSelect.Enabled = false;
            }
        }

        private void UpdateFlightDetails()
        {
            if (SelectedFlight == null)
            {
                txtFlightDetails.Clear();
                return;
            }

            var details = $"Нислэгийн дугаар: {SelectedFlight.FlightNumber}\n" +
                         $"Чиглэл: {SelectedFlight.DepartureAirport} → {SelectedFlight.ArrivalAirport}\n" +
                         $"Хөөрөх цаг: {SelectedFlight.ScheduledDeparture:yyyy-MM-dd HH:mm}\n" +
                         $"Ирэх цаг: {SelectedFlight.ScheduledArrival:yyyy-MM-dd HH:mm}\n" +
                         $"Төлөв: {SelectedFlight.Status}\n" +
                         $"Хаалга: {SelectedFlight.GateNumber ?? "Тодорхойгүй"}\n" +
                         $"Онгоц: {SelectedFlight.AircraftType}\n" +
                         $"Бүртгэлийн эхлэх цаг: {SelectedFlight.CheckinOpenTime:HH:mm}\n" +
                         $"Бүртгэлийн дуусах цаг: {SelectedFlight.CheckinCloseTime:HH:mm}";

            txtFlightDetails.Text = details;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (SelectedFlight != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadFlights();
        }

        private void lstFlights_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedFlight != null)
            {
                btnSelect_Click(sender, e);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _httpClient?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
