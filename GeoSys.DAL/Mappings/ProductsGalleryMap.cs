#region - Using

using GeoSys.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace GeoSys.DAL.Mappings
{
    public class ProductsGalleryMap : BaseMap<ProductsGallery>
    {
        public override void Configure(EntityTypeBuilder<ProductsGallery> builder)
        {
            builder.ToTable("ProductsGallery");
            builder.Property(x => x.Title).IsRequired();
        }
    }
}
