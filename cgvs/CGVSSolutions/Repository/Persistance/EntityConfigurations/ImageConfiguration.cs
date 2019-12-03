using Models;
using System.Data.Entity.ModelConfiguration;

namespace Repository.Persistance.EntityConfigurations
{
    public class ImageConfiguration : EntityTypeConfiguration<Images>
    {
        public ImageConfiguration()
        {
            Property(i => i.Path).IsMaxLength().IsRequired().HasColumnType("nVarchar");
            Property(i=>i.Id).IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(i => i.Version).IsOptional();
        }
    }
}
