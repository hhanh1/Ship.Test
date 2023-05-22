namespace Ship.Common.Models
{
    public class BaseSearchViewModel
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string? SortBy { get; set; }
        public bool? SortDes { get; set; }
    }
}
