#region - Using


#endregion

namespace GeoSys.DAL.Models
{
    public class ProductsGallery : BaseModel
    {
        public string Title { get; set; }
        public int ProductId { get; set; }
        public Products Products { get; set; }
    }
}
