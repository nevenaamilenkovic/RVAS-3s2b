namespace Tsql3s2b.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public List<Product> Products { get; set; } = new();
        public string SearchTerm { get; set; } = string.Empty;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
