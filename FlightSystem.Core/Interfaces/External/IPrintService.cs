using FlightSystem.Core.DTOs.Response;

namespace FlightSystem.Core.Interfaces.External
{
    public interface IPrintService
    {
        Task<bool> PrintBoardingPassAsync(BoardingPassDto boardingPass);
        Task<byte[]> GenerateBoardingPassPdfAsync(BoardingPassDto boardingPass);
        Task<bool> IsPrinterAvailableAsync();
        Task<PrintStatusDto> GetPrintStatusAsync();
    }
}
