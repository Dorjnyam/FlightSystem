namespace FlightSystem.CheckinApp
{
    partial class UpdateFlightPassengerForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelMain;
        private GroupBox grpBookingInfo;
        private Label lblFlightInfo;
        private Label lblFlightLabel;
        private Label lblPassengerInfo;
        private Label lblPassengerLabel;
        private Label lblBookingReference;
        private Label lblBookingReferenceLabel;
        private Label lblCheckinStatus;
        private Label lblCheckinStatusLabel;
        private GroupBox grpSpecialRequests;
        private TextBox txtSpecialRequests;
        private Label lblSpecialRequests;
        private GroupBox grpBaggageInfo;
        private TextBox txtBaggageInfo;
        private Label lblBaggageInfo;
        private Button btnUpdate;
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
            panelMain = new Panel();
            btnCancel = new Button();
            btnUpdate = new Button();
            grpBaggageInfo = new GroupBox();
            txtBaggageInfo = new TextBox();
            lblBaggageInfo = new Label();
            grpSpecialRequests = new GroupBox();
            txtSpecialRequests = new TextBox();
            lblSpecialRequests = new Label();
            grpBookingInfo = new GroupBox();
            lblCheckinStatus = new Label();
            lblCheckinStatusLabel = new Label();
            lblBookingReference = new Label();
            lblBookingReferenceLabel = new Label();
            lblPassengerInfo = new Label();
            lblPassengerLabel = new Label();
            lblFlightInfo = new Label();
            lblFlightLabel = new Label();
            panelMain.SuspendLayout();
            grpBaggageInfo.SuspendLayout();
            grpSpecialRequests.SuspendLayout();
            grpBookingInfo.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(btnCancel);
            panelMain.Controls.Add(btnUpdate);
            panelMain.Controls.Add(grpBaggageInfo);
            panelMain.Controls.Add(grpSpecialRequests);
            panelMain.Controls.Add(grpBookingInfo);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(600, 540);
            panelMain.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(158, 158, 158);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(470, 480);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 40);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(255, 152, 0);
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(350, 480);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(100, 40);
            btnUpdate.TabIndex = 3;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // grpBaggageInfo
            // 
            grpBaggageInfo.Controls.Add(txtBaggageInfo);
            grpBaggageInfo.Controls.Add(lblBaggageInfo);
            grpBaggageInfo.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            grpBaggageInfo.Location = new Point(20, 340);
            grpBaggageInfo.Name = "grpBaggageInfo";
            grpBaggageInfo.Size = new Size(560, 120);
            grpBaggageInfo.TabIndex = 2;
            grpBaggageInfo.TabStop = false;
            grpBaggageInfo.Text = "Baggage Information (Editable)";
            // 
            // txtBaggageInfo
            // 
            txtBaggageInfo.Font = new Font("Microsoft Sans Serif", 10F);
            txtBaggageInfo.Location = new Point(20, 55);
            txtBaggageInfo.Multiline = true;
            txtBaggageInfo.Name = "txtBaggageInfo";
            txtBaggageInfo.ScrollBars = ScrollBars.Vertical;
            txtBaggageInfo.Size = new Size(520, 50);
            txtBaggageInfo.TabIndex = 1;
            // 
            // lblBaggageInfo
            // 
            lblBaggageInfo.AutoSize = true;
            lblBaggageInfo.Font = new Font("Microsoft Sans Serif", 10F);
            lblBaggageInfo.Location = new Point(20, 30);
            lblBaggageInfo.Name = "lblBaggageInfo";
            lblBaggageInfo.Size = new Size(96, 17);
            lblBaggageInfo.TabIndex = 0;
            lblBaggageInfo.Text = "Baggage Info:";
            // 
            // grpSpecialRequests
            // 
            grpSpecialRequests.Controls.Add(txtSpecialRequests);
            grpSpecialRequests.Controls.Add(lblSpecialRequests);
            grpSpecialRequests.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            grpSpecialRequests.Location = new Point(20, 200);
            grpSpecialRequests.Name = "grpSpecialRequests";
            grpSpecialRequests.Size = new Size(560, 120);
            grpSpecialRequests.TabIndex = 1;
            grpSpecialRequests.TabStop = false;
            grpSpecialRequests.Text = "Special Requests (Editable)";
            // 
            // txtSpecialRequests
            // 
            txtSpecialRequests.Font = new Font("Microsoft Sans Serif", 10F);
            txtSpecialRequests.Location = new Point(20, 55);
            txtSpecialRequests.Multiline = true;
            txtSpecialRequests.Name = "txtSpecialRequests";
            txtSpecialRequests.ScrollBars = ScrollBars.Vertical;
            txtSpecialRequests.Size = new Size(520, 50);
            txtSpecialRequests.TabIndex = 1;
            // 
            // lblSpecialRequests
            // 
            lblSpecialRequests.AutoSize = true;
            lblSpecialRequests.Font = new Font("Microsoft Sans Serif", 10F);
            lblSpecialRequests.Location = new Point(20, 30);
            lblSpecialRequests.Name = "lblSpecialRequests";
            lblSpecialRequests.Size = new Size(122, 17);
            lblSpecialRequests.TabIndex = 0;
            lblSpecialRequests.Text = "Special Requests:";
            // 
            // grpBookingInfo
            // 
            grpBookingInfo.Controls.Add(lblCheckinStatus);
            grpBookingInfo.Controls.Add(lblCheckinStatusLabel);
            grpBookingInfo.Controls.Add(lblBookingReference);
            grpBookingInfo.Controls.Add(lblBookingReferenceLabel);
            grpBookingInfo.Controls.Add(lblPassengerInfo);
            grpBookingInfo.Controls.Add(lblPassengerLabel);
            grpBookingInfo.Controls.Add(lblFlightInfo);
            grpBookingInfo.Controls.Add(lblFlightLabel);
            grpBookingInfo.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            grpBookingInfo.Location = new Point(20, 20);
            grpBookingInfo.Name = "grpBookingInfo";
            grpBookingInfo.Size = new Size(560, 160);
            grpBookingInfo.TabIndex = 0;
            grpBookingInfo.TabStop = false;
            grpBookingInfo.Text = "Booking Information (Read Only)";
            // 
            // lblCheckinStatus
            // 
            lblCheckinStatus.AutoSize = true;
            lblCheckinStatus.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblCheckinStatus.Location = new Point(130, 105);
            lblCheckinStatus.Name = "lblCheckinStatus";
            lblCheckinStatus.Size = new Size(118, 17);
            lblCheckinStatus.TabIndex = 7;
            lblCheckinStatus.Text = "Not Checked In";
            // 
            // lblCheckinStatusLabel
            // 
            lblCheckinStatusLabel.AutoSize = true;
            lblCheckinStatusLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblCheckinStatusLabel.Location = new Point(20, 105);
            lblCheckinStatusLabel.Name = "lblCheckinStatusLabel";
            lblCheckinStatusLabel.Size = new Size(127, 17);
            lblCheckinStatusLabel.TabIndex = 6;
            lblCheckinStatusLabel.Text = "Check-in Status:";
            // 
            // lblBookingReference
            // 
            lblBookingReference.AutoSize = true;
            lblBookingReference.Font = new Font("Microsoft Sans Serif", 10F);
            lblBookingReference.Location = new Point(150, 80);
            lblBookingReference.Name = "lblBookingReference";
            lblBookingReference.Size = new Size(83, 17);
            lblBookingReference.TabIndex = 5;
            lblBookingReference.Text = "ABC123456";
            // 
            // lblBookingReferenceLabel
            // 
            lblBookingReferenceLabel.AutoSize = true;
            lblBookingReferenceLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblBookingReferenceLabel.Location = new Point(20, 80);
            lblBookingReferenceLabel.Name = "lblBookingReferenceLabel";
            lblBookingReferenceLabel.Size = new Size(151, 17);
            lblBookingReferenceLabel.TabIndex = 4;
            lblBookingReferenceLabel.Text = "Booking Reference:";
            // 
            // lblPassengerInfo
            // 
            lblPassengerInfo.AutoSize = true;
            lblPassengerInfo.Font = new Font("Microsoft Sans Serif", 10F);
            lblPassengerInfo.Location = new Point(110, 55);
            lblPassengerInfo.Name = "lblPassengerInfo";
            lblPassengerInfo.Size = new Size(187, 17);
            lblPassengerInfo.TabIndex = 3;
            lblPassengerInfo.Text = "Passenger Name (Passport)";
            // 
            // lblPassengerLabel
            // 
            lblPassengerLabel.AutoSize = true;
            lblPassengerLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblPassengerLabel.Location = new Point(20, 55);
            lblPassengerLabel.Name = "lblPassengerLabel";
            lblPassengerLabel.Size = new Size(90, 17);
            lblPassengerLabel.TabIndex = 2;
            lblPassengerLabel.Text = "Passenger:";
            // 
            // lblFlightInfo
            // 
            lblFlightInfo.AutoSize = true;
            lblFlightInfo.Font = new Font("Microsoft Sans Serif", 10F);
            lblFlightInfo.Location = new Point(80, 30);
            lblFlightInfo.Name = "lblFlightInfo";
            lblFlightInfo.Size = new Size(147, 17);
            lblFlightInfo.TabIndex = 1;
            lblFlightInfo.Text = "Flight Number - Route";
            // 
            // lblFlightLabel
            // 
            lblFlightLabel.AutoSize = true;
            lblFlightLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblFlightLabel.Location = new Point(20, 30);
            lblFlightLabel.Name = "lblFlightLabel";
            lblFlightLabel.Size = new Size(53, 17);
            lblFlightLabel.TabIndex = 0;
            lblFlightLabel.Text = "Flight:";
            // 
            // UpdateFlightPassengerForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 540);
            Controls.Add(panelMain);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "UpdateFlightPassengerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Update Flight Passenger Booking";
            panelMain.ResumeLayout(false);
            grpBaggageInfo.ResumeLayout(false);
            grpBaggageInfo.PerformLayout();
            grpSpecialRequests.ResumeLayout(false);
            grpSpecialRequests.PerformLayout();
            grpBookingInfo.ResumeLayout(false);
            grpBookingInfo.PerformLayout();
            ResumeLayout(false);
        }
    }
}
