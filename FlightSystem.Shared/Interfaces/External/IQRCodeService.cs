using FlightSystem.Shared.DTOs.Response;

namespace FlightSystem.Shared.Interfaces.External
{
    public interface IQRCodeService
    {
        string GenerateQRCode(string data);
        string GenerateBarcode(string data);
        bool ValidateQRCode(string qrCode);
        BoardingPassDataDto? DecodeQRCode(string qrCode);
    }
}
