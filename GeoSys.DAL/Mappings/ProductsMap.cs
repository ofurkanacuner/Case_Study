#region - Using

using GeoSys.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace GeoSys.DAL.Mappings
{
    public class ProductsMap : BaseMap<Products>
    {
        public override void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.ToTable("Products");
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Price).IsRequired();
        }
    }
}
