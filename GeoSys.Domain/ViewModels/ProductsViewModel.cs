namespace GeoSys.Domain.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public float Price { get; set; }
        public int CategoriId { get; set; }
        public string CategoriTitle { get; set; }
    }
}
