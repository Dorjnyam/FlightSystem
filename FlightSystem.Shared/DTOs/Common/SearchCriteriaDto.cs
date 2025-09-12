namespace FlightSystem.Shared.DTOs.Common
{
    public class SearchCriteriaDto
    {
        public string? SearchTerm { get; set; }
        public Dictionary<string, object> Filters { get; set; } = [];
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
