namespace FlightSystem.CheckinApp
{
    partial class FlightSelectionForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelMain;
        private Label lblTitle;
        private ListView lstFlights;
        private ColumnHeader colFlightNumber;
        private ColumnHeader colDeparture;
        private ColumnHeader colArrival;
        private ColumnHeader colTime;
        private ColumnHeader colStatus;
        private ColumnHeader colGate;
        private GroupBox grpFlightDetails;
        private TextBox txtFlightDetails;
        private Button btnSelect;
        private Button btnCancel;
        private Button btnRefresh;
        private Label lblStatus;

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
            lblStatus = new Label();
            btnRefresh = new Button();
            btnCancel = new Button();
            btnSelect = new Button();
            grpFlightDetails = new GroupBox();
            txtFlightDetails = new TextBox();
            lstFlights = new ListView();
            colFlightNumber = new ColumnHeader();
            colDeparture = new ColumnHeader();
            colArrival = new ColumnHeader();
            colTime = new ColumnHeader();
            colStatus = new ColumnHeader();
            colGate = new ColumnHeader();
            lblTitle = new Label();
            panelMain.SuspendLayout();
            grpFlightDetails.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(lblStatus);
            panelMain.Controls.Add(btnRefresh);
            panelMain.Controls.Add(btnCancel);
            panelMain.Controls.Add(btnSelect);
            panelMain.Controls.Add(grpFlightDetails);
            panelMain.Controls.Add(lstFlights);
            panelMain.Controls.Add(lblTitle);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(900, 700);
            panelMain.TabIndex = 0;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Microsoft Sans Serif", 10F);
            lblStatus.Location = new Point(20, 363);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(114, 17);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Ачаалж байна...";
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(25, 118, 210);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(780, 20);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 30);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Шинэчлэх";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(158, 158, 158);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(640, 600);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 40);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Цуцлах";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSelect
            // 
            btnSelect.BackColor = Color.FromArgb(76, 175, 80);
            btnSelect.Enabled = false;
            btnSelect.FlatAppearance.BorderSize = 0;
            btnSelect.FlatStyle = FlatStyle.Flat;
            btnSelect.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            btnSelect.ForeColor = Color.White;
            btnSelect.Location = new Point(500, 600);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(120, 40);
            btnSelect.TabIndex = 3;
            btnSelect.Text = "Сонгох";
            btnSelect.UseVisualStyleBackColor = false;
            btnSelect.Click += btnSelect_Click;
            // 
            // grpFlightDetails
            // 
            grpFlightDetails.Controls.Add(txtFlightDetails);
            grpFlightDetails.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            grpFlightDetails.Location = new Point(20, 380);
            grpFlightDetails.Name = "grpFlightDetails";
            grpFlightDetails.Size = new Size(860, 200);
            grpFlightDetails.TabIndex = 2;
            grpFlightDetails.TabStop = false;
            grpFlightDetails.Text = "Нислэгийн дэлгэрэнгүй мэдээлэл";
            // 
            // txtFlightDetails
            // 
            txtFlightDetails.Font = new Font("Microsoft Sans Serif", 9F);
            txtFlightDetails.Location = new Point(15, 25);
            txtFlightDetails.Multiline = true;
            txtFlightDetails.Name = "txtFlightDetails";
            txtFlightDetails.ReadOnly = true;
            txtFlightDetails.ScrollBars = ScrollBars.Vertical;
            txtFlightDetails.Size = new Size(830, 160);
            txtFlightDetails.TabIndex = 0;
            // 
            // lstFlights
            // 
            lstFlights.Columns.AddRange(new ColumnHeader[] { colFlightNumber, colDeparture, colArrival, colTime, colStatus, colGate });
            lstFlights.FullRowSelect = true;
            lstFlights.GridLines = true;
            lstFlights.Location = new Point(20, 60);
            lstFlights.MultiSelect = false;
            lstFlights.Name = "lstFlights";
            lstFlights.Size = new Size(860, 300);
            lstFlights.TabIndex = 1;
            lstFlights.UseCompatibleStateImageBehavior = false;
            lstFlights.View = View.Details;
            lstFlights.SelectedIndexChanged += lstFlights_SelectedIndexChanged;
            lstFlights.DoubleClick += lstFlights_DoubleClick;
            // 
            // colFlightNumber
            // 
            colFlightNumber.Text = "Нислэгийн дугаар";
            colFlightNumber.Width = 120;
            // 
            // colDeparture
            // 
            colDeparture.Text = "Хөөрөх газар";
            colDeparture.Width = 100;
            // 
            // colArrival
            // 
            colArrival.Text = "Очих газар";
            colArrival.Width = 100;
            // 
            // colTime
            // 
            colTime.Text = "Хөөрөх цаг";
            colTime.Width = 100;
            // 
            // colStatus
            // 
            colStatus.Text = "Төлөв";
            colStatus.Width = 120;
            // 
            // colGate
            // 
            colGate.Text = "Хаалга";
            colGate.Width = 80;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(25, 118, 210);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(168, 26);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Нислэг сонгох";
            // 
            // FlightSelectionForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 700);
            Controls.Add(panelMain);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FlightSelectionForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Нислэг сонгох";
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            grpFlightDetails.ResumeLayout(false);
            grpFlightDetails.PerformLayout();
            ResumeLayout(false);
        }
    }
}
