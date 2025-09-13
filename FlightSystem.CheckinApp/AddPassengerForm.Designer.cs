namespace FlightSystem.CheckinApp
{
    partial class AddPassengerForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelMain;
        private GroupBox grpPersonalInfo;
        private TextBox txtFirstName;
        private Label lblFirstName;
        private TextBox txtLastName;
        private Label lblLastName;
        private TextBox txtPassportNumber;
        private Label lblPassportNumber;
        private TextBox txtNationality;
        private Label lblNationality;
        private DateTimePicker dtpDateOfBirth;
        private Label lblDateOfBirth;
        private ComboBox cmbPassengerType;
        private Label lblPassengerType;
        private GroupBox grpContactInfo;
        private TextBox txtPhoneNumber;
        private Label lblPhoneNumber;
        private TextBox txtEmail;
        private Label lblEmail;
        private TextBox txtAddress;
        private Label lblAddress;
        private GroupBox grpEmergencyContact;
        private TextBox txtEmergencyContactName;
        private Label lblEmergencyContactName;
        private TextBox txtEmergencyContactPhone;
        private Label lblEmergencyContactPhone;
        private GroupBox grpSpecialRequests;
        private TextBox txtSpecialRequests;
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
            panelMain = new Panel();
            btnCancel = new Button();
            btnCreate = new Button();
            grpSpecialRequests = new GroupBox();
            txtSpecialRequests = new TextBox();
            grpEmergencyContact = new GroupBox();
            txtEmergencyContactPhone = new TextBox();
            lblEmergencyContactPhone = new Label();
            txtEmergencyContactName = new TextBox();
            lblEmergencyContactName = new Label();
            grpContactInfo = new GroupBox();
            txtAddress = new TextBox();
            lblAddress = new Label();
            txtEmail = new TextBox();
            lblEmail = new Label();
            txtPhoneNumber = new TextBox();
            lblPhoneNumber = new Label();
            grpPersonalInfo = new GroupBox();
            cmbPassengerType = new ComboBox();
            lblPassengerType = new Label();
            dtpDateOfBirth = new DateTimePicker();
            lblDateOfBirth = new Label();
            txtNationality = new TextBox();
            lblNationality = new Label();
            txtPassportNumber = new TextBox();
            lblPassportNumber = new Label();
            txtLastName = new TextBox();
            lblLastName = new Label();
            txtFirstName = new TextBox();
            lblFirstName = new Label();
            panelMain.SuspendLayout();
            grpSpecialRequests.SuspendLayout();
            grpEmergencyContact.SuspendLayout();
            grpContactInfo.SuspendLayout();
            grpPersonalInfo.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(btnCancel);
            panelMain.Controls.Add(btnCreate);
            panelMain.Controls.Add(grpSpecialRequests);
            panelMain.Controls.Add(grpEmergencyContact);
            panelMain.Controls.Add(grpContactInfo);
            panelMain.Controls.Add(grpPersonalInfo);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(700, 800);
            panelMain.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(158, 158, 158);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(590, 700);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 40);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.FromArgb(76, 175, 80);
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCreate.ForeColor = Color.White;
            btnCreate.Location = new Point(480, 700);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(100, 40);
            btnCreate.TabIndex = 4;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += btnCreate_Click;
            // 
            // grpSpecialRequests
            // 
            grpSpecialRequests.Controls.Add(txtSpecialRequests);
            grpSpecialRequests.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpSpecialRequests.Location = new Point(20, 500);
            grpSpecialRequests.Name = "grpSpecialRequests";
            grpSpecialRequests.Size = new Size(660, 120);
            grpSpecialRequests.TabIndex = 3;
            grpSpecialRequests.TabStop = false;
            grpSpecialRequests.Text = "Special Requests";
            // 
            // txtSpecialRequests
            // 
            txtSpecialRequests.Font = new Font("Segoe UI", 10F);
            txtSpecialRequests.Location = new Point(20, 30);
            txtSpecialRequests.Multiline = true;
            txtSpecialRequests.Name = "txtSpecialRequests";
            txtSpecialRequests.ScrollBars = ScrollBars.Vertical;
            txtSpecialRequests.Size = new Size(620, 80);
            txtSpecialRequests.TabIndex = 0;
            // 
            // grpEmergencyContact
            // 
            grpEmergencyContact.Controls.Add(txtEmergencyContactPhone);
            grpEmergencyContact.Controls.Add(lblEmergencyContactPhone);
            grpEmergencyContact.Controls.Add(txtEmergencyContactName);
            grpEmergencyContact.Controls.Add(lblEmergencyContactName);
            grpEmergencyContact.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpEmergencyContact.Location = new Point(20, 380);
            grpEmergencyContact.Name = "grpEmergencyContact";
            grpEmergencyContact.Size = new Size(660, 100);
            grpEmergencyContact.TabIndex = 2;
            grpEmergencyContact.TabStop = false;
            grpEmergencyContact.Text = "Emergency Contact";
            // 
            // txtEmergencyContactPhone
            // 
            txtEmergencyContactPhone.Font = new Font("Segoe UI", 10F);
            txtEmergencyContactPhone.Location = new Point(440, 33);
            txtEmergencyContactPhone.Name = "txtEmergencyContactPhone";
            txtEmergencyContactPhone.Size = new Size(150, 25);
            txtEmergencyContactPhone.TabIndex = 3;
            // 
            // lblEmergencyContactPhone
            // 
            lblEmergencyContactPhone.AutoSize = true;
            lblEmergencyContactPhone.Font = new Font("Segoe UI", 10F);
            lblEmergencyContactPhone.Location = new Point(330, 35);
            lblEmergencyContactPhone.Name = "lblEmergencyContactPhone";
            lblEmergencyContactPhone.Size = new Size(103, 19);
            lblEmergencyContactPhone.TabIndex = 2;
            lblEmergencyContactPhone.Text = "Contact Phone:";
            // 
            // txtEmergencyContactName
            // 
            txtEmergencyContactName.Font = new Font("Segoe UI", 10F);
            txtEmergencyContactName.Location = new Point(120, 32);
            txtEmergencyContactName.Name = "txtEmergencyContactName";
            txtEmergencyContactName.Size = new Size(200, 25);
            txtEmergencyContactName.TabIndex = 1;
            // 
            // lblEmergencyContactName
            // 
            lblEmergencyContactName.AutoSize = true;
            lblEmergencyContactName.Font = new Font("Segoe UI", 10F);
            lblEmergencyContactName.Location = new Point(20, 35);
            lblEmergencyContactName.Name = "lblEmergencyContactName";
            lblEmergencyContactName.Size = new Size(100, 19);
            lblEmergencyContactName.TabIndex = 0;
            lblEmergencyContactName.Text = "Contact Name:";
            // 
            // grpContactInfo
            // 
            grpContactInfo.Controls.Add(txtAddress);
            grpContactInfo.Controls.Add(lblAddress);
            grpContactInfo.Controls.Add(txtEmail);
            grpContactInfo.Controls.Add(lblEmail);
            grpContactInfo.Controls.Add(txtPhoneNumber);
            grpContactInfo.Controls.Add(lblPhoneNumber);
            grpContactInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpContactInfo.Location = new Point(20, 240);
            grpContactInfo.Name = "grpContactInfo";
            grpContactInfo.Size = new Size(660, 120);
            grpContactInfo.TabIndex = 1;
            grpContactInfo.TabStop = false;
            grpContactInfo.Text = "Contact Information";
            // 
            // txtAddress
            // 
            txtAddress.Font = new Font("Segoe UI", 10F);
            txtAddress.Location = new Point(90, 68);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(550, 25);
            txtAddress.TabIndex = 5;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 10F);
            lblAddress.Location = new Point(20, 70);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(61, 19);
            lblAddress.TabIndex = 4;
            lblAddress.Text = "Address:";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(350, 33);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(200, 25);
            txtEmail.TabIndex = 3;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.Location = new Point(300, 35);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(44, 19);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email:";
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Font = new Font("Segoe UI", 10F);
            txtPhoneNumber.Location = new Point(130, 33);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(150, 25);
            txtPhoneNumber.TabIndex = 1;
            // 
            // lblPhoneNumber
            // 
            lblPhoneNumber.AutoSize = true;
            lblPhoneNumber.Font = new Font("Segoe UI", 10F);
            lblPhoneNumber.Location = new Point(20, 35);
            lblPhoneNumber.Name = "lblPhoneNumber";
            lblPhoneNumber.Size = new Size(105, 19);
            lblPhoneNumber.TabIndex = 0;
            lblPhoneNumber.Text = "Phone Number:";
            // 
            // grpPersonalInfo
            // 
            grpPersonalInfo.Controls.Add(cmbPassengerType);
            grpPersonalInfo.Controls.Add(lblPassengerType);
            grpPersonalInfo.Controls.Add(dtpDateOfBirth);
            grpPersonalInfo.Controls.Add(lblDateOfBirth);
            grpPersonalInfo.Controls.Add(txtNationality);
            grpPersonalInfo.Controls.Add(lblNationality);
            grpPersonalInfo.Controls.Add(txtPassportNumber);
            grpPersonalInfo.Controls.Add(lblPassportNumber);
            grpPersonalInfo.Controls.Add(txtLastName);
            grpPersonalInfo.Controls.Add(lblLastName);
            grpPersonalInfo.Controls.Add(txtFirstName);
            grpPersonalInfo.Controls.Add(lblFirstName);
            grpPersonalInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpPersonalInfo.Location = new Point(20, 20);
            grpPersonalInfo.Name = "grpPersonalInfo";
            grpPersonalInfo.Size = new Size(660, 200);
            grpPersonalInfo.TabIndex = 0;
            grpPersonalInfo.TabStop = false;
            grpPersonalInfo.Text = "Personal Information";
            // 
            // cmbPassengerType
            // 
            cmbPassengerType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPassengerType.Font = new Font("Segoe UI", 10F);
            cmbPassengerType.FormattingEnabled = true;
            cmbPassengerType.Location = new Point(380, 103);
            cmbPassengerType.Name = "cmbPassengerType";
            cmbPassengerType.Size = new Size(120, 25);
            cmbPassengerType.TabIndex = 11;
            // 
            // lblPassengerType
            // 
            lblPassengerType.AutoSize = true;
            lblPassengerType.Font = new Font("Segoe UI", 10F);
            lblPassengerType.Location = new Point(260, 105);
            lblPassengerType.Name = "lblPassengerType";
            lblPassengerType.Size = new Size(106, 19);
            lblPassengerType.TabIndex = 10;
            lblPassengerType.Text = "Passenger Type:";
            // 
            // dtpDateOfBirth
            // 
            dtpDateOfBirth.Font = new Font("Segoe UI", 10F);
            dtpDateOfBirth.Format = DateTimePickerFormat.Short;
            dtpDateOfBirth.Location = new Point(120, 103);
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            dtpDateOfBirth.Size = new Size(120, 25);
            dtpDateOfBirth.TabIndex = 9;
            // 
            // lblDateOfBirth
            // 
            lblDateOfBirth.AutoSize = true;
            lblDateOfBirth.Font = new Font("Segoe UI", 10F);
            lblDateOfBirth.Location = new Point(20, 105);
            lblDateOfBirth.Name = "lblDateOfBirth";
            lblDateOfBirth.Size = new Size(90, 19);
            lblDateOfBirth.TabIndex = 8;
            lblDateOfBirth.Text = "Date of Birth:";
            // 
            // txtNationality
            // 
            txtNationality.Font = new Font("Segoe UI", 10F);
            txtNationality.Location = new Point(410, 68);
            txtNationality.Name = "txtNationality";
            txtNationality.Size = new Size(120, 25);
            txtNationality.TabIndex = 7;
            // 
            // lblNationality
            // 
            lblNationality.AutoSize = true;
            lblNationality.Font = new Font("Segoe UI", 10F);
            lblNationality.Location = new Point(320, 70);
            lblNationality.Name = "lblNationality";
            lblNationality.Size = new Size(78, 19);
            lblNationality.TabIndex = 6;
            lblNationality.Text = "Nationality:";
            // 
            // txtPassportNumber
            // 
            txtPassportNumber.Font = new Font("Segoe UI", 10F);
            txtPassportNumber.Location = new Point(150, 68);
            txtPassportNumber.Name = "txtPassportNumber";
            txtPassportNumber.Size = new Size(150, 25);
            txtPassportNumber.TabIndex = 5;
            // 
            // lblPassportNumber
            // 
            lblPassportNumber.AutoSize = true;
            lblPassportNumber.Font = new Font("Segoe UI", 10F);
            lblPassportNumber.Location = new Point(20, 70);
            lblPassportNumber.Name = "lblPassportNumber";
            lblPassportNumber.Size = new Size(119, 19);
            lblPassportNumber.TabIndex = 4;
            lblPassportNumber.Text = "Passport Number:";
            // 
            // txtLastName
            // 
            txtLastName.Font = new Font("Segoe UI", 10F);
            txtLastName.Location = new Point(370, 33);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(150, 25);
            txtLastName.TabIndex = 3;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Font = new Font("Segoe UI", 10F);
            lblLastName.Location = new Point(280, 35);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(77, 19);
            lblLastName.TabIndex = 2;
            lblLastName.Text = "Last Name:";
            // 
            // txtFirstName
            // 
            txtFirstName.Font = new Font("Segoe UI", 10F);
            txtFirstName.Location = new Point(110, 33);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(150, 25);
            txtFirstName.TabIndex = 1;
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Font = new Font("Segoe UI", 10F);
            lblFirstName.Location = new Point(20, 35);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(78, 19);
            lblFirstName.TabIndex = 0;
            lblFirstName.Text = "First Name:";
            // 
            // AddPassengerForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 800);
            Controls.Add(panelMain);
            Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "AddPassengerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add New Passenger";
            panelMain.ResumeLayout(false);
            grpSpecialRequests.ResumeLayout(false);
            grpSpecialRequests.PerformLayout();
            grpEmergencyContact.ResumeLayout(false);
            grpEmergencyContact.PerformLayout();
            grpContactInfo.ResumeLayout(false);
            grpContactInfo.PerformLayout();
            grpPersonalInfo.ResumeLayout(false);
            grpPersonalInfo.PerformLayout();
            ResumeLayout(false);
        }
    }
}
