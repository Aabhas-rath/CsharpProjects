using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Persistance.EntityConfigurations
{
    public class AlbumConfigurations : EntityTypeConfiguration<Album>
    {
        public AlbumConfigurations()
        {
            Property(i => i.FolderPath).HasColumnType("nVarchar").HasMaxLength(1000).IsRequired().HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            Property(i => i.Name).HasColumnType("nVarchar").HasMaxLength(300).IsRequired().HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute() { IsUnique = true}));
            Property(i=>i.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(i => i.Images).WithOptional(i =>i.Album).HasForeignKey(a=>a.AlbumId);
        }
    }
}
