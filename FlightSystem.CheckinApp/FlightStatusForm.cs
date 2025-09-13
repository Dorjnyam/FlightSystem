using System;
using System.Windows.Forms;
using FlightSystem.Core.Enums;
using FlightSystem.Shared.DTOs.Response;

namespace FlightSystem.CheckinApp
{
    public partial class FlightStatusForm : Form
    {
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public FlightStatus SelectedStatus { get; private set; }
        private readonly FlightInfoDto _flight;

        public FlightStatusForm(FlightInfoDto flight)
        {
            InitializeComponent();
            _flight = flight;
            InitializeForm();
        }

        private void InitializeForm()
        {
            lblCurrentFlight.Text = $"Нислэг: {_flight.FlightNumber} ({_flight.DepartureAirport} → {_flight.ArrivalAirport})";
            lblCurrentStatus.Text = $"Одоогийн төлөв: {_flight.Status}";

            // Боломжтой төлвүүдийг нэмэх
            cmbNewStatus.Items.Clear();
            
            // Одоогийн төлвөөс шилжих боломжтой төлвүүдийг нэмэх
            var currentStatus = GetFlightStatusFromString(_flight.Status);
            var possibleTransitions = GetPossibleTransitions(currentStatus);
            
            foreach (var status in possibleTransitions)
            {
                cmbNewStatus.Items.Add(new StatusItem(status, GetStatusDisplayName(status)));
            }
            
            cmbNewStatus.DisplayMember = "DisplayName";
            cmbNewStatus.ValueMember = "Status";
        }

        private FlightStatus GetFlightStatusFromString(string statusString)
        {
            return statusString switch
            {
                "Төлөвлөсөн" => FlightStatus.Scheduled,
                "Бүртгэж байна" => FlightStatus.CheckinOpen,
                "Бүртгэл хаагдсан" => FlightStatus.CheckinClosed,
                "Онгоцонд сууж байна" => FlightStatus.Boarding,
                "Сүүлчийн дуудлага" => FlightStatus.LastCall,
                "Хаалга хаагдсан" => FlightStatus.GateClosed,
                "Ниссэн" => FlightStatus.Departed,
                "Хойшилсон" => FlightStatus.Delayed,
                "Цуцалсан" => FlightStatus.Cancelled,
                _ => FlightStatus.Scheduled
            };
        }

        private FlightStatus[] GetPossibleTransitions(FlightStatus currentStatus)
        {
            return currentStatus switch
            {
                FlightStatus.Scheduled => new[] { FlightStatus.CheckinOpen, FlightStatus.Delayed, FlightStatus.Cancelled },
                FlightStatus.CheckinOpen => new[] { FlightStatus.CheckinClosed, FlightStatus.Delayed, FlightStatus.Cancelled },
                FlightStatus.CheckinClosed => new[] { FlightStatus.Boarding, FlightStatus.Delayed },
                FlightStatus.Boarding => new[] { FlightStatus.LastCall, FlightStatus.Delayed },
                FlightStatus.LastCall => new[] { FlightStatus.GateClosed, FlightStatus.Delayed },
                FlightStatus.GateClosed => new[] { FlightStatus.Departed },
                FlightStatus.Departed => Array.Empty<FlightStatus>(),
                FlightStatus.Delayed => new[] { FlightStatus.CheckinOpen, FlightStatus.CheckinClosed, FlightStatus.Boarding, FlightStatus.Cancelled },
                FlightStatus.Cancelled => Array.Empty<FlightStatus>(),
                _ => Array.Empty<FlightStatus>()
            };
        }

        private string GetStatusDisplayName(FlightStatus status)
        {
            return status switch
            {
                FlightStatus.Scheduled => "Төлөвлөсөн",
                FlightStatus.CheckinOpen => "Бүртгэж байна",
                FlightStatus.CheckinClosed => "Бүртгэл хаагдсан",
                FlightStatus.Boarding => "Онгоцонд сууж байна",
                FlightStatus.LastCall => "Сүүлчийн дуудлага",
                FlightStatus.GateClosed => "Хаалга хаагдсан",
                FlightStatus.Departed => "Ниссэн",
                FlightStatus.Delayed => "Хойшилсон",
                FlightStatus.Cancelled => "Цуцалсан",
                _ => status.ToString()
            };
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbNewStatus.SelectedItem is StatusItem selectedItem)
            {
                SelectedStatus = selectedItem.Status;
                
                var result = MessageBox.Show(
                    $"Нислэгийн төлвийг '{selectedItem.DisplayName}' болгон өөрчлөх үү?",
                    "Баталгаажуулах",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Шинэ төлөв сонгоно уу", "Алдаа", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    public class StatusItem
    {
        public FlightStatus Status { get; set; }
        public string DisplayName { get; set; }

        public StatusItem(FlightStatus status, string displayName)
        {
            Status = status;
            DisplayName = displayName;
        }
    }
}
