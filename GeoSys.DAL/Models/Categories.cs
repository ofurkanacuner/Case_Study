#region - Using


#endregion

namespace GeoSys.DAL.Models
{
    public class Categories : BaseModel
    {
        public string Title { get; set; }
        public int? TopCategoriId { get; set; }
        public Categories TopCategori { get; set; }
    }
}
