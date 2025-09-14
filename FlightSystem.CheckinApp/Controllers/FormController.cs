using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightSystem.Shared.DTOs.Common;
using FlightSystem.Shared.DTOs.Response;
using FlightSystem.Core.Enums;

namespace FlightSystem.CheckinApp.Controllers
{
    /// <summary>
    /// Centralized form controller to manage all form operations
    /// </summary>
    public class FormController
    {
        private readonly EmployeeDto _currentEmployee;
        private Form? _currentMainForm;

        public FormController(EmployeeDto employee)
        {
            _currentEmployee = employee ?? throw new ArgumentNullException(nameof(employee));
        }

        public void SetMainForm(Form mainForm)
        {
            _currentMainForm = mainForm;
        }

        #region Flight Management

        /// <summary>
        /// Show Add Flight dialog
        /// </summary>
        /// <returns>DialogResult indicating success/failure</returns>
        public async Task<DialogResult> ShowAddFlightDialogAsync()
        {
            try
            {
                using var addFlightForm = new AddFlightForm(_currentEmployee);
                var result = addFlightForm.ShowDialog(_currentMainForm);
                
                if (result == DialogResult.OK)
                {
                    await NotifyDataChangedAsync("Flight");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening Add Flight dialog: {ex.Message}");
                return DialogResult.Abort;
            }
        }

        /// <summary>
        /// Show Flight Status Change dialog
        /// </summary>
        /// <param name="flight">Flight to change status for</param>
        /// <returns>DialogResult and new status</returns>
        public async Task<(DialogResult Result, string? NewStatus)> ShowFlightStatusDialogAsync(FlightInfoDto flight)
        {
            try
            {
                using var statusForm = new FlightStatusForm(flight);
                var result = statusForm.ShowDialog(_currentMainForm);
                
                if (result == DialogResult.OK)
                {
                    var newStatus = GetFlightStatusString(statusForm.SelectedStatus);
                    await NotifyDataChangedAsync("FlightStatus");
                    return (result, newStatus);
                }
                
                return (result, null);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening Flight Status dialog: {ex.Message}");
                return (DialogResult.Abort, null);
            }
        }

        /// <summary>
        /// Show Flight Selection dialog
        /// </summary>
        /// <returns>DialogResult and selected flight</returns>
        public (DialogResult Result, FlightInfoDto? SelectedFlight) ShowFlightSelectionDialog()
        {
            try
            {
                using var selectionForm = new FlightSelectionForm();
                var result = selectionForm.ShowDialog(_currentMainForm);
                
                return (result, selectionForm.SelectedFlight);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening Flight Selection dialog: {ex.Message}");
                return (DialogResult.Abort, null);
            }
        }

        #endregion

        #region Passenger Management

        /// <summary>
        /// Show Add Passenger dialog
        /// </summary>
        /// <returns>DialogResult indicating success/failure</returns>
        public async Task<DialogResult> ShowAddPassengerDialogAsync()
        {
            try
            {
                using var addPassengerForm = new AddPassengerForm(_currentEmployee);
                var result = addPassengerForm.ShowDialog(_currentMainForm);
                
                if (result == DialogResult.OK)
                {
                    await NotifyDataChangedAsync("Passenger");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening Add Passenger dialog: {ex.Message}");
                return DialogResult.Abort;
            }
        }

        #endregion

        #region Booking Management

        /// <summary>
        /// Show Add Booking dialog
        /// </summary>
        /// <returns>DialogResult indicating success/failure</returns>
        public async Task<DialogResult> ShowAddBookingDialogAsync()
        {
            try
            {
                using var addBookingForm = new AddBookingForm(_currentEmployee);
                var result = addBookingForm.ShowDialog(_currentMainForm);
                
                if (result == DialogResult.OK)
                {
                    await NotifyDataChangedAsync("Booking");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening Add Booking dialog: {ex.Message}");
                return DialogResult.Abort;
            }
        }

        /// <summary>
        /// Show FlightPassenger Management dialog
        /// </summary>
        /// <returns>DialogResult indicating success/failure</returns>
        public async Task<DialogResult> ShowFlightPassengerManagementDialogAsync()
        {
            try
            {
                using var managementForm = new FlightPassengerManagementForm(_currentEmployee);
                var result = managementForm.ShowDialog(_currentMainForm);
                
                if (result == DialogResult.OK)
                {
                    await NotifyDataChangedAsync("FlightPassenger");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening FlightPassenger Management dialog: {ex.Message}");
                return DialogResult.Abort;
            }
        }

        /// <summary>
        /// Show Update FlightPassenger dialog
        /// </summary>
        /// <param name="flightPassenger">FlightPassenger to update</param>
        /// <returns>DialogResult indicating success/failure</returns>
        public async Task<DialogResult> ShowUpdateFlightPassengerDialogAsync(FlightPassengerDto flightPassenger)
        {
            try
            {
                using var updateForm = new UpdateFlightPassengerForm(flightPassenger, _currentEmployee);
                var result = updateForm.ShowDialog(_currentMainForm);
                
                if (result == DialogResult.OK)
                {
                    await NotifyDataChangedAsync("FlightPassenger");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening Update FlightPassenger dialog: {ex.Message}");
                return DialogResult.Abort;
            }
        }

        #endregion

        #region Check-in Management

        /// <summary>
        /// Show Check-in Main Form for selected flight
        /// </summary>
        /// <param name="flight">Flight for check-in</param>
        /// <returns>DialogResult indicating success/failure</returns>
        public DialogResult ShowCheckinMainForm(FlightInfoDto flight)
        {
            try
            {
                using var checkinForm = new CheckinMainForm(_currentEmployee, flight);
                return checkinForm.ShowDialog(_currentMainForm);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening Check-in form: {ex.Message}");
                return DialogResult.Abort;
            }
        }

        #endregion

        #region Options and Navigation

        /// <summary>
        /// Show Options form (alternative navigation)
        /// </summary>
        /// <returns>DialogResult indicating user choice</returns>
        public DialogResult ShowOptionsForm()
        {
            try
            {
                using var optionsForm = new OptionsForm(_currentEmployee);
                return optionsForm.ShowDialog(_currentMainForm);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening Options form: {ex.Message}");
                return DialogResult.Abort;
            }
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Show error message to user
        /// </summary>
        /// <param name="message">Error message</param>
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Show success message to user
        /// </summary>
        /// <param name="message">Success message</param>
        public void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Show confirmation dialog
        /// </summary>
        /// <param name="message">Confirmation message</param>
        /// <param name="title">Dialog title</param>
        /// <returns>DialogResult from user</returns>
        public DialogResult ShowConfirmationDialog(string message, string title = "Confirm")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Convert FlightStatus enum to display string
        /// </summary>
        /// <param name="status">Flight status enum</param>
        /// <returns>Display string</returns>
        private string GetFlightStatusString(FlightStatus status)
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

        /// <summary>
        /// Notify main form of data changes for refresh
        /// </summary>
        /// <param name="dataType">Type of data that changed</param>
        private async Task NotifyDataChangedAsync(string dataType)
        {
            try
            {
                if (_currentMainForm is CheckinMainSystemForm mainForm)
                {
                    // Refresh data based on type
                    switch (dataType.ToLower())
                    {
                        case "flight":
                            await mainForm.RefreshFlightsAsync();
                            break;
                        case "passenger":
                            await mainForm.RefreshPassengersAsync();
                            break;
                        case "booking":
                        case "flightpassenger":
                            await mainForm.RefreshFlightPassengersAsync();
                            break;
                        case "flightstatus":
                            await mainForm.RefreshFlightsAsync();
                            break;
                        default:
                            await mainForm.RefreshAllDataAsync();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error but don't show to user as this is background refresh
                System.Diagnostics.Debug.WriteLine($"Error refreshing data: {ex.Message}");
            }
        }

        #endregion

        #region Validation Methods

        /// <summary>
        /// Validate if employee has permission for operation
        /// </summary>
        /// <param name="operation">Operation to check</param>
        /// <returns>True if permitted</returns>
        public bool ValidatePermission(string operation)
        {
            // Add role-based permission checking here if needed
            // For now, all check-in agents have full permissions
            return !string.IsNullOrEmpty(_currentEmployee.Role);
        }

        /// <summary>
        /// Check if form controller is properly initialized
        /// </summary>
        /// <returns>True if ready</returns>
        public bool IsReady()
        {
            return _currentEmployee != null && !string.IsNullOrEmpty(_currentEmployee.EmployeeCode);
        }

        #endregion

        #region Events

        /// <summary>
        /// Event fired when data changes require UI refresh
        /// </summary>
        public event EventHandler<string>? DataChanged;

        /// <summary>
        /// Fire data changed event
        /// </summary>
        /// <param name="dataType">Type of data that changed</param>
        protected virtual void OnDataChanged(string dataType)
        {
            DataChanged?.Invoke(this, dataType);
        }

        #endregion
    }
}
