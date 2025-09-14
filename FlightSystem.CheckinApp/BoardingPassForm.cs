using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using FlightSystem.Shared.DTOs.Response;
using QRCoder;

namespace FlightSystem.CheckinApp
{
    public partial class BoardingPassForm : Form
    {
        private BoardingPassDto _boardingPass;
        private PrintDocument _printDocument;
        private PictureBox _qrCodePictureBox;
        private PictureBox _barcodePictureBox;

        public BoardingPassForm(BoardingPassDto boardingPass)
        {
            _boardingPass = boardingPass;
            InitializeComponent();
            SetupBoardingPass();
            GenerateQRCodes();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties - Make it much larger and properly positioned
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(900, 700); // Much larger form
            this.Name = "BoardingPassForm";
            this.Text = "Boarding Pass - Print Preview";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.BackColor = Color.White;
            
            // Create main panel for boarding pass
            var mainPanel = new Panel
            {
                Size = new Size(850, 550),
                Location = new Point(25, 25),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(mainPanel);

            // Header section
            var headerPanel = new Panel
            {
                Size = new Size(830, 80),
                Location = new Point(10, 10),
                BackColor = Color.DodgerBlue
            };
            mainPanel.Controls.Add(headerPanel);

            // Airline name
            var lblAirline = new Label
            {
                Text = "MIAT MONGOLIAN AIRLINES",
                Font = new Font("Arial", 18F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 15),
                Size = new Size(400, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };
            headerPanel.Controls.Add(lblAirline);

            // Boarding Pass title
            var lblBoardingPass = new Label
            {
                Text = "BOARDING PASS",
                Font = new Font("Arial", 24F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(450, 15),
                Size = new Size(350, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };
            headerPanel.Controls.Add(lblBoardingPass);

            // Flight info section
            var flightPanel = new Panel
            {
                Size = new Size(830, 120),
                Location = new Point(10, 100),
                BackColor = Color.LightGray
            };
            mainPanel.Controls.Add(flightPanel);

            // Flight number
            var lblFlightNumber = new Label
            {
                Text = $"FLIGHT {_boardingPass.Flight?.FlightNumber ?? "N/A"}",
                Font = new Font("Arial", 20F, FontStyle.Bold),
                Location = new Point(20, 10),
                Size = new Size(200, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };
            flightPanel.Controls.Add(lblFlightNumber);

            // Route
            var lblRoute = new Label
            {
                Text = $"{_boardingPass.Flight?.DepartureAirport ?? "N/A"} → {_boardingPass.Flight?.ArrivalAirport ?? "N/A"}",
                Font = new Font("Arial", 16F, FontStyle.Bold),
                Location = new Point(20, 45),
                Size = new Size(300, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            flightPanel.Controls.Add(lblRoute);

            // Departure time
            var lblDeparture = new Label
            {
                Text = $"DEPARTURE: {_boardingPass.Flight?.ScheduledDeparture:yyyy-MM-dd HH:mm}",
                Font = new Font("Arial", 12F),
                Location = new Point(20, 75),
                Size = new Size(400, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            flightPanel.Controls.Add(lblDeparture);

            // Gate info
            var lblGate = new Label
            {
                Text = $"GATE: {_boardingPass.Gate ?? "TBA"}",
                Font = new Font("Arial", 14F, FontStyle.Bold),
                Location = new Point(450, 10),
                Size = new Size(150, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };
            flightPanel.Controls.Add(lblGate);

            // Boarding time
            var lblBoardingTime = new Label
            {
                Text = $"BOARDING: {_boardingPass.BoardingTime:HH:mm}",
                Font = new Font("Arial", 12F, FontStyle.Bold),
                Location = new Point(450, 45),
                Size = new Size(200, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            flightPanel.Controls.Add(lblBoardingTime);

            // Seat info
            var lblSeat = new Label
            {
                Text = $"SEAT: {_boardingPass.Seat?.SeatNumber ?? "N/A"}",
                Font = new Font("Arial", 16F, FontStyle.Bold),
                Location = new Point(450, 75),
                Size = new Size(150, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            flightPanel.Controls.Add(lblSeat);

            // Passenger info section
            var passengerPanel = new Panel
            {
                Size = new Size(830, 80),
                Location = new Point(10, 230),
                BackColor = Color.White
            };
            mainPanel.Controls.Add(passengerPanel);

            // Passenger name
            var lblPassengerName = new Label
            {
                Text = $"PASSENGER: {_boardingPass.Passenger?.FirstName} {_boardingPass.Passenger?.LastName}",
                Font = new Font("Arial", 14F, FontStyle.Bold),
                Location = new Point(20, 10),
                Size = new Size(600, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            passengerPanel.Controls.Add(lblPassengerName);

            // Passport
            var lblPassport = new Label
            {
                Text = $"PASSPORT: {_boardingPass.Passenger?.PassportNumber}",
                Font = new Font("Arial", 12F),
                Location = new Point(20, 40),
                Size = new Size(400, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            passengerPanel.Controls.Add(lblPassport);

            // Booking reference
            var lblBookingRef = new Label
            {
                Text = $"BOOKING REF: {_boardingPass.BoardingPassCode}",
                Font = new Font("Arial", 12F, FontStyle.Bold),
                Location = new Point(450, 40),
                Size = new Size(350, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            passengerPanel.Controls.Add(lblBookingRef);

            // QR Code and Barcode section
            var codePanel = new Panel
            {
                Size = new Size(830, 150),
                Location = new Point(10, 320),
                BackColor = Color.White
            };
            mainPanel.Controls.Add(codePanel);

            // QR Code
            _qrCodePictureBox = new PictureBox
            {
                Size = new Size(120, 120),
                Location = new Point(20, 15),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };
            codePanel.Controls.Add(_qrCodePictureBox);

            var lblQRCode = new Label
            {
                Text = "QR CODE",
                Font = new Font("Arial", 10F, FontStyle.Bold),
                Location = new Point(20, 140),
                Size = new Size(120, 15),
                TextAlign = ContentAlignment.TopCenter
            };
            codePanel.Controls.Add(lblQRCode);

            // Barcode
            _barcodePictureBox = new PictureBox
            {
                Size = new Size(300, 120),
                Location = new Point(180, 15),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };
            codePanel.Controls.Add(_barcodePictureBox);

            var lblBarcode = new Label
            {
                Text = "BARCODE",
                Font = new Font("Arial", 10F, FontStyle.Bold),
                Location = new Point(180, 140),
                Size = new Size(300, 15),
                TextAlign = ContentAlignment.TopCenter
            };
            codePanel.Controls.Add(lblBarcode);

            // Footer info
            var footerPanel = new Panel
            {
                Size = new Size(830, 60),
                Location = new Point(10, 480),
                BackColor = Color.LightBlue
            };
            mainPanel.Controls.Add(footerPanel);

            var lblFooter1 = new Label
            {
                Text = "• Please arrive at the gate 30 minutes before departure",
                Font = new Font("Arial", 10F),
                Location = new Point(20, 10),
                Size = new Size(800, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            footerPanel.Controls.Add(lblFooter1);

            var lblFooter2 = new Label
            {
                Text = "• Valid ID and boarding pass required for boarding",
                Font = new Font("Arial", 10F),
                Location = new Point(20, 35),
                Size = new Size(800, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            footerPanel.Controls.Add(lblFooter2);

            // Control buttons
            var btnPrint = new Button
            {
                Text = "🖨️ Print Boarding Pass",
                Size = new Size(180, 50),
                Location = new Point(150, 600),
                Font = new Font("Arial", 12F, FontStyle.Bold),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnPrint.Click += BtnPrint_Click;
            this.Controls.Add(btnPrint);

            var btnClose = new Button
            {
                Text = "✕ Close",
                Size = new Size(120, 50),
                Location = new Point(750, 600),
                Font = new Font("Arial", 12F),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);

            this.ResumeLayout(false);
        }

        private void SetupBoardingPass()
        {
            _printDocument = new PrintDocument();
            _printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void GenerateQRCodes()
        {
            try
            {
                // Generate QR Code
                var qrText = $"BP:{_boardingPass.BoardingPassCode}|FL:{_boardingPass.Flight?.FlightNumber}|PS:{_boardingPass.Passenger?.PassportNumber}|ST:{_boardingPass.Seat?.SeatNumber}";
                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);
                var qrCodeImage = qrCode.GetGraphic(10);
                
                _qrCodePictureBox.Image = qrCodeImage;

                // Generate Barcode (simple text-based barcode simulation)
                var barcodeText = _boardingPass.BoardingPassCode ?? "N/A";
                var barcodeImage = GenerateBarcode(barcodeText);
                _barcodePictureBox.Image = barcodeImage;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generating QR/Barcode: {ex.Message}");
            }
        }

        private Bitmap GenerateBarcode(string text)
        {
            var bitmap = new Bitmap(300, 120);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
                
                var font = new Font("Arial", 10F, FontStyle.Bold);
                var brush = Brushes.Black;
                
                // Draw barcode lines (simplified representation)
                var random = new Random(text.GetHashCode());
                var x = 20;
                for (int i = 0; i < 50; i++)
                {
                    var height = random.Next(20, 80);
                    var width = random.Next(1, 4);
                    graphics.FillRectangle(brush, x, 20, width, height);
                    x += width + 1;
                }
                
                // Draw text below barcode
                graphics.DrawString(text, font, brush, 100, 105);
            }
            return bitmap;
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                var printDialog = new PrintDialog
                {
                    Document = _printDocument,
                    AllowPrintToFile = true,
                    AllowSelection = false,
                    AllowSomePages = false,
                    ShowHelp = false,
                    ShowNetwork = false
                };

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    _printDocument.Print();
                    MessageBox.Show("Boarding pass sent to printer successfully!", "Print Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing boarding pass: {ex.Message}", "Print Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            var graphics = e.Graphics;
            var font = new Font("Arial", 10F);
            var boldFont = new Font("Arial", 12F, FontStyle.Bold);
            var largeFont = new Font("Arial", 18F, FontStyle.Bold);
            var titleFont = new Font("Arial", 24F, FontStyle.Bold);
            
            var brush = Brushes.Black;
            var x = 50;
            var y = 50;

            // Header background
            graphics.FillRectangle(Brushes.DodgerBlue, x - 10, y - 10, 700, 60);
            graphics.DrawString("MIAT MONGOLIAN AIRLINES", largeFont, Brushes.White, x, y);
            graphics.DrawString("BOARDING PASS", titleFont, Brushes.White, x + 400, y);
            y += 80;

            // Flight information section
            graphics.FillRectangle(Brushes.LightGray, x - 10, y - 10, 700, 120);
            graphics.DrawString($"FLIGHT {_boardingPass.Flight?.FlightNumber ?? "N/A"}", largeFont, brush, x, y);
            graphics.DrawString($"{_boardingPass.Flight?.DepartureAirport ?? "N/A"} → {_boardingPass.Flight?.ArrivalAirport ?? "N/A"}", boldFont, brush, x, y + 30);
            graphics.DrawString($"DEPARTURE: {_boardingPass.Flight?.ScheduledDeparture:yyyy-MM-dd HH:mm}", font, brush, x, y + 60);
            graphics.DrawString($"GATE: {_boardingPass.Gate ?? "TBA"}", boldFont, brush, x + 400, y);
            graphics.DrawString($"BOARDING: {_boardingPass.BoardingTime:HH:mm}", font, brush, x + 400, y + 30);
            graphics.DrawString($"SEAT: {_boardingPass.Seat?.SeatNumber ?? "N/A"}", largeFont, brush, x + 400, y + 60);
            y += 140;

            // Passenger information
            graphics.DrawString("PASSENGER INFORMATION", boldFont, brush, x, y);
            y += 30;
            graphics.DrawString($"NAME: {_boardingPass.Passenger?.FirstName} {_boardingPass.Passenger?.LastName}", boldFont, brush, x, y);
            y += 25;
            graphics.DrawString($"PASSPORT: {_boardingPass.Passenger?.PassportNumber}", font, brush, x, y);
            graphics.DrawString($"BOOKING REF: {_boardingPass.BoardingPassCode}", boldFont, brush, x + 300, y);
            y += 40;

            // QR Code and Barcode
            if (_qrCodePictureBox.Image != null)
            {
                graphics.DrawImage(_qrCodePictureBox.Image, x, y, 120, 120);
            }
            
            if (_barcodePictureBox.Image != null)
            {
                graphics.DrawImage(_barcodePictureBox.Image, x + 150, y, 300, 120);
            }

            y += 150;

            // Footer
            graphics.DrawString("Please arrive at the gate 30 minutes before departure", font, brush, x, y);
            y += 20;
            graphics.DrawString("Valid ID and boarding pass required for boarding", font, brush, x, y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // The visual content is now handled by the controls in InitializeComponent
        }
    }
}