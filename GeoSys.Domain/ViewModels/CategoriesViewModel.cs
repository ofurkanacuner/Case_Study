#region - Using

using System.ComponentModel.DataAnnotations;

#endregion

namespace GeoSys.Domain.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        [Display(Name = "Kategori Adı : ")]
        public string Title { get; set; }

        [Display(Name = "Üst Kategori Adı : ")]
        public int? TopCategoriId { get; set; }

        public string TopCategoriTitle { get; set; }
    }
}

