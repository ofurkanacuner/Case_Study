#region - Using

using System.Collections.Generic;

#endregion

namespace GeoSys.DAL.Models
{
    public class Products : BaseModel
    {
        public string Title { get; set; }
        public float Price { get; set; }
        public Categories Categories { get; set; }
        public List<ProductsGallery> ProductsGallery { get; set; }
    }
}
