using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace quiz.Entities.Configurations;
public class TopicConfiguration : EntityBaseConfiguration<Topic>
{
    public override void Configure(EntityTypeBuilder<Topic> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.Name).HasColumnType("nvarchar(50)").IsRequired();
        builder.Property(b => b.Description).HasColumnType("nvarchar(250)");
        builder.Property(b => b.NameHash).HasColumnType("nvarchar(64)").IsRequired();
        builder.HasIndex(b => b.NameHash).IsUnique();
        builder.Property(e => e.Difficulty)
        .HasConversion(
            v => v.ToString(),
            v => (EDifficulty)Enum.Parse(typeof(EDifficulty), v));
    }
}