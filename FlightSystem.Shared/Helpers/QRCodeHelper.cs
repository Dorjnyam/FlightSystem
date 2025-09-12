using QRCoder;
using FlightSystem.Core.Constants;
using FlightSystem.Core.DTOs.Response;

namespace FlightSystem.Shared.Helpers;

public interface IQRCodeHelper
{
    string GenerateQRCode(string data);
    byte[] GenerateQRCodeBytes(string data);
    bool ValidateQRCode(string qrCode);
    BoardingPassDataDto? DecodeQRCode(string qrCode);
}

public class QRCodeHelper : IQRCodeHelper
{
    public string GenerateQRCode(string data)
    {
        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode($"{SystemConstants.QR_CODE_PREFIX}{data}", QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new Base64QRCode(qrCodeData);
        
        return qrCode.GetGraphic(20);
    }

    public byte[] GenerateQRCodeBytes(string data)
    {
        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode($"{SystemConstants.QR_CODE_PREFIX}{data}", QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new PngByteQRCode(qrCodeData);
        
        return qrCode.GetGraphic(20);
    }

    public bool ValidateQRCode(string qrCode)
    {
        return !string.IsNullOrWhiteSpace(qrCode) && 
               qrCode.StartsWith(SystemConstants.QR_CODE_PREFIX);
    }

    public BoardingPassDataDto? DecodeQRCode(string qrCode)
    {
        if (!ValidateQRCode(qrCode))
            return null;

        try
        {
            var data = qrCode[SystemConstants.QR_CODE_PREFIX.Length..];
            var parts = data.Split('|');
            
            if (parts.Length < 3) return null;

            // Parse QR data: FP:123|SA:456|T:2024-01-01T10:00:00Z
            var flightPassengerId = int.Parse(parts[0].Split(':')[1]);
            var seatAssignmentId = int.Parse(parts[1].Split(':')[1]);
            var timestamp = DateTime.Parse(parts[2].Split(':')[1]);

            // В реальном приложении здесь бы был запрос к БД
            // Возвращаем заглушку
            return new BoardingPassDataDto
            {
                PassengerName = "Тест Зорчигч",
                FlightNumber = "MN123",
                SeatNumber = "12A",
                DepartureTime = timestamp,
                Gate = "A1",
                BoardingPassCode = $"BP{flightPassengerId:D4}"
            };
        }
        catch
        {
            return null;
        }
    }
}
