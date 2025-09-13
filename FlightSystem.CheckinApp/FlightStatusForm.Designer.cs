namespace FlightSystem.CheckinApp
{
    partial class FlightStatusForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblCurrentFlight;
        private Label lblCurrentStatus;
        private Label lblNewStatus;
        private ComboBox cmbNewStatus;
        private Button btnOK;
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
            this.lblCurrentFlight = new Label();
            this.lblCurrentStatus = new Label();
            this.lblNewStatus = new Label();
            this.cmbNewStatus = new ComboBox();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();
            
            // lblCurrentFlight
            this.lblCurrentFlight.AutoSize = true;
            this.lblCurrentFlight.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.lblCurrentFlight.Location = new Point(12, 15);
            this.lblCurrentFlight.Name = "lblCurrentFlight";
            this.lblCurrentFlight.Size = new Size(80, 17);
            this.lblCurrentFlight.TabIndex = 0;
            this.lblCurrentFlight.Text = "Нислэг:";
            
            // lblCurrentStatus
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.Font = new Font("Microsoft Sans Serif", 9F);
            this.lblCurrentStatus.ForeColor = Color.Blue;
            this.lblCurrentStatus.Location = new Point(12, 45);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new Size(120, 15);
            this.lblCurrentStatus.TabIndex = 1;
            this.lblCurrentStatus.Text = "Одоогийн төлөв:";
            
            // lblNewStatus
            this.lblNewStatus.AutoSize = true;
            this.lblNewStatus.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            this.lblNewStatus.Location = new Point(12, 80);
            this.lblNewStatus.Name = "lblNewStatus";
            this.lblNewStatus.Size = new Size(85, 15);
            this.lblNewStatus.TabIndex = 2;
            this.lblNewStatus.Text = "Шинэ төлөв:";
            
            // cmbNewStatus
            this.cmbNewStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbNewStatus.Font = new Font("Microsoft Sans Serif", 9F);
            this.cmbNewStatus.FormattingEnabled = true;
            this.cmbNewStatus.Location = new Point(12, 98);
            this.cmbNewStatus.Name = "cmbNewStatus";
            this.cmbNewStatus.Size = new Size(300, 23);
            this.cmbNewStatus.TabIndex = 3;
            
            // btnOK
            this.btnOK.BackColor = Color.Green;
            this.btnOK.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            this.btnOK.ForeColor = Color.White;
            this.btnOK.Location = new Point(120, 140);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(90, 30);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "Өөрчлөх";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            
            // btnCancel
            this.btnCancel.BackColor = Color.Red;
            this.btnCancel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(220, 140);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(90, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Цуцлах";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            
            // FlightStatusForm
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(330, 190);
            this.Controls.Add(this.lblCurrentFlight);
            this.Controls.Add(this.lblCurrentStatus);
            this.Controls.Add(this.lblNewStatus);
            this.Controls.Add(this.cmbNewStatus);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FlightStatusForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Нислэгийн төлөв өөрчлөх";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
