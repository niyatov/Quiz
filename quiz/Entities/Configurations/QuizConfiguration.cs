using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace quiz.Entities.Configurations;
public class QuizConfiguration : EntityBaseConfiguration<Quiz>
{
    public override void Configure(EntityTypeBuilder<Quiz> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.Title).HasColumnType("nvarchar(50)");
        builder.Property(b => b.Description).HasColumnType("nvarchar(250)");
        builder.Property(b => b.StartTime).HasColumnType("datetime");
        builder.Property(b => b.EndTime).HasColumnType("datetime");
        builder.Property(b => b.PasswordHash).HasColumnType("nvarchar(64)");
        builder.HasIndex(b => b.PasswordHash).IsUnique();
    }
}