using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TagManage.Data.Entities;

namespace TagManage.Data.EntityMapping
{
    public class TagEntityMap : IEntityTypeConfiguration<TagEntity>
    {
        public void Configure(EntityTypeBuilder<TagEntity> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(64)
                .IsRequired();
            builder.Property(p => p.Count)
                .IsRequired();
            builder.Property(p => p.IsRequired)
                .IsRequired();
            builder.Property(p => p.IsModeratorOnly)
                .IsRequired();
            builder.Property(p => p.HasSynonyms)
                .IsRequired();
                
        }
    }
}
