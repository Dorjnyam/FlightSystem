namespace FlightSystem.Core.DTOs.Common
{
    public class PaginatedResultDto<T>
    {
        public List<T> Data { get; set; } = [];
        public PaginationDto Pagination { get; set; } = new();
    }
}
