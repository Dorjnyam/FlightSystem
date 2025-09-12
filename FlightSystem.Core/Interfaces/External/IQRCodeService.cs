using FlightSystem.Core.DTOs.Response;

namespace FlightSystem.Core.Interfaces.External
{
    public interface IQRCodeService
    {
        string GenerateQRCode(string data);
        string GenerateBarcode(string data);
        bool ValidateQRCode(string qrCode);
        BoardingPassDataDto? DecodeQRCode(string qrCode);
    }
}
