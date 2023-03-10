using GlassTest.Documents.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlassTest.Documents.Repositories.EntityConfigurations
{
    public class DocumentEntityTypeConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                    .UseIdentityColumn();
            builder.Property(x => x.Title).HasMaxLength(255);
            builder.Property(x => x.Content).HasMaxLength(int.MaxValue);
        }
    }
}