namespace FlightSystem.CheckinApp
{
    partial class AddBookingForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelMain;
        private GroupBox grpBookingInfo;
        private TextBox txtBookingReference;
        private Label lblBookingReference;
        private Button btnGenerateReference;
        private ComboBox cmbFlight;
        private Label lblFlight;
        private ComboBox cmbPassenger;
        private Label lblPassenger;
        private GroupBox grpSpecialRequests;
        private TextBox txtSpecialRequests;
        private GroupBox grpBaggageInfo;
        private TextBox txtBaggageWeight;
        private Label lblBaggageWeight;
        private TextBox txtBaggageCount;
        private Label lblBaggageCount;
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
            this.txtBaggageCount = new TextBox();
            this.lblBaggageCount = new Label();
            this.txtBaggageWeight = new TextBox();
            this.lblBaggageWeight = new Label();
            this.grpSpecialRequests = new GroupBox();
            this.txtSpecialRequests = new TextBox();
            this.grpBookingInfo = new GroupBox();
            this.cmbPassenger = new ComboBox();
            this.lblPassenger = new Label();
            this.cmbFlight = new ComboBox();
            this.lblFlight = new Label();
            this.btnGenerateReference = new Button();
            this.txtBookingReference = new TextBox();
            this.lblBookingReference = new Label();
            
            this.panelMain.SuspendLayout();
            this.grpBaggageInfo.SuspendLayout();
            this.grpSpecialRequests.SuspendLayout();
            this.grpBookingInfo.SuspendLayout();
            this.SuspendLayout();
            
            // panelMain
            this.panelMain.Controls.Add(this.btnCancel);
            this.panelMain.Controls.Add(this.btnCreate);
            this.panelMain.Controls.Add(this.grpBaggageInfo);
            this.panelMain.Controls.Add(this.grpSpecialRequests);
            this.panelMain.Controls.Add(this.grpBookingInfo);
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Location = new Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new Size(600, 700);
            this.panelMain.TabIndex = 0;
            
            // grpBookingInfo
            this.grpBookingInfo.Controls.Add(this.cmbPassenger);
            this.grpBookingInfo.Controls.Add(this.lblPassenger);
            this.grpBookingInfo.Controls.Add(this.cmbFlight);
            this.grpBookingInfo.Controls.Add(this.lblFlight);
            this.grpBookingInfo.Controls.Add(this.btnGenerateReference);
            this.grpBookingInfo.Controls.Add(this.txtBookingReference);
            this.grpBookingInfo.Controls.Add(this.lblBookingReference);
            this.grpBookingInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpBookingInfo.Location = new Point(20, 20);
            this.grpBookingInfo.Name = "grpBookingInfo";
            this.grpBookingInfo.Size = new Size(560, 200);
            this.grpBookingInfo.TabIndex = 0;
            this.grpBookingInfo.TabStop = false;
            this.grpBookingInfo.Text = "Booking Information";
            
            // lblBookingReference
            this.lblBookingReference.AutoSize = true;
            this.lblBookingReference.Font = new Font("Segoe UI", 10F);
            this.lblBookingReference.Location = new Point(20, 35);
            this.lblBookingReference.Name = "lblBookingReference";
            this.lblBookingReference.Size = new Size(125, 19);
            this.lblBookingReference.TabIndex = 0;
            this.lblBookingReference.Text = "Booking Reference:";
            
            // txtBookingReference
            this.txtBookingReference.Font = new Font("Segoe UI", 10F);
            this.txtBookingReference.Location = new Point(155, 33);
            this.txtBookingReference.Name = "txtBookingReference";
            this.txtBookingReference.Size = new Size(150, 25);
            this.txtBookingReference.TabIndex = 1;
            
            // btnGenerateReference
            this.btnGenerateReference.BackColor = Color.FromArgb(25, 118, 210);
            this.btnGenerateReference.FlatAppearance.BorderSize = 0;
            this.btnGenerateReference.FlatStyle = FlatStyle.Flat;
            this.btnGenerateReference.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnGenerateReference.ForeColor = Color.White;
            this.btnGenerateReference.Location = new Point(320, 32);
            this.btnGenerateReference.Name = "btnGenerateReference";
            this.btnGenerateReference.Size = new Size(80, 27);
            this.btnGenerateReference.TabIndex = 2;
            this.btnGenerateReference.Text = "Generate";
            this.btnGenerateReference.UseVisualStyleBackColor = false;
            this.btnGenerateReference.Click += new EventHandler(this.btnGenerateReference_Click);
            
            // lblFlight
            this.lblFlight.AutoSize = true;
            this.lblFlight.Font = new Font("Segoe UI", 10F);
            this.lblFlight.Location = new Point(20, 75);
            this.lblFlight.Name = "lblFlight";
            this.lblFlight.Size = new Size(42, 19);
            this.lblFlight.TabIndex = 3;
            this.lblFlight.Text = "Flight:";
            
            // cmbFlight
            this.cmbFlight.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFlight.Font = new Font("Segoe UI", 10F);
            this.cmbFlight.FormattingEnabled = true;
            this.cmbFlight.Location = new Point(75, 73);
            this.cmbFlight.Name = "cmbFlight";
            this.cmbFlight.Size = new Size(470, 25);
            this.cmbFlight.TabIndex = 4;
            
            // lblPassenger
            this.lblPassenger.AutoSize = true;
            this.lblPassenger.Font = new Font("Segoe UI", 10F);
            this.lblPassenger.Location = new Point(20, 115);
            this.lblPassenger.Name = "lblPassenger";
            this.lblPassenger.Size = new Size(71, 19);
            this.lblPassenger.TabIndex = 5;
            this.lblPassenger.Text = "Passenger:";
            
            // cmbPassenger
            this.cmbPassenger.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPassenger.Font = new Font("Segoe UI", 10F);
            this.cmbPassenger.FormattingEnabled = true;
            this.cmbPassenger.Location = new Point(100, 113);
            this.cmbPassenger.Name = "cmbPassenger";
            this.cmbPassenger.Size = new Size(445, 25);
            this.cmbPassenger.TabIndex = 6;
            
            // grpSpecialRequests
            this.grpSpecialRequests.Controls.Add(this.txtSpecialRequests);
            this.grpSpecialRequests.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpSpecialRequests.Location = new Point(20, 240);
            this.grpSpecialRequests.Name = "grpSpecialRequests";
            this.grpSpecialRequests.Size = new Size(560, 120);
            this.grpSpecialRequests.TabIndex = 1;
            this.grpSpecialRequests.TabStop = false;
            this.grpSpecialRequests.Text = "Special Requests";
            
            // txtSpecialRequests
            this.txtSpecialRequests.Font = new Font("Segoe UI", 10F);
            this.txtSpecialRequests.Location = new Point(20, 30);
            this.txtSpecialRequests.Multiline = true;
            this.txtSpecialRequests.Name = "txtSpecialRequests";
            this.txtSpecialRequests.ScrollBars = ScrollBars.Vertical;
            this.txtSpecialRequests.Size = new Size(520, 80);
            this.txtSpecialRequests.TabIndex = 0;
            
            // grpBaggageInfo
            this.grpBaggageInfo.Controls.Add(this.txtBaggageCount);
            this.grpBaggageInfo.Controls.Add(this.lblBaggageCount);
            this.grpBaggageInfo.Controls.Add(this.txtBaggageWeight);
            this.grpBaggageInfo.Controls.Add(this.lblBaggageWeight);
            this.grpBaggageInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpBaggageInfo.Location = new Point(20, 380);
            this.grpBaggageInfo.Name = "grpBaggageInfo";
            this.grpBaggageInfo.Size = new Size(560, 100);
            this.grpBaggageInfo.TabIndex = 2;
            this.grpBaggageInfo.TabStop = false;
            this.grpBaggageInfo.Text = "Baggage Information";
            
            // lblBaggageWeight
            this.lblBaggageWeight.AutoSize = true;
            this.lblBaggageWeight.Font = new Font("Segoe UI", 10F);
            this.lblBaggageWeight.Location = new Point(20, 35);
            this.lblBaggageWeight.Name = "lblBaggageWeight";
            this.lblBaggageWeight.Size = new Size(110, 19);
            this.lblBaggageWeight.TabIndex = 0;
            this.lblBaggageWeight.Text = "Baggage Weight:";
            
            // txtBaggageWeight
            this.txtBaggageWeight.Font = new Font("Segoe UI", 10F);
            this.txtBaggageWeight.Location = new Point(140, 33);
            this.txtBaggageWeight.Name = "txtBaggageWeight";
            this.txtBaggageWeight.Size = new Size(100, 25);
            this.txtBaggageWeight.TabIndex = 1;
            
            // lblBaggageCount
            this.lblBaggageCount.AutoSize = true;
            this.lblBaggageCount.Font = new Font("Segoe UI", 10F);
            this.lblBaggageCount.Location = new Point(260, 35);
            this.lblBaggageCount.Name = "lblBaggageCount";
            this.lblBaggageCount.Size = new Size(105, 19);
            this.lblBaggageCount.TabIndex = 2;
            this.lblBaggageCount.Text = "Baggage Count:";
            
            // txtBaggageCount
            this.txtBaggageCount.Font = new Font("Segoe UI", 10F);
            this.txtBaggageCount.Location = new Point(375, 33);
            this.txtBaggageCount.Name = "txtBaggageCount";
            this.txtBaggageCount.Size = new Size(100, 25);
            this.txtBaggageCount.TabIndex = 3;
            
            // btnCreate
            this.btnCreate.BackColor = Color.FromArgb(76, 175, 80);
            this.btnCreate.FlatAppearance.BorderSize = 0;
            this.btnCreate.FlatStyle = FlatStyle.Flat;
            this.btnCreate.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnCreate.ForeColor = Color.White;
            this.btnCreate.Location = new Point(350, 600);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new Size(100, 40);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new EventHandler(this.btnCreate_Click);
            
            // btnCancel
            this.btnCancel.BackColor = Color.FromArgb(158, 158, 158);
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(470, 600);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(100, 40);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            
            // AddBookingForm
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(600, 700);
            this.Controls.Add(this.panelMain);
            this.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Name = "AddBookingForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Add New Booking";
            
            this.panelMain.ResumeLayout(false);
            this.grpBaggageInfo.ResumeLayout(false);
            this.grpBaggageInfo.PerformLayout();
            this.grpSpecialRequests.ResumeLayout(false);
            this.grpSpecialRequests.PerformLayout();
            this.grpBookingInfo.ResumeLayout(false);
            this.grpBookingInfo.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
