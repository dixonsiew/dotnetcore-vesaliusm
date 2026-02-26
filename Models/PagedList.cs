namespace vesalius_m.Models
{
    public class PagedList<T>
    {
        public required List<T> List { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
    }
}
