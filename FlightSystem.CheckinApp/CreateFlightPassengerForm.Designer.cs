namespace FlightSystem.CheckinApp
{
    partial class CreateFlightPassengerForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelMain;
        private GroupBox grpFlightSelection;
        private ComboBox cmbFlight;
        private Label lblFlight;
        private GroupBox grpPassengerSelection;
        private ComboBox cmbPassenger;
        private Label lblPassenger;
        private GroupBox grpBookingDetails;
        private TextBox txtBookingReference;
        private Label lblBookingReference;
        private GroupBox grpSpecialRequests;
        private TextBox txtSpecialRequests;
        private Label lblSpecialRequests;
        private GroupBox grpBaggageInfo;
        private TextBox txtBaggageInfo;
        private Label lblBaggageInfo;
        private Button btnCreate;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelMain = new Panel();
            this.btnCancel = new Button();
            this.btnCreate = new Button();
            this.grpBaggageInfo = new GroupBox();
            this.txtBaggageInfo = new TextBox();
            this.lblBaggageInfo = new Label();
            this.grpSpecialRequests = new GroupBox();
            this.txtSpecialRequests = new TextBox();
            this.lblSpecialRequests = new Label();
            this.grpBookingDetails = new GroupBox();
            this.txtBookingReference = new TextBox();
            this.lblBookingReference = new Label();
            this.grpPassengerSelection = new GroupBox();
            this.cmbPassenger = new ComboBox();
            this.lblPassenger = new Label();
            this.grpFlightSelection = new GroupBox();
            this.cmbFlight = new ComboBox();
            this.lblFlight = new Label();
            
            this.panelMain.SuspendLayout();
            this.grpBaggageInfo.SuspendLayout();
            this.grpSpecialRequests.SuspendLayout();
            this.grpBookingDetails.SuspendLayout();
            this.grpPassengerSelection.SuspendLayout();
            this.grpFlightSelection.SuspendLayout();
            this.SuspendLayout();
            
            // panelMain
            this.panelMain.Controls.Add(this.btnCancel);
            this.panelMain.Controls.Add(this.btnCreate);
            this.panelMain.Controls.Add(this.grpBaggageInfo);
            this.panelMain.Controls.Add(this.grpSpecialRequests);
            this.panelMain.Controls.Add(this.grpBookingDetails);
            this.panelMain.Controls.Add(this.grpPassengerSelection);
            this.panelMain.Controls.Add(this.grpFlightSelection);
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Location = new Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new Size(600, 700);
            this.panelMain.TabIndex = 0;
            
            // grpFlightSelection
            this.grpFlightSelection.Controls.Add(this.cmbFlight);
            this.grpFlightSelection.Controls.Add(this.lblFlight);
            this.grpFlightSelection.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.grpFlightSelection.Location = new Point(20, 20);
            this.grpFlightSelection.Name = "grpFlightSelection";
            this.grpFlightSelection.Size = new Size(560, 80);
            this.grpFlightSelection.TabIndex = 0;
            this.grpFlightSelection.TabStop = false;
            this.grpFlightSelection.Text = "Flight Selection";
            
            // lblFlight
            this.lblFlight.AutoSize = true;
            this.lblFlight.Font = new Font("Microsoft Sans Serif", 10F);
            this.lblFlight.Location = new Point(20, 35);
            this.lblFlight.Name = "lblFlight";
            this.lblFlight.Size = new Size(40, 17);
            this.lblFlight.TabIndex = 0;
            this.lblFlight.Text = "Flight:";
            
            // cmbFlight
            this.cmbFlight.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFlight.Font = new Font("Microsoft Sans Serif", 10F);
            this.cmbFlight.FormattingEnabled = true;
            this.cmbFlight.Location = new Point(100, 32);
            this.cmbFlight.Name = "cmbFlight";
            this.cmbFlight.Size = new Size(440, 24);
            this.cmbFlight.TabIndex = 1;
            
            // grpPassengerSelection
            this.grpPassengerSelection.Controls.Add(this.cmbPassenger);
            this.grpPassengerSelection.Controls.Add(this.lblPassenger);
            this.grpPassengerSelection.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.grpPassengerSelection.Location = new Point(20, 120);
            this.grpPassengerSelection.Name = "grpPassengerSelection";
            this.grpPassengerSelection.Size = new Size(560, 80);
            this.grpPassengerSelection.TabIndex = 1;
            this.grpPassengerSelection.TabStop = false;
            this.grpPassengerSelection.Text = "Passenger Selection";
            
            // lblPassenger
            this.lblPassenger.AutoSize = true;
            this.lblPassenger.Font = new Font("Microsoft Sans Serif", 10F);
            this.lblPassenger.Location = new Point(20, 35);
            this.lblPassenger.Name = "lblPassenger";
            this.lblPassenger.Size = new Size(74, 17);
            this.lblPassenger.TabIndex = 0;
            this.lblPassenger.Text = "Passenger:";
            
            // cmbPassenger
            this.cmbPassenger.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPassenger.Font = new Font("Microsoft Sans Serif", 10F);
            this.cmbPassenger.FormattingEnabled = true;
            this.cmbPassenger.Location = new Point(100, 32);
            this.cmbPassenger.Name = "cmbPassenger";
            this.cmbPassenger.Size = new Size(440, 24);
            this.cmbPassenger.TabIndex = 1;
            
            // grpBookingDetails
            this.grpBookingDetails.Controls.Add(this.txtBookingReference);
            this.grpBookingDetails.Controls.Add(this.lblBookingReference);
            this.grpBookingDetails.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.grpBookingDetails.Location = new Point(20, 220);
            this.grpBookingDetails.Name = "grpBookingDetails";
            this.grpBookingDetails.Size = new Size(560, 80);
            this.grpBookingDetails.TabIndex = 2;
            this.grpBookingDetails.TabStop = false;
            this.grpBookingDetails.Text = "Booking Details";
            
            // lblBookingReference
            this.lblBookingReference.AutoSize = true;
            this.lblBookingReference.Font = new Font("Microsoft Sans Serif", 10F);
            this.lblBookingReference.Location = new Point(20, 35);
            this.lblBookingReference.Name = "lblBookingReference";
            this.lblBookingReference.Size = new Size(121, 17);
            this.lblBookingReference.TabIndex = 0;
            this.lblBookingReference.Text = "Booking Reference:";
            
            // txtBookingReference
            this.txtBookingReference.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtBookingReference.Location = new Point(150, 32);
            this.txtBookingReference.Name = "txtBookingReference";
            this.txtBookingReference.Size = new Size(390, 23);
            this.txtBookingReference.TabIndex = 1;
            
            // grpSpecialRequests
            this.grpSpecialRequests.Controls.Add(this.txtSpecialRequests);
            this.grpSpecialRequests.Controls.Add(this.lblSpecialRequests);
            this.grpSpecialRequests.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.grpSpecialRequests.Location = new Point(20, 320);
            this.grpSpecialRequests.Name = "grpSpecialRequests";
            this.grpSpecialRequests.Size = new Size(560, 120);
            this.grpSpecialRequests.TabIndex = 3;
            this.grpSpecialRequests.TabStop = false;
            this.grpSpecialRequests.Text = "Special Requests";
            
            // lblSpecialRequests
            this.lblSpecialRequests.AutoSize = true;
            this.lblSpecialRequests.Font = new Font("Microsoft Sans Serif", 10F);
            this.lblSpecialRequests.Location = new Point(20, 30);
            this.lblSpecialRequests.Name = "lblSpecialRequests";
            this.lblSpecialRequests.Size = new Size(119, 17);
            this.lblSpecialRequests.TabIndex = 0;
            this.lblSpecialRequests.Text = "Special Requests:";
            
            // txtSpecialRequests
            this.txtSpecialRequests.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtSpecialRequests.Location = new Point(20, 55);
            this.txtSpecialRequests.Multiline = true;
            this.txtSpecialRequests.Name = "txtSpecialRequests";
            this.txtSpecialRequests.ScrollBars = ScrollBars.Vertical;
            this.txtSpecialRequests.Size = new Size(520, 50);
            this.txtSpecialRequests.TabIndex = 1;
            
            // grpBaggageInfo
            this.grpBaggageInfo.Controls.Add(this.txtBaggageInfo);
            this.grpBaggageInfo.Controls.Add(this.lblBaggageInfo);
            this.grpBaggageInfo.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.grpBaggageInfo.Location = new Point(20, 460);
            this.grpBaggageInfo.Name = "grpBaggageInfo";
            this.grpBaggageInfo.Size = new Size(560, 120);
            this.grpBaggageInfo.TabIndex = 4;
            this.grpBaggageInfo.TabStop = false;
            this.grpBaggageInfo.Text = "Baggage Information";
            
            // lblBaggageInfo
            this.lblBaggageInfo.AutoSize = true;
            this.lblBaggageInfo.Font = new Font("Microsoft Sans Serif", 10F);
            this.lblBaggageInfo.Location = new Point(20, 30);
            this.lblBaggageInfo.Name = "lblBaggageInfo";
            this.lblBaggageInfo.Size = new Size(110, 17);
            this.lblBaggageInfo.TabIndex = 0;
            this.lblBaggageInfo.Text = "Baggage Info:";
            
            // txtBaggageInfo
            this.txtBaggageInfo.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtBaggageInfo.Location = new Point(20, 55);
            this.txtBaggageInfo.Multiline = true;
            this.txtBaggageInfo.Name = "txtBaggageInfo";
            this.txtBaggageInfo.ScrollBars = ScrollBars.Vertical;
            this.txtBaggageInfo.Size = new Size(520, 50);
            this.txtBaggageInfo.TabIndex = 1;
            
            // btnCreate
            this.btnCreate.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnCreate.FlatAppearance.BorderSize = 0;
            this.btnCreate.FlatStyle = FlatStyle.Flat;
            this.btnCreate.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Location = new Point(350, 600);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new Size(100, 40);
            this.btnCreate.TabIndex = 5;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new EventHandler(this.btnCreate_Click);
            
            // btnCancel
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new Point(470, 600);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(100, 40);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            
            // CreateFlightPassengerForm
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(600, 700);
            this.Controls.Add(this.panelMain);
            this.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Name = "CreateFlightPassengerForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Create Flight Passenger Booking";
            
            this.panelMain.ResumeLayout(false);
            this.grpBaggageInfo.ResumeLayout(false);
            this.grpBaggageInfo.PerformLayout();
            this.grpSpecialRequests.ResumeLayout(false);
            this.grpSpecialRequests.PerformLayout();
            this.grpBookingDetails.ResumeLayout(false);
            this.grpBookingDetails.PerformLayout();
            this.grpPassengerSelection.ResumeLayout(false);
            this.grpPassengerSelection.PerformLayout();
            this.grpFlightSelection.ResumeLayout(false);
            this.grpFlightSelection.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
