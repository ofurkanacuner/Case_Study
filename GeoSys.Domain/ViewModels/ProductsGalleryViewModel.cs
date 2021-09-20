#region - Using

using System.ComponentModel.DataAnnotations;

#endregion

namespace GeoSys.Domain.ViewModels
{
    public class ProductsGalleryViewModel : BaseViewModel
    {
        [Display(Name = "Başlık : ")]
        public string Title { get; set; }

        [Display(Name = "Ürün Adı : ")]
        public int ProductId { get; set; }

        public string ProductsTitle { get; set; }
    }
}
