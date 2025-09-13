using System;
using System.Drawing;
using System.Windows.Forms;
using FlightSystem.Shared.DTOs.Response;

namespace FlightSystem.CheckinApp
{
    public partial class OptionsForm : Form
    {
        private readonly EmployeeDto _currentEmployee;

        public OptionsForm(EmployeeDto employee)
        {
            InitializeComponent();
            _currentEmployee = employee;
            InitializeForm();
        }

        private void InitializeForm()
        {
            lblWelcome.Text = $"Welcome, {_currentEmployee.FullName}";
            lblRole.Text = $"Role: {_currentEmployee.Role}";
        }

        private void btnCheckin_Click(object sender, EventArgs e)
        {
            var flightSelectionForm = new FlightSelectionForm();
            if (flightSelectionForm.ShowDialog() == DialogResult.OK && flightSelectionForm.SelectedFlight != null)
            {
                var checkinForm = new CheckinMainForm(_currentEmployee, flightSelectionForm.SelectedFlight);
                checkinForm.ShowDialog();
            }
        }

        private void btnFlightPassengerManagement_Click(object sender, EventArgs e)
        {
            var managementForm = new FlightPassengerManagementForm(_currentEmployee);
            managementForm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
