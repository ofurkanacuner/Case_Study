#region - Using

using GeoSys.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace GeoSys.DAL.Mappings
{
    public class CategoriesMap : BaseMap<Categories>
    {
        public override void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.ToTable("Categories");
            builder.Property(x => x.Title).IsRequired();

        }
    }
}
